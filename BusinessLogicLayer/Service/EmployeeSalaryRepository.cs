using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using DataAccessLayer.Entites.Employees;
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
    public class EmployeeSalaryRepository : IEmployeeSalary
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public EmployeeSalaryRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }

        public async Task<GeneralResponse> InsertEmployeeSalary(EmployeeSalariesDTO employeeSalariesDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertEmployeeSalary1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", employeeSalariesDTO.EmployeeId);
                    command.Parameters.AddWithValue("@EffectiveDate", employeeSalariesDTO.EffectiveDate);
                    command.Parameters.AddWithValue("@BaseSalary", employeeSalariesDTO.BaseSalary);
                    command.Parameters.AddWithValue("@Allowances", employeeSalariesDTO.Allowances);
                    command.Parameters.AddWithValue("@OvertimePay", employeeSalariesDTO.OvertimePay);
                    command.Parameters.AddWithValue("@Bonus", employeeSalariesDTO.Bonus);
                    command.Parameters.AddWithValue("@Deductions", employeeSalariesDTO.Deductions);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0
                        ? new GeneralResponse(true, "Employee salary details inserted successfully")
                        : new GeneralResponse(false, "Employee salary insertion failed");
                }
            }
        }

        public async Task<GeneralResponse> UpdateEmployeeSalary(int id, EmployeeSalariesDTO employeeSalariesDTO)
        {
            if (id != employeeSalariesDTO.SalaryId)
            {
                return new GeneralResponse(false, "ID doesn't match");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateEmployeeSalary1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SalaryId", employeeSalariesDTO.SalaryId);
                    command.Parameters.AddWithValue("@EmployeeId", employeeSalariesDTO.EmployeeId);
                    command.Parameters.AddWithValue("@EffectiveDate", employeeSalariesDTO.EffectiveDate);
                    command.Parameters.AddWithValue("@BaseSalary", employeeSalariesDTO.BaseSalary);
                    command.Parameters.AddWithValue("@Allowances", employeeSalariesDTO.Allowances);
                    command.Parameters.AddWithValue("@OvertimePay", employeeSalariesDTO.OvertimePay);
                    command.Parameters.AddWithValue("@Bonus", employeeSalariesDTO.Bonus);
                    command.Parameters.AddWithValue("@Deductions", employeeSalariesDTO.Deductions);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0
                        ? new GeneralResponse(true, "Employee salary details updated successfully")
                        : new GeneralResponse(false, "Employee salary update failed");
                }
            }
        }

        public async Task<GeneralResponse> DeleteEmployeeSalary(int EmployeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeleteEmployeeSalary1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0
                        ? new GeneralResponse(true, "Employee salary deleted successfully")
                        : new GeneralResponse(false, "Employee salary deletion failed");
                }
            }
        }

        public async Task<GeneralResponseData<List<EmployeeSalariesDTO>>> GetAllEmployeeSalaries()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllEmployeeSalaries1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    var employeeSalariesList = new List<EmployeeSalariesDTO>();

                    while (await reader.ReadAsync())
                    {
                        var employeeSalary = new EmployeeSalariesDTO
                        {
                            SalaryId = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            EffectiveDate = reader.GetDateTime(2),
                            BaseSalary = reader.GetDecimal(3),
                            Allowances = reader.IsDBNull(4) ? null : reader.GetDecimal(4),
                            OvertimePay = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                            Bonus = reader.IsDBNull(6) ? null : reader.GetDecimal(6),
                            Deductions = reader.IsDBNull(7) ? null : reader.GetDecimal(7),
                            CreatedAt = reader.GetDateTime(8),
                            UpdatedAt = reader.GetDateTime(9),
                            IsDeleted = reader.GetBoolean(10)
                        };
                        employeeSalariesList.Add(employeeSalary);
                    }

                    return employeeSalariesList.Count > 0
                        ? new GeneralResponseData<List<EmployeeSalariesDTO>>(true, "Employee salaries retrieved successfully", employeeSalariesList)
                        : new GeneralResponseData<List<EmployeeSalariesDTO>>(false, "No employee salaries found", null);
                }
            }
        }

        public async Task<GeneralResponseData<EmployeeSalariesDTO>> GetEmployeeSalaryById(int EmployeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetEmployeeSalaryById1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        var employeeSalary = new EmployeeSalariesDTO
                        {
                            SalaryId = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            EffectiveDate = reader.GetDateTime(2),
                            BaseSalary = reader.GetDecimal(3),
                            Allowances = reader.IsDBNull(4) ? null : reader.GetDecimal(4),
                            OvertimePay = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                            Bonus = reader.IsDBNull(6) ? null : reader.GetDecimal(6),
                            Deductions = reader.IsDBNull(7) ? null : reader.GetDecimal(7),
                            CreatedAt = reader.GetDateTime(8),
                            UpdatedAt = reader.GetDateTime(9),
                            IsDeleted = reader.GetBoolean(10)
                        };
                        return new GeneralResponseData<EmployeeSalariesDTO>(true, "Employee salary retrieved successfully", employeeSalary);
                    }
                    return new GeneralResponseData<EmployeeSalariesDTO>(false, "Employee salary not found", null);
                }
            }
        }
    }
}
