using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contacts
{
    public interface IDepartment
    {
        Task<ServiceResponse.GeneralResponse> InsertDepartment(DepartmentDTO departmentDTO);
        Task<ServiceResponse.GeneralResponse> UpdateDepartment(int id, DepartmentDTO departmentDTO);
        Task<ServiceResponse.GeneralResponse> DeleteDepartment(int departmentId);
        Task<ServiceResponse.GeneralResponseData<List<DepartmentDTO>>> GetAllDepartments();
        Task<ServiceResponse.GeneralResponseData<DepartmentDTO>> GetDepartmentById(int departmentId);
    }
}
