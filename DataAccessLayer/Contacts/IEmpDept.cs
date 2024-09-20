using DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Contacts
{
    public interface IEmpDept
    {
        Task<ServiceResponse.GeneralResponse> InsertEmpDept(EmpDeptDTO empDeptDTO);
        Task<ServiceResponse.GeneralResponse> UpdateEmpDept(int id, EmpDeptDTO empDeptDTO);
        Task<ServiceResponse.GeneralResponse> DeleteEmpDept(int empDeptId);
        Task<ServiceResponse.GeneralResponseData<List<EmpDeptDTO>>> GetAllEmpDepts();
        Task<ServiceResponse.GeneralResponseData<EmpDeptDTO>> GetEmpDeptById(int empDeptId);
    }
}
