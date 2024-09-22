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

        [HttpGet("GetAllFullEmployees")]
        public async Task<IActionResult> GetAllFullEmployees()
        {
            var response = await employee.GetAllFullEmployee();
            return Ok(response);
        }

        [HttpGet("GetAllFullEmployeesById")]
        public async Task<IActionResult> GetAllFullEmployees(int id)
        {
            var response = await employee.GetAllFullEmployeeById(id);
            return Ok(response);
        }

        [HttpPost("CreateEmployeeReal")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployeeReal( EmployeeCreateDTO employeeDTO,  IFormFile document)
        {
            var response = await employee.CreateEmployeeWithDocumentAsync(employeeDTO, document);
            return Ok(response);
        }

        [HttpPut("EditEmployeeReal")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEmployeeReal(EmployeeEditDTO employeeDTO , int id)
        {
            var response = await employee.EditEmployee(employeeDTO, id);
            return Ok(response);
        }


        [HttpPost("CreateEmployeeWD")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployeeWDl(EmployeeCreateDTOWD employeeDTO)
        {
            var response = await employee.CreateEmployeeWD(employeeDTO);
            return Ok(response);
        }

        [HttpPut("EditEmployeeWD")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEmployeeWD(EmployeeCreateDTOWD employeeDTO, int id)
        {
            var response = await employee.EditEmployeeWD(employeeDTO, id);
            return Ok(response);
        }
    }
}
