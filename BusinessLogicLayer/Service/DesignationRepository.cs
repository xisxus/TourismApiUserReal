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
    public class DesignationRepository : IDesignation
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DesignationRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }

        public async Task<ServiceResponse.GeneralResponse> InsertDesignation(DesignationDTO designationDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertDesignation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DesignationTitle", designationDTO.DesignationTitle);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Designation inserted successfully");
                    }
                    return new GeneralResponse(false, "Designation insertion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> UpdateDesignation(int id, DesignationDTO designationDTO)
        {
            if (id != designationDTO.DesignationId)
            {
                return new GeneralResponse(false, "ID doesn't match");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateDesignation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DesignationId", designationDTO.DesignationId);
                    command.Parameters.AddWithValue("@DesignationTitle", designationDTO.DesignationTitle);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Designation updated successfully");
                    }
                    return new GeneralResponse(false, "Designation update failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> DeleteDesignation(int designationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeleteDesignation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DesignationId", designationId);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Designation deleted successfully");
                    }
                    return new GeneralResponse(false, "Designation deletion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<List<DesignationDTO>>> GetAllDesignations()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllDesignations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    var designationList = new List<DesignationDTO>();

                    while (await reader.ReadAsync())
                    {
                        var designation = new DesignationDTO
                        {
                            DesignationId = reader.GetInt32(0),
                            DesignationTitle = reader.GetString(1)
                        };
                        designationList.Add(designation);
                    }

                    if (designationList.Count > 0)
                    {
                        return new GeneralResponseData<List<DesignationDTO>>(true, "Designations retrieved successfully", designationList);
                    }
                    return new GeneralResponseData<List<DesignationDTO>>(false, "No designations found", null);
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<DesignationDTO>> GetDesignationById(int designationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetDesignationById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DesignationId", designationId);
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        var designation = new DesignationDTO
                        {
                            DesignationId = reader.GetInt32(0),
                            DesignationTitle = reader.GetString(1)
                        };
                        return new GeneralResponseData<DesignationDTO>(true, "Designation retrieved successfully", designation);
                    }
                    return new GeneralResponseData<DesignationDTO>(false, "Designation not found", null);
                }
            }
        }
    }
}
