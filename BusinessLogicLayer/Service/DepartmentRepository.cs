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
    public class DepartmentRepository : IDepartment
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DepartmentRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }

        public async Task<ServiceResponse.GeneralResponse> InsertDepartment(DepartmentDTO departmentDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertDepartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DeptName", departmentDTO.DeptName);
                    command.Parameters.AddWithValue("@Description", departmentDTO.Description);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Department inserted successfully");
                    }
                    return new GeneralResponse(false, "Department insertion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> UpdateDepartment(int id, DepartmentDTO departmentDTO)
        {
            if (id != departmentDTO.DepartmentId)
            {
                return new GeneralResponse(false, "ID doesn't match");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateDepartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DepartmentId", departmentDTO.DepartmentId);
                    command.Parameters.AddWithValue("@DeptName", departmentDTO.DeptName);
                    command.Parameters.AddWithValue("@Description", departmentDTO.Description);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Department updated successfully");
                    }
                    return new GeneralResponse(false, "Department update failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> DeleteDepartment(int departmentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeleteDepartment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Department deleted successfully");
                    }
                    return new GeneralResponse(false, "Department deletion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<List<DepartmentDTO>>> GetAllDepartments()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllDepartments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    var departmentList = new List<DepartmentDTO>();

                    while (await reader.ReadAsync())
                    {
                        var department = new DepartmentDTO
                        {
                            DepartmentId = reader.GetInt32(0),
                            DeptName = reader.GetString(1),
                            Description = reader.GetString(2)
                        };
                        departmentList.Add(department);
                    }

                    if (departmentList.Count > 0)
                    {
                        return new GeneralResponseData<List<DepartmentDTO>>(true, "Departments retrieved successfully", departmentList);
                    }
                    return new GeneralResponseData<List<DepartmentDTO>>(false, "No departments found", null);
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<DepartmentDTO>> GetDepartmentById(int departmentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetDepartmentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        var department = new DepartmentDTO
                        {
                            DepartmentId = reader.GetInt32(0),
                            DeptName = reader.GetString(1),
                            Description = reader.GetString(2)
                        };
                        return new GeneralResponseData<DepartmentDTO>(true, "Department retrieved successfully", department);
                    }
                    return new GeneralResponseData<DepartmentDTO>(false, "Department not found", null);
                }
            }
        }

    }
}
