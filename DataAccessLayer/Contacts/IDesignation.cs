using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contacts
{
    public interface IDesignation
    {
        Task<ServiceResponse.GeneralResponse> InsertDesignation(DesignationDTO designationDTO);
        Task<ServiceResponse.GeneralResponse> UpdateDesignation(int id, DesignationDTO designationDTO);
        Task<ServiceResponse.GeneralResponse> DeleteDesignation(int designationId);
        Task<ServiceResponse.GeneralResponseData<List<DesignationDTO>>> GetAllDesignations();
        Task<ServiceResponse.GeneralResponseData<DesignationDTO>> GetDesignationById(int designationId);
    }
}
