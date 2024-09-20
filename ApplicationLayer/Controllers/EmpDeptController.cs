using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpDeptController(IEmpDept empDept) : ControllerBase
    {
        [HttpPost("CreateEmpDept")]
        public async Task<IActionResult> CreateEmpDept(EmpDeptDTO empDeptDTO)
        {
            var response = await empDept.InsertEmpDept(empDeptDTO);
            return Ok(response);
        }

        [HttpPut("UpdateEmpDept/{id}")]
        public async Task<IActionResult> UpdateEmpDept(int id, EmpDeptDTO empDeptDTO)
        {
            var response = await empDept.UpdateEmpDept(id, empDeptDTO);
            return Ok(response);
        }

        [HttpDelete("DeleteEmpDept/{id}")]
        public async Task<IActionResult> DeleteEmpDept(int id)
        {
            var response = await empDept.DeleteEmpDept(id);
            return Ok(response);
        }

        [HttpGet("GetEmpDeptById/{id}")]
        public async Task<IActionResult> GetEmpDeptById(int id)
        {
            var response = await empDept.GetEmpDeptById(id);
            return Ok(response);
        }

        [HttpGet("GetAllEmpDepts")]
        public async Task<IActionResult> GetAllEmpDepts()
        {
            var response = await empDept.GetAllEmpDepts();
            return Ok(response);
        }
    }
}
