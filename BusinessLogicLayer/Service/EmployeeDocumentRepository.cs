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

namespace BusinessLogicLayer.Service
{
    public class EmployeeDocumentRepository : IEmployeeDocument
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public EmployeeDocumentRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }

        public async Task InsertEmployeeDocument(EmployeeDocumentDTO employeeDocumentDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertEmployeeDocument", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", employeeDocumentDTO.EmployeeId);
                    command.Parameters.AddWithValue("@DocumentUrl", employeeDocumentDTO.DocumentUrl);
                    command.Parameters.AddWithValue("@DocumentType", employeeDocumentDTO.DocumentType);
                    command.Parameters.AddWithValue("@Description", employeeDocumentDTO.Description);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@IsDeleted", false);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<EmployeeDocumentDTO>> GetAllEmployeeDocuments(int employeeId)
        {
            var documents = new List<EmployeeDocumentDTO>();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllEmployeeDocuments", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    connection.Open();

                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        documents.Add(new EmployeeDocumentDTO
                        {
                            DocumentId = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            DocumentUrl = reader.GetString(2),
                            DocumentType = reader.GetString(3),
                            Description = reader.GetString(4),
                            CreatedAt = reader.GetDateTime(5),
                            UpdatedAt = reader.GetDateTime(6),
                            IsDeleted = reader.GetBoolean(7),
                        });
                    }
                }
            }
            return documents;
        }
    }
}
