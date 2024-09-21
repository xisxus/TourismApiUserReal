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
    public class PersonRepository : IPerson
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public PersonRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("con");
        }

        // Insert Person with Pictures
        public async Task<ServiceResponse.GeneralResponse> InsertPersonWithPicturesAsync(PersonDTO personDTO)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("usp_InsertPersonWithPictures", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@FirstName", personDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", personDTO.LastName);
                    command.Parameters.AddWithValue("@Gender", personDTO.Gender);
                    command.Parameters.AddWithValue("@IdentityCard", personDTO.IdentityCard);
                    command.Parameters.AddWithValue("@PassportID", personDTO.PassportID);
                    command.Parameters.AddWithValue("@Mobile", personDTO.Mobile);
                    command.Parameters.AddWithValue("@Address", personDTO.Address);
                    command.Parameters.AddWithValue("@CityId", personDTO.CityId);
                    command.Parameters.AddWithValue("@ApplicationUserId", personDTO.ApplicationUserId);
                    command.Parameters.AddWithValue("@DateofBirth", personDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@PersonType", personDTO.PersonType);

                    var picturesTable = new DataTable();
                    picturesTable.Columns.Add("PictureUrl", typeof(string));
                    picturesTable.Columns.Add("Caption", typeof(string));
                    foreach (var picture in personDTO.PersonPictures)
                    {
                        picturesTable.Rows.Add(picture.PictureUrl, picture.Caption);
                    }
                    var picturesParam = command.Parameters.AddWithValue("@Pictures", picturesTable);
                    picturesParam.SqlDbType = SqlDbType.Structured;
                    picturesParam.TypeName = "dbo.PersonPicturesTableType";

                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();

                    return result > 0
                        ? new ServiceResponse.GeneralResponse(true, "Person inserted successfully")
                        : new ServiceResponse.GeneralResponse(false, "Person insertion failed");
                }
            }
        }

        // Update Person with Pictures
        public async Task<ServiceResponse.GeneralResponse> UpdatePersonWithPicturesAsync(int id, PersonDTO personDTO)
        {
            if (id != personDTO.PersonId)
            {
                return new ServiceResponse.GeneralResponse(false, "ID mismatch");
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("usp_UpdatePersonWithPictures", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonId", personDTO.PersonId);
                    command.Parameters.AddWithValue("@FirstName", personDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", personDTO.LastName);
                    command.Parameters.AddWithValue("@Gender", personDTO.Gender);
                    command.Parameters.AddWithValue("@IdentityCard", personDTO.IdentityCard);
                    command.Parameters.AddWithValue("@PassportID", personDTO.PassportID);
                    command.Parameters.AddWithValue("@Mobile", personDTO.Mobile);
                    command.Parameters.AddWithValue("@Address", personDTO.Address);
                    command.Parameters.AddWithValue("@CityId", personDTO.CityId);
                    command.Parameters.AddWithValue("@DateofBirth", personDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@PersonType", personDTO.PersonType);

                    var picturesTable = new DataTable();
                    picturesTable.Columns.Add("PictureUrl", typeof(string));
                    picturesTable.Columns.Add("Caption", typeof(string));
                    foreach (var picture in personDTO.PersonPictures)
                    {
                        picturesTable.Rows.Add(picture.PictureUrl, picture.Caption);
                    }
                    var picturesParam = command.Parameters.AddWithValue("@Pictures", picturesTable);
                    picturesParam.SqlDbType = SqlDbType.Structured;
                    picturesParam.TypeName = "dbo.PersonPicturesTableType";

                    connection.Open();
                    var result = await command.ExecuteNonQueryAsync();

                    return result > 0
                        ? new ServiceResponse.GeneralResponse(true, "Person updated successfully")
                        : new ServiceResponse.GeneralResponse(false, "Person update failed");
                }
            }
        }

        // Get Person with Pictures
        public async Task<ServiceResponse.GeneralResponseData<PersonDTO>> GetPersonWithPicturesAsync(int personId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("usp_GetPersonWithPictures", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonId", personId);

                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();

                    PersonDTO person = null;

                    if (await reader.ReadAsync())
                    {
                        person = new PersonDTO
                        {
                            PersonId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Gender = reader.GetString(3),
                            IdentityCard = reader.GetString(4),
                            PassportID = reader.GetString(5),
                            Mobile = reader.GetString(6),
                            Address = reader.GetString(7),
                            CityId = reader.GetInt32(8),
                            ApplicationUserId = reader.GetString(9),
                            DateOfBirth = reader.GetDateTime(10),
                            CreatedAt = reader.GetDateTime(11),
                            UpdatedAt = reader.GetDateTime(12),
                            PersonType = reader.GetString(13)
                        };
                    }

                    if (reader.NextResult())
                    {
                        while (await reader.ReadAsync())
                        {
                            person.PersonPictures.Add(new PersonPictureDTO
                            {
                                PictureId = reader.GetInt32(0),
                                PictureUrl = reader.GetString(1),
                                Caption = reader.GetString(2)
                            });
                        }
                    }

                    return person != null
                        ? new ServiceResponse.GeneralResponseData<PersonDTO>(true, "Person retrieved successfully", person)
                        : new ServiceResponse.GeneralResponseData<PersonDTO>(false, "Person not found", null);
                }
            }
        }
    }
}
