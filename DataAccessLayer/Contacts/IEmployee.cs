using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.DTOs.ServiceResponse;

namespace DataAccessLayer.Contacts
{
    public interface IEmployee
    {
        Task<ServiceResponse.GeneralResponseData<List<EmployeeDTO>>> GetALlEmployee();
        Task<GeneralResponseData<EmployeeDTO>> GetEmployeeById(int employeeId);
        Task<GeneralResponse> InsertEmployee(EmployeeDTO employeeDTO);
        Task<GeneralResponse> UpdateEmployee(int Id ,EmployeeDTO employeeDTO);
        Task<GeneralResponse> DeleteEmployee(int employeeId);

        Task<ServiceResponse.GeneralResponseData<List<EmpDTO>>> GetAllFullEmployee();

        Task<ServiceResponse.GeneralResponseData<List<EmpDTO>>> GetAllFullEmployeeById(int id);

        Task<ServiceResponse.GeneralResponse> CreateEmployee(EmployeeCreateDTO employeeDTO);

        Task<ServiceResponse.GeneralResponse> EditEmployee(EmployeeEditDTO employeeDTO, int id);
    }
}
