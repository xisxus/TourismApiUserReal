using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using DataAccessLayer.Entites.Employees;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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

        public async Task<ServiceResponse.GeneralResponseData<List<EmpDTO>>> GetAllFullEmployee()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_GetAllEmployeeFullDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    var employeeDictionary = new Dictionary<int, EmpDTO>();

                    while (await reader.ReadAsync())
                    {
                        var employeeId = reader.GetInt32(0); // Get EmployeeId (0)

                        // Check if the employee already exists in the dictionary
                        if (!employeeDictionary.TryGetValue(employeeId, out var existingEmployee))
                        {
                            // Create a new employee if not found
                            existingEmployee = new EmpDTO
                            {
                                EmployeeId = employeeId,
                                EmployeeCode = reader.GetString(1),
                                DateOfBirth = reader.GetDateTime(2),
                                JoiningDate = reader.GetDateTime(3),
                                MaritalStatus = reader.GetBoolean(4),
                                IdentityCard = reader.GetString(5),
                                DesignationId = reader.GetInt32(13),
                                DesignationTittle = reader.GetString(14),
                                DepartmentId = reader.GetInt32(10),
                                DeptName = reader.GetString(11),
                                ApplicationUserId = reader.GetString(6),
                                CreatedAt = reader.GetDateTime(7),
                                UpdatedAt = reader.GetDateTime(8),
                                DepartmentDescription = reader.IsDBNull(12) ? null : reader.GetString(12),
                                isDeleted = reader.GetBoolean(9),
                                EmployeeBanks = new List<EmployeeBankDTO>(),
                                EmployeeDocuments = new List<EmployeeDocumentDTO>()
                            };

                            // Add the new employee to the dictionary
                            employeeDictionary[employeeId] = existingEmployee;
                        }

                        // Add bank details if available
                        if (!reader.IsDBNull(15))
                        {
                            var bankDetails = new EmployeeBankDTO
                            {
                                EmployeeBankID = reader.GetInt32(15),
                                BankName = reader.GetString(16),
                                AccountNumber = reader.GetString(17),
                                BranchName = reader.GetString(18),
                                AccountType = reader.GetString(19)
                            };

                            // Ensure bank details are not duplicated
                            if (!existingEmployee.EmployeeBanks.Any(b => b.EmployeeBankID == bankDetails.EmployeeBankID))
                            {
                                existingEmployee.EmployeeBanks.Add(bankDetails);
                            }
                        }

                        // Add document details if available
                        if (!reader.IsDBNull(20))
                        {
                            var documentDetails = new EmployeeDocumentDTO
                            {
                                DocumentId = reader.GetInt32(20),
                                DocumentUrl = reader.GetString(21),
                                DocumentType = reader.GetString(22),
                                UploadDate = reader.GetDateTime(23),
                                DocumentDescription = reader.GetString(24)
                            };

                            // Ensure document details are not duplicated
                            if (!existingEmployee.EmployeeDocuments.Any(d => d.DocumentId == documentDetails.DocumentId))
                            {
                                existingEmployee.EmployeeDocuments.Add(documentDetails);
                            }
                        }
                    }

                    var employeeList = employeeDictionary.Values.ToList();

                    if (employeeList.Count > 0)
                    {
                        return new ServiceResponse.GeneralResponseData<List<EmpDTO>>(true, "Employees retrieved successfully", employeeList);
                    }
                    return new ServiceResponse.GeneralResponseData<List<EmpDTO>>(false, "No employees found", null);
                }
            }
        }


        public async Task<ServiceResponse.GeneralResponseData<List<EmpDTO>>> GetAllFullEmployeeById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_GetEmployeeFullDetailsById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", id);
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    var employeeDictionary = new Dictionary<int, EmpDTO>();

                    while (await reader.ReadAsync())
                    {
                        var employeeId = reader.GetInt32(0); // EmployeeId (0)

                        // Check if the employee already exists in the dictionary
                        if (!employeeDictionary.TryGetValue(employeeId, out var existingEmployee))
                        {
                            // Create a new employee if not found
                            existingEmployee = new EmpDTO
                            {
                                EmployeeId = employeeId,
                                EmployeeCode = reader.GetString(1),
                                DateOfBirth = reader.GetDateTime(2),
                                JoiningDate = reader.GetDateTime(3),
                                MaritalStatus = reader.GetBoolean(4),
                                IdentityCard = reader.GetString(5),
                                DesignationId = reader.GetInt32(13),
                                DesignationTittle = reader.GetString(14),
                                DepartmentId = reader.GetInt32(10),
                                DeptName = reader.GetString(11),
                                ApplicationUserId = reader.GetString(6),
                                CreatedAt = reader.GetDateTime(7),
                                UpdatedAt = reader.GetDateTime(8),
                                DepartmentDescription = reader.IsDBNull(12) ? null : reader.GetString(12),
                                isDeleted = reader.GetBoolean(9),
                                EmployeeBanks = new List<EmployeeBankDTO>(),
                                EmployeeDocuments = new List<EmployeeDocumentDTO>()
                            };

                            // Add the new employee to the dictionary
                            employeeDictionary[employeeId] = existingEmployee;
                        }

                        // Add bank details if available
                        if (!reader.IsDBNull(15))
                        {
                            var bankDetails = new EmployeeBankDTO
                            {
                                EmployeeBankID = reader.GetInt32(15),
                                BankName = reader.GetString(16),
                                AccountNumber = reader.GetString(17),
                                BranchName = reader.GetString(18),
                                AccountType = reader.GetString(19)
                            };

                            // Ensure bank details are not duplicated
                            if (!existingEmployee.EmployeeBanks.Any(b => b.EmployeeBankID == bankDetails.EmployeeBankID))
                            {
                                existingEmployee.EmployeeBanks.Add(bankDetails);
                            }
                        }

                        // Add document details if available
                        if (!reader.IsDBNull(20))
                        {
                            var documentDetails = new EmployeeDocumentDTO
                            {
                                DocumentId = reader.GetInt32(20),
                                DocumentUrl = reader.GetString(21),
                                DocumentType = reader.GetString(22),
                                UploadDate = reader.GetDateTime(23),
                                DocumentDescription = reader.GetString(24)
                            };

                            // Ensure document details are not duplicated
                            if (!existingEmployee.EmployeeDocuments.Any(d => d.DocumentId == documentDetails.DocumentId))
                            {
                                existingEmployee.EmployeeDocuments.Add(documentDetails);
                            }
                        }
                    }

                    var employeeList = employeeDictionary.Values.ToList();

                    if (employeeList.Count > 0)
                    {
                        return new ServiceResponse.GeneralResponseData<List<EmpDTO>>(true, "Employees retrieved successfully", employeeList);
                    }
                    return new ServiceResponse.GeneralResponseData<List<EmpDTO>>(false, "No employees found", null);
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


        public async Task<ServiceResponse.GeneralResponse> CreateEmployee(EmployeeCreateDTO employeeDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("InsertEmployeeFull", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Employee information
                    command.Parameters.AddWithValue("@EmployeeCode", employeeDTO.EmployeeCode);
                    command.Parameters.AddWithValue("@DateOfBirth", employeeDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@JoiningDate", employeeDTO.JoiningDate);
                    command.Parameters.AddWithValue("@MaritalStatus", employeeDTO.MaritalStatus);
                    command.Parameters.AddWithValue("@IdentityCard", employeeDTO.IdentityCard);
                    command.Parameters.AddWithValue("@DesignationId", employeeDTO.DesignationId);
                    command.Parameters.AddWithValue("@DepartmentId", employeeDTO.DepartmentId);
                    command.Parameters.AddWithValue("@ApplicationUserId", employeeDTO.ApplicationUserId);

                    // EmpDepts
                    var empDeptsTable = new DataTable();
                    empDeptsTable.Columns.Add("DepartmentId", typeof(int));
                    foreach (var dept in employeeDTO.EmpDepts)
                    {
                        empDeptsTable.Rows.Add(dept.DepartmentId);
                    }
                    var empDeptsParam = command.Parameters.AddWithValue("@EmpDepts", empDeptsTable);
                    empDeptsParam.SqlDbType = SqlDbType.Structured;
                    empDeptsParam.TypeName = "dbo.EmpDeptsType";

                    // EmployeeBanks
                    var employeeBanksTable = new DataTable();
                    employeeBanksTable.Columns.Add("BankName", typeof(string));
                    employeeBanksTable.Columns.Add("AccountNumber", typeof(string));
                    employeeBanksTable.Columns.Add("BranchName", typeof(string));
                    employeeBanksTable.Columns.Add("AccountType", typeof(string));
                    foreach (var bank in employeeDTO.EmployeeBanks)
                    {
                        employeeBanksTable.Rows.Add(bank.BankName, bank.AccountNumber, bank.BranchName, bank.AccountType);
                    }
                    var employeeBanksParam = command.Parameters.AddWithValue("@EmployeeBank", employeeBanksTable);
                    employeeBanksParam.SqlDbType = SqlDbType.Structured;
                    employeeBanksParam.TypeName = "dbo.EmployeeBankType";

                    // EmployeeDocuments
                    var employeeDocumentsTable = new DataTable();
                    employeeDocumentsTable.Columns.Add("DocumentUrl", typeof(string));
                    employeeDocumentsTable.Columns.Add("DocumentType", typeof(string));
                    employeeDocumentsTable.Columns.Add("UploadDate", typeof(DateTime));
                    employeeDocumentsTable.Columns.Add("Description", typeof(string));
                    foreach (var doc in employeeDTO.EmployeeDocuments)
                    {
                        employeeDocumentsTable.Rows.Add(doc.DocumentUrl, doc.DocumentType, doc.UploadDate, doc.Description);
                    }
                    var employeeDocumentsParam = command.Parameters.AddWithValue("@EmployeeDocuments", employeeDocumentsTable);
                    employeeDocumentsParam.SqlDbType = SqlDbType.Structured;
                    employeeDocumentsParam.TypeName = "dbo.EmployeeDocumentsType";

                    // EmployeeSalaries
                    var employeeSalariesTable = new DataTable();
                    employeeSalariesTable.Columns.Add("EffectiveDate", typeof(DateTime));
                    employeeSalariesTable.Columns.Add("BaseSalary", typeof(decimal));
                    employeeSalariesTable.Columns.Add("Allowances", typeof(decimal));
                    employeeSalariesTable.Columns.Add("OvertimePay", typeof(decimal));
                    employeeSalariesTable.Columns.Add("Bonus", typeof(decimal));
                    employeeSalariesTable.Columns.Add("Deductions", typeof(decimal));
                    foreach (var salary in employeeDTO.EmployeeSalaries)
                    {
                        employeeSalariesTable.Rows.Add(salary.EffectiveDate, salary.BaseSalary, salary.Allowances, salary.OvertimePay, salary.Bonus, salary.Deductions);
                    }
                    var employeeSalariesParam = command.Parameters.AddWithValue("@EmployeeSalaries", employeeSalariesTable);
                    employeeSalariesParam.SqlDbType = SqlDbType.Structured;
                    employeeSalariesParam.TypeName = "dbo.EmployeeSalariesType";

                    // Execute the command
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result < 0)
                    {
                        return new GeneralResponse(true, "Employee inserted successfully");
                    }
                    return new GeneralResponse(false, "Employee insertion failed");
                }
            }
        }


        public async Task<ServiceResponse.GeneralResponse> EditEmployee(EmployeeEditDTO employeeDTO, int id)
        {
            if (id != employeeDTO.EmployeeId)
            {
                return new GeneralResponse(false, "ID misMatch");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("UpdateEmployeeFull", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Employee information
                    command.Parameters.AddWithValue("@EmployeeId", employeeDTO.EmployeeId);
                    command.Parameters.AddWithValue("@EmployeeCode", employeeDTO.EmployeeCode);
                    command.Parameters.AddWithValue("@DateOfBirth", employeeDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@JoiningDate", employeeDTO.JoiningDate);
                    command.Parameters.AddWithValue("@MaritalStatus", employeeDTO.MaritalStatus);
                    command.Parameters.AddWithValue("@IdentityCard", employeeDTO.IdentityCard);
                    command.Parameters.AddWithValue("@DesignationId", employeeDTO.DesignationId);
                    command.Parameters.AddWithValue("@DepartmentId", employeeDTO.DepartmentId);
                    command.Parameters.AddWithValue("@ApplicationUserId", employeeDTO.ApplicationUserId);

                    // EmpDepts
                    var empDeptsTable = new DataTable();
                    empDeptsTable.Columns.Add("DepartmentId", typeof(int));
                    foreach (var dept in employeeDTO.EmpDepts)
                    {
                        empDeptsTable.Rows.Add(dept.DepartmentId);
                    }
                    var empDeptsParam = command.Parameters.AddWithValue("@EmpDepts", empDeptsTable);
                    empDeptsParam.SqlDbType = SqlDbType.Structured;
                    empDeptsParam.TypeName = "dbo.EmpDeptsType";

                    // EmployeeBanks
                    var employeeBanksTable = new DataTable();
                    employeeBanksTable.Columns.Add("BankName", typeof(string));
                    employeeBanksTable.Columns.Add("AccountNumber", typeof(string));
                    employeeBanksTable.Columns.Add("BranchName", typeof(string));
                    employeeBanksTable.Columns.Add("AccountType", typeof(string));
                    foreach (var bank in employeeDTO.EmployeeBanks)
                    {
                        employeeBanksTable.Rows.Add(bank.BankName, bank.AccountNumber, bank.BranchName, bank.AccountType);
                    }
                    var employeeBanksParam = command.Parameters.AddWithValue("@EmployeeBank", employeeBanksTable);
                    employeeBanksParam.SqlDbType = SqlDbType.Structured;
                    employeeBanksParam.TypeName = "dbo.EmployeeBankType";

                    // EmployeeDocuments
                    var employeeDocumentsTable = new DataTable();
                    employeeDocumentsTable.Columns.Add("DocumentUrl", typeof(string));
                    employeeDocumentsTable.Columns.Add("DocumentType", typeof(string));
                    employeeDocumentsTable.Columns.Add("UploadDate", typeof(DateTime));
                    employeeDocumentsTable.Columns.Add("Description", typeof(string));
                    foreach (var doc in employeeDTO.EmployeeDocuments)
                    {
                        employeeDocumentsTable.Rows.Add(doc.DocumentUrl, doc.DocumentType, doc.UploadDate, doc.Description);
                    }
                    var employeeDocumentsParam = command.Parameters.AddWithValue("@EmployeeDocuments", employeeDocumentsTable);
                    employeeDocumentsParam.SqlDbType = SqlDbType.Structured;
                    employeeDocumentsParam.TypeName = "dbo.EmployeeDocumentsType";

                    // EmployeeSalaries
                    var employeeSalariesTable = new DataTable();
                    employeeSalariesTable.Columns.Add("EffectiveDate", typeof(DateTime));
                    employeeSalariesTable.Columns.Add("BaseSalary", typeof(decimal));
                    employeeSalariesTable.Columns.Add("Allowances", typeof(decimal));
                    employeeSalariesTable.Columns.Add("OvertimePay", typeof(decimal));
                    employeeSalariesTable.Columns.Add("Bonus", typeof(decimal));
                    employeeSalariesTable.Columns.Add("Deductions", typeof(decimal));
                    foreach (var salary in employeeDTO.EmployeeSalaries)
                    {
                        employeeSalariesTable.Rows.Add(salary.EffectiveDate, salary.BaseSalary, salary.Allowances, salary.OvertimePay, salary.Bonus, salary.Deductions);
                    }
                    var employeeSalariesParam = command.Parameters.AddWithValue("@EmployeeSalaries", employeeSalariesTable);
                    employeeSalariesParam.SqlDbType = SqlDbType.Structured;
                    employeeSalariesParam.TypeName = "dbo.EmployeeSalariesType";

                    // Execute the stored procedure
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return new ServiceResponse.GeneralResponse(true, "Updated Successfull");
           
        }


    }
}
