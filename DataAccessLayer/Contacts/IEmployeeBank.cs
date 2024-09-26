using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.ServiceResponse;

namespace DataAccessLayer.Contacts
{
    public interface IEmployeeBank
    {
        Task<ServiceResponse.GeneralResponseData<List<EmployeeBankDTO>>> InsertEmployeeBanks(List<EmployeeBankDTO> employeeBankDTOs);
        Task<GeneralResponseSingle> InsertEmployeeBank(EmployeeBankDTO employeeBankDTO);
        Task<ServiceResponse.GeneralResponse> UpdateEmployeeBank(int id, EmployeeBankDTO employeeBankDTO);
        Task<ServiceResponse.GeneralResponse> DeleteEmployeeBank(int employeeBankID);
        Task<ServiceResponse.GeneralResponseData<List<EmployeeBankDTO>>> GetAllEmployeeBanks();
        Task<ServiceResponse.GeneralResponseData<EmployeeBankDTO>> GetEmployeeBankById(int employeeBankID);
    }
}
