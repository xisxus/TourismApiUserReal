using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.ServiceResponse;

namespace BusinessLogicLayer.Service
{
    public class EmployeeRepository : IEmployee
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }

        public async Task<ServiceResponse.GeneralResponse> DeleteEmployee(int employeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeleteEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    connection.Open();

                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Employee deleted successfully");
                    }
                    return new GeneralResponse(false, "Employee deletion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<List<EmployeeDTO>>> GetALlEmployee()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetAllEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    var employeeList = new List<EmployeeDTO>();

                    while (await reader.ReadAsync())
                    {
                        var employee = new EmployeeDTO
                        {
                            EmployeeId = reader.GetInt32(0),
                            EmployeeCode = reader.GetString(1),
                            DateOfBirth = reader.GetDateTime(2),
                            JoiningDate = reader.GetDateTime(3),
                            MaritalStatus = reader.GetBoolean(4),
                            IdentityCard = reader.GetString(5),
                            DesignationId = reader.GetInt32(6),
                            DesignationTittle = reader.GetString(7),
                            DepartmentId = reader.GetInt32(8),
                            DeptName =reader.GetString(9),
                            ApplicationUserId = reader.GetString(10),
                        };
                        employeeList.Add(employee);
                    }

                    if (employeeList.Count > 0)
                    {
                        return new GeneralResponseData<List<EmployeeDTO>>(true, "Employees retrieved successfully", employeeList);
                    }
                    return new GeneralResponseData<List<EmployeeDTO>>(false, "No employees found", null);
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<EmployeeDTO>> GetEmployeeById(int employeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetEmployeeById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        var employee = new EmployeeDTO
                        {
                            EmployeeId = reader.GetInt32(0),
                            EmployeeCode = reader.GetString(1),
                            DateOfBirth = reader.GetDateTime(2),
                            JoiningDate = reader.GetDateTime(3),
                            MaritalStatus = reader.GetBoolean(4),
                            IdentityCard = reader.GetString(5),
                            DesignationId = reader.GetInt32(6),
                            DepartmentId = reader.GetInt32(7),
                            ApplicationUserId = reader.GetString(8),
                        };
                        return new GeneralResponseData<EmployeeDTO>(true, "Employee retrieved successfully", employee);
                    }
                    return new GeneralResponseData<EmployeeDTO>(false, "Employee not found", null);
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> InsertEmployee(EmployeeDTO employeeDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeCode", employeeDTO.EmployeeCode);
                    command.Parameters.AddWithValue("@DateOfBirth", employeeDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@JoiningDate", employeeDTO.JoiningDate);
                    command.Parameters.AddWithValue("@MaritalStatus", employeeDTO.MaritalStatus);
                    command.Parameters.AddWithValue("@IdentityCard", employeeDTO.IdentityCard);
                    command.Parameters.AddWithValue("@DesignationId", employeeDTO.DesignationId);
                    command.Parameters.AddWithValue("@DepartmentId", employeeDTO.DepartmentId);
                    command.Parameters.AddWithValue("@ApplicationUserId", employeeDTO.ApplicationUserId);
                    connection.Open();

                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Employee inserted successfully");
                    }
                    return new GeneralResponse(false, "Employee insertion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> UpdateEmployee( int id ,EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.EmployeeId)
            {
                return new GeneralResponse(false, "ID doesnt match");
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", employeeDTO.EmployeeId);
                    command.Parameters.AddWithValue("@EmployeeCode", employeeDTO.EmployeeCode);
                    command.Parameters.AddWithValue("@DateOfBirth", employeeDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@JoiningDate", employeeDTO.JoiningDate);
                    command.Parameters.AddWithValue("@MaritalStatus", employeeDTO.MaritalStatus);
                    command.Parameters.AddWithValue("@IdentityCard", employeeDTO.IdentityCard);
                    command.Parameters.AddWithValue("@DesignationId", employeeDTO.DesignationId);
                    command.Parameters.AddWithValue("@DepartmentId", employeeDTO.DepartmentId);
                    command.Parameters.AddWithValue("@ApplicationUserId", employeeDTO.ApplicationUserId);
                    connection.Open();

                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Employee updated successfully");
                    }
                    return new GeneralResponse(false, "Employee update failed");
                }
            }
        }
    }
}
