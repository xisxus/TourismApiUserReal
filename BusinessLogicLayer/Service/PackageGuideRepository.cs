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
    public class PackageGuideRepository : IPackageGuide
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public PackageGuideRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }

        public async Task<GeneralResponse> InsertPackageGuide(PackageGuideDTO packageGuideDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertPackageGuide", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PackageId", packageGuideDTO.PackageId);
                    command.Parameters.AddWithValue("@GuideId", packageGuideDTO.GuideId);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "PackageGuide inserted successfully");
                    }
                    return new GeneralResponse(false, "Insertion failed");
                }
            }
        }

        public async Task<GeneralResponse> UpdatePackageGuide(int id, PackageGuideDTO packageGuideDTO)
        {
            if (id != packageGuideDTO.PackageGuideId)
            {
                return new GeneralResponse(false, "ID mismatch");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_UpdatePackageGuide", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PackageGuideId", packageGuideDTO.PackageGuideId);
                    command.Parameters.AddWithValue("@PackageId", packageGuideDTO.PackageId);
                    command.Parameters.AddWithValue("@GuideId", packageGuideDTO.GuideId);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "PackageGuide updated successfully");
                    }
                    return new GeneralResponse(false, "Update failed");
                }
            }
        }

        public async Task<GeneralResponse> DeletePackageGuide(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_DeletePackageGuide", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PackageGuideId", id);
                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();
                    if (result > 0)
                    {
                        return new GeneralResponse(true, "PackageGuide deleted successfully");
                    }
                    return new GeneralResponse(false, "Deletion failed");
                }
            }
        }

        public async Task<GeneralResponseData<List<PackageGuideDTO>>> GetAllPackageGuides()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetAllPackageGuides", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    var packageGuides = new List<PackageGuideDTO>();

                    while (await reader.ReadAsync())
                    {
                        packageGuides.Add(new PackageGuideDTO
                        {
                            PackageGuideId = reader.GetInt32(0),
                            PackageId = reader.GetInt32(1),
                            GuideId = reader.GetInt32(2)
                        });
                    }
                    return new GeneralResponseData<List<PackageGuideDTO>>(true, "Success", packageGuides);
                }
            }
        }

        public async Task<GeneralResponseData<List<PackageGuideDTO>>> GetPackageGuideByGuideId(int guideId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_GetPackageGuideByGuideId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@GuideId", guideId);
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    var packageGuides = new List<PackageGuideDTO>();

                    while (await reader.ReadAsync())
                    {
                        var packageGuide = new PackageGuideDTO
                        {
                            PackageGuideId = reader.GetInt32(0),
                            PackageId = reader.GetInt32(1),
                            GuideId = reader.GetInt32(2)
                        };
                        packageGuides.Add(packageGuide);
                    }

                    if (packageGuides.Any())
                    {
                        return new GeneralResponseData<List<PackageGuideDTO>>(true, "Success", packageGuides);
                    }
                    return new GeneralResponseData<List<PackageGuideDTO>>(false, "No PackageGuides found for this GuideId", null);
                }
            }
        }

    }
}
