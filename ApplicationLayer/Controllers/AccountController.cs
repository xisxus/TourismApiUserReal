
using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IUserAccount userAccount) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var response = await userAccount.CreateAccount(userDTO);
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await userAccount.LoginAccount(loginDTO);
            return Ok(response);
        }

        [HttpPost("CreateRole")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var response = await userAccount.CreateRole(roleName);
            return Ok(response);
        }


        [HttpPost("AssignRole")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRoleToUser(string userEmail, string roleName)
        {
            var response = await userAccount.AssignRoleToUser(userEmail, roleName);
            return Ok(response);
        }

        [HttpPost("UnAssignRole")]
        public async Task<IActionResult> UnassignRoleFromUser(string userEmail, string roleName)
        {
            var response = await userAccount.UnassignRoleFromUser(userEmail, roleName);
            return Ok(response);
        }

        [HttpGet("GetUserRoles/{userEmail}")]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserRoles(string userEmail)
        {
            var response = await userAccount.GetUserRoles(userEmail);
            return Ok(response);
        }

        [HttpGet("GetAllUsersWithRoles")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            var response = await userAccount.GetAllUsersWithRoles();
            return Ok(response);
        }
    }

   
}
