using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IDepartment department) : ControllerBase
    {
        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment(DepartmentDTO departmentDTO)
        {
            var response = await department.InsertDepartment(departmentDTO);
            return Ok(response);
        }

        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentDTO departmentDTO)
        {
            var response = await department.UpdateDepartment(id, departmentDTO);
            return Ok(response);
        }

        [HttpDelete("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var response = await department.DeleteDepartment(id);
            return Ok(response);
        }

        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var response = await department.GetDepartmentById(id);
            return Ok(response);
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var response = await department.GetAllDepartments();
            return Ok(response);
        }
    }
}
