using BusinessLogicLayer.Service;
using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployee employee) : ControllerBase
    {
        [HttpPost("CreateEmployee")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employeeDTO)
        {
            var response = await employee.InsertEmployee(employeeDTO);
            return Ok(response);
        }

        [HttpPut("UpdateEmployee/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO employeeDTO)
        {
            var response = await employee.UpdateEmployee(id, employeeDTO);
            return Ok(response);
        }

        [HttpDelete("DeleteEmployee/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var response = await employee.DeleteEmployee(id);
            return Ok(response);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var response = await employee.GetEmployeeById(id);
            return Ok(response);
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await employee.GetALlEmployee();
            return Ok(response);
        }
    }
}
