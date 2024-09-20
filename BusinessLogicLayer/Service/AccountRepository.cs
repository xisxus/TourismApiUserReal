using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using DataAccessLayer.Entites.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.ServiceResponse;

namespace BusinessLogicLayer.Service
{
    public class AccountRepository : IUserAccount
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;
        private readonly string _connectionString;

        public AccountRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.config = config;
            _connectionString = config.GetConnectionString("con");
        }




        public async Task<GeneralResponse> AssignRoleToUser(string userEmail, string roleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("AssignRoleToUser2", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserEmail", userEmail);
                    command.Parameters.AddWithValue("@RoleName", roleName);
                    connection.Open();

                    var result = (int)await command.ExecuteScalarAsync();

                    if (result == -1) return new GeneralResponse(false, "User not found");
                    if (result == -2) return new GeneralResponse(false, "Role not found");
                    if (result == -3) return new GeneralResponse(false, "Role already assigned");
                    return new GeneralResponse(true, "Role assigned to user successfully");
                }
            }
        }

        public async Task<GeneralResponse> UnassignRoleFromUser(string userEmail, string roleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("UnassignRoleFromUser2", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserEmail", userEmail);
                    command.Parameters.AddWithValue("@RoleName", roleName);
                    connection.Open();

                    var result = (int)await command.ExecuteScalarAsync();

                    if (result == -1) return new GeneralResponse(false, "User not found");
                    if (result == -2) return new GeneralResponse(false, "Role not found");
                    if (result == -3) return new GeneralResponse(false, "Role not assigned to the user");
                    if (result == -4) return new GeneralResponse(false, "Cannot unassign User role");
                    return new GeneralResponse(true, "Role unassigned from user successfully");
                }
            }
        }


        public async Task<ServiceResponse.GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            if (userDTO == null) return new GeneralResponse(false, "Model is empty");
            var newUser = new ApplicationUser()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };
            var user = await userManager.FindByNameAsync(newUser.Email);

            if (user is not null) return new GeneralResponse(false, "User Already Registered");
            var createuser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createuser.Succeeded) return new GeneralResponse(false, "Error Occured");
            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)

                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                await userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account created");

            }
        }

        public async Task<GeneralResponse> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return new GeneralResponse(false, "Role name is required");

            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("CreateRole3", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleName", roleName);
                    connection.Open();

                    var result = (int)await command.ExecuteScalarAsync();

                    if (result == -1) return new GeneralResponse(false, "Role already exists");
                    return new GeneralResponse(true, "Role created successfully");
                }
            }
        }

        public async Task<GeneralResponseData<List<UserWithRolesDTO>>> GetAllUsersWithRoles()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetAllUsersWithRoles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    var userRolesList = new List<UserWithRolesDTO>();

                    while (await reader.ReadAsync())
                    {
                        var userWithRoles = new UserWithRolesDTO
                        {
                            Id = reader.GetString(0),
                            Email = reader.GetString(1),
                            FullName = reader.GetString(2),
                            Roles = new List<string> { reader.GetString(3) }
                        };
                        userRolesList.Add(userWithRoles);
                    }

                    if (!userRolesList.Any())
                        return new GeneralResponseData<List<UserWithRolesDTO>>(false, "No users found", null);

                    return new GeneralResponseData<List<UserWithRolesDTO>>(true, "Users retrieved successfully", userRolesList);
                }
            }
        }



        public async Task<GeneralResponseData<List<string>>> GetUserRoles(string userEmail)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetUserRoles", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserEmail", userEmail);
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (!reader.HasRows)
                        return new GeneralResponseData<List<string>>(false, "User not found or no roles assigned", null);

                    var roles = new List<string>();
                    while (await reader.ReadAsync())
                    {
                        roles.Add(reader.GetString(0));
                    }

                    return new GeneralResponseData<List<string>>(true, "Roles retrieved successfully", roles);
                }
            }
        }

        public async Task<ServiceResponse.LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null) return new LoginResponse(false, null!, "Login container is empty");
            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);

            if (getUser is null) return new LoginResponse(false, null!, "User not Found");
            bool checkPassword = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);

            if (!checkPassword) return new LoginResponse(false, null!, "Invalid username/password");
            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());

            string token = GenerateJWTToken(userSession);
            return new LoginResponse(true, token, "User login Successfull");
        }

        private string GenerateJWTToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
            };
            var token = new JwtSecurityToken(
                issuer: config["JWT:Issuer"],
                audience: config["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
