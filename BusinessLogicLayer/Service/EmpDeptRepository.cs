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
    public class EmpDeptRepository : IEmpDept
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public EmpDeptRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }

        public async Task<ServiceResponse.GeneralResponse> InsertEmpDept(EmpDeptDTO empDeptDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertEmpDept", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", empDeptDTO.EmployeeId);
                    command.Parameters.AddWithValue("@DepartmentId", empDeptDTO.DepartmentId);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "EmpDept inserted successfully");
                    }
                    return new GeneralResponse(false, "EmpDept insertion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> UpdateEmpDept(int id, EmpDeptDTO empDeptDTO)
        {
            if (id != empDeptDTO.EmpDeptId)
            {
                return new GeneralResponse(false, "ID doesn't match");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateEmpDept", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpDeptId", empDeptDTO.EmpDeptId);
                    command.Parameters.AddWithValue("@EmployeeId", empDeptDTO.EmployeeId);
                    command.Parameters.AddWithValue("@DepartmentId", empDeptDTO.DepartmentId);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "EmpDept updated successfully");
                    }
                    return new GeneralResponse(false, "EmpDept update failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> DeleteEmpDept(int empDeptId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeleteEmpDept", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpDeptId", empDeptId);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "EmpDept deleted successfully");
                    }
                    return new GeneralResponse(false, "EmpDept deletion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<List<EmpDeptDTO>>> GetAllEmpDepts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllEmpDepts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    var empDeptList = new List<EmpDeptDTO>();

                    while (await reader.ReadAsync())
                    {
                        var empDept = new EmpDeptDTO
                        {
                            EmpDeptId = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            DepartmentId = reader.GetInt32(2),
                            CreatedAt = reader.GetDateTime(3),
                            UpdatedAt = reader.GetDateTime(4)
                        };
                        empDeptList.Add(empDept);
                    }

                    if (empDeptList.Count > 0)
                    {
                        return new GeneralResponseData<List<EmpDeptDTO>>(true, "EmpDepts retrieved successfully", empDeptList);
                    }
                    return new GeneralResponseData<List<EmpDeptDTO>>(false, "No EmpDepts found", null);
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<EmpDeptDTO>> GetEmpDeptById(int empDeptId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetEmpDeptById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpDeptId", empDeptId);
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        var empDept = new EmpDeptDTO
                        {
                            EmpDeptId = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            DepartmentId = reader.GetInt32(2),
                            CreatedAt = reader.GetDateTime(3),
                            UpdatedAt = reader.GetDateTime(4)
                        };
                        return new GeneralResponseData<EmpDeptDTO>(true, "EmpDept retrieved successfully", empDept);
                    }
                    return new GeneralResponseData<EmpDeptDTO>(false, "EmpDept not found", null);
                }
            }
        }
    }
}
