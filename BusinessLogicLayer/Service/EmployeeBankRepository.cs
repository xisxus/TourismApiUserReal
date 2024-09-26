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
    public class EmployeeBankRepository : IEmployeeBank
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public EmployeeBankRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }


        public async Task<ServiceResponse.GeneralResponseData<List<EmployeeBankDTO>>> InsertEmployeeBanks(List<EmployeeBankDTO> employeeBankDTOs)
        {
            var insertedBanks = new List<EmployeeBankDTO>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                foreach (var employeeBankDTO in employeeBankDTOs)
                {
                    using (var command = new SqlCommand("SP_InsertEmployeeBank1", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmployeeID", employeeBankDTO.EmployeeID);
                        command.Parameters.AddWithValue("@BankName", employeeBankDTO.BankName);
                        command.Parameters.AddWithValue("@AccountNumber", employeeBankDTO.AccountNumber);
                        command.Parameters.AddWithValue("@BranchName", employeeBankDTO.BranchName);
                        command.Parameters.AddWithValue("@AccountType", employeeBankDTO.AccountType);

                        var result = await command.ExecuteNonQueryAsync();

                        if (result > 0)
                        {
                            // If insertion is successful, add to the list
                            insertedBanks.Add(employeeBankDTO);
                        }
                        else
                        {
                            // Handle failure for individual insertions if necessary
                            return new ServiceResponse.GeneralResponseData<List<EmployeeBankDTO>>(false, "nourl", null);
                        }
                    }
                }
            }

            return new ServiceResponse.GeneralResponseData<List<EmployeeBankDTO>>(true, "url", insertedBanks);
        }




        public async Task<GeneralResponseSingle> InsertEmployeeBank(EmployeeBankDTO employeeBankDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertEmployeeBank1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", employeeBankDTO.EmployeeID);
                    command.Parameters.AddWithValue("@BankName", employeeBankDTO.BankName);
                    command.Parameters.AddWithValue("@AccountNumber", employeeBankDTO.AccountNumber);
                    command.Parameters.AddWithValue("@BranchName", employeeBankDTO.BranchName);
                    command.Parameters.AddWithValue("@AccountType", employeeBankDTO.AccountType);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0
                        ? new GeneralResponseSingle(true, "Url", employeeBankDTO.EmployeeID)
                        : new GeneralResponseSingle(false, "Employee bank insertion failed");
                }
            }
        }

        public async Task<GeneralResponse> UpdateEmployeeBank(int id, EmployeeBankDTO employeeBankDTO)
        {
            if (id != employeeBankDTO.EmployeeBankID)
            {
                return new GeneralResponse(false, "ID doesn't match");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateEmployeeBank1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeBankID", employeeBankDTO.EmployeeBankID);
                    command.Parameters.AddWithValue("@EmployeeID", employeeBankDTO.EmployeeID);
                    command.Parameters.AddWithValue("@BankName", employeeBankDTO.BankName);
                    command.Parameters.AddWithValue("@AccountNumber", employeeBankDTO.AccountNumber);
                    command.Parameters.AddWithValue("@BranchName", employeeBankDTO.BranchName);
                    command.Parameters.AddWithValue("@AccountType", employeeBankDTO.AccountType);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0
                        ? new GeneralResponse(true, "url")
                        : new GeneralResponse(false, "Employee bank update failed");
                }
            }
        }

        public async Task<GeneralResponse> DeleteEmployeeBank(int employeeID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeleteEmployeeBank1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    return result > 0
                        ? new GeneralResponse(true, "Employee bank deleted successfully")
                        : new GeneralResponse(false, "Employee bank deletion failed");
                }
            }
        }

        public async Task<GeneralResponseData<List<EmployeeBankDTO>>> GetAllEmployeeBanks()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllEmployeeBanks1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    var employeeBankList = new List<EmployeeBankDTO>();

                    while (await reader.ReadAsync())
                    {
                        var employeeBank = new EmployeeBankDTO
                        {
                            EmployeeBankID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            BankName = reader.GetString(2),
                            AccountNumber = reader.GetString(3),
                            BranchName = reader.GetString(4),
                            AccountType = reader.GetString(5),
                            CreatedAt = reader.GetDateTime(6),
                            UpdatedAt = reader.GetDateTime(7),
                            IsDeleted = reader.GetBoolean(8)
                        };
                        employeeBankList.Add(employeeBank);
                    }

                    return employeeBankList.Count > 0
                        ? new GeneralResponseData<List<EmployeeBankDTO>>(true, "Employee banks retrieved successfully", employeeBankList)
                        : new GeneralResponseData<List<EmployeeBankDTO>>(false, "No employee banks found", null);
                }
            }
        }

        public async Task<GeneralResponseData<EmployeeBankDTO>> GetEmployeeBankById(int employeeID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetEmployeeBankById1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        var employeeBank = new EmployeeBankDTO
                        {
                            EmployeeBankID = reader.GetInt32(0),
                            EmployeeID = reader.GetInt32(1),
                            BankName = reader.GetString(2),
                            AccountNumber = reader.GetString(3),
                            BranchName = reader.GetString(4),
                            AccountType = reader.GetString(5),
                            CreatedAt = reader.GetDateTime(6),
                            UpdatedAt = reader.GetDateTime(7),
                            IsDeleted = reader.GetBoolean(8)
                        };
                        return new GeneralResponseData<EmployeeBankDTO>(true, "Employee bank retrieved successfully", employeeBank);
                    }
                    return new GeneralResponseData<EmployeeBankDTO>(false, "Employee bank not found", null);
                }
            }
        }
    }
}
