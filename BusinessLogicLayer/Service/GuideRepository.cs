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
    public class GuideRepository : IGuide
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public GuideRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }

        public async Task<ServiceResponse.GeneralResponse> InsertGuide(GuideDTO guideDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertGuide1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GuideName", guideDTO.GuideName);
                    command.Parameters.AddWithValue("@GuideAddress", guideDTO.GuideAddress);
                    command.Parameters.AddWithValue("@GuideCode", guideDTO.GuideCode);
                    command.Parameters.AddWithValue("@ApplicationUserID", guideDTO.ApplicationUserID);

                    connection.Open();
                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        return new GeneralResponse(true, "Guide inserted successfully");
                    }
                    return new GeneralResponse(false, "Guide insertion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> UpdateGuide(int id, GuideDTO guideDTO)
        {
            if (id != guideDTO.GuideId)
            {
                return new GeneralResponse(false, "ID doesn't match");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdateGuide1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GuideId", guideDTO.GuideId);
                    command.Parameters.AddWithValue("@GuideName", guideDTO.GuideName);
                    command.Parameters.AddWithValue("@GuideAddress", guideDTO.GuideAddress);
                    command.Parameters.AddWithValue("@GuideCode", guideDTO.GuideCode);
                    command.Parameters.AddWithValue("@ApplicationUserID", guideDTO.ApplicationUserID);

                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Guide updated successfully");
                    }
                    return new GeneralResponse(false, "Guide update failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponse> DeleteGuide(int guideId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeleteGuide1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GuideId", guideId);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "Guide deleted successfully");
                    }
                    return new GeneralResponse(false, "Guide deletion failed");
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<List<GuideDTO>>> GetAllGuides()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllGuides1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    var guideList = new List<GuideDTO>();

                    while (await reader.ReadAsync())
                    {
                        var guide = new GuideDTO
                        {
                            GuideId = reader.GetInt32(0),
                            GuideName = reader.GetString(1),
                            GuideAddress = reader.GetString(2),
                            GuideCode = reader.GetString(3),
                            ApplicationUserID = reader.GetString(4),
                            CreatedAt = reader.GetDateTime(5),
                            UpdatedAt = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                            IsDeleted = reader.GetBoolean(7)
                        };
                        guideList.Add(guide);
                    }

                    return guideList.Count > 0
                        ? new GeneralResponseData<List<GuideDTO>>(true, "Guides retrieved successfully", guideList)
                        : new GeneralResponseData<List<GuideDTO>>(false, "No guides found", null);
                }
            }
        }

        public async Task<ServiceResponse.GeneralResponseData<GuideDTO>> GetGuideById(int guideId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetGuideById1", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GuideId", guideId);
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        var guide = new GuideDTO
                        {
                            GuideId = reader.GetInt32(0),
                            GuideName = reader.GetString(1),
                            GuideAddress = reader.GetString(2),
                            GuideCode = reader.GetString(3),
                            ApplicationUserID = reader.GetString(4),
                            CreatedAt = reader.GetDateTime(5),
                            UpdatedAt = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                            IsDeleted = reader.GetBoolean(7)
                        };
                        return new GeneralResponseData<GuideDTO>(true, "Guide retrieved successfully", guide);
                    }
                    return new GeneralResponseData<GuideDTO>(false, "Guide not found", null);
                }
            }
        }

       
    }
}
