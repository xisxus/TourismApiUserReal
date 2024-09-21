using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contacts
{
    public interface IPerson
    {
        Task<ServiceResponse.GeneralResponse> InsertPersonWithPicturesAsync(PersonDTO personDTO);
        Task<ServiceResponse.GeneralResponse> UpdatePersonWithPicturesAsync(int id, PersonDTO personDTO);
        Task<ServiceResponse.GeneralResponseData<PersonDTO>> GetPersonWithPicturesAsync(int personId);
    }
}
