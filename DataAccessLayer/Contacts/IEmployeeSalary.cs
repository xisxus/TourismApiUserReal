using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contacts
{
    public interface IEmployeeSalary
    {
        Task<ServiceResponse.GeneralResponse> InsertEmployeeSalary(EmployeeSalariesDTO employeeSalariesDTO);
        Task<ServiceResponse.GeneralResponse> UpdateEmployeeSalary(int id, EmployeeSalariesDTO employeeSalariesDTO);
        Task<ServiceResponse.GeneralResponse> DeleteEmployeeSalary(int salaryId);
        Task<ServiceResponse.GeneralResponseData<List<EmployeeSalariesDTO>>> GetAllEmployeeSalaries();
        Task<ServiceResponse.GeneralResponseData<EmployeeSalariesDTO>> GetEmployeeSalaryById(int salaryId);
    }
}
