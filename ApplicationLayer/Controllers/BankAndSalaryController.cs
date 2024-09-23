using BusinessLogicLayer.Service;
using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAndSalaryController : ControllerBase
    {
        private readonly IEmployeeBank _employeeBankRepository;
        private readonly IEmployeeSalary _employeeSalariesRepository;

        public BankAndSalaryController(IEmployeeBank employeeBankRepository, IEmployeeSalary employeeSalariesRepository)
        {
            _employeeBankRepository = employeeBankRepository;
            _employeeSalariesRepository = employeeSalariesRepository;
        }


        

        [HttpPost("salary")]
        public async Task<IActionResult> CreateEmployeeSalary([FromBody] EmployeeSalariesDTO employeeSalariesDTO)
        {
            var response = await _employeeSalariesRepository.InsertEmployeeSalary(employeeSalariesDTO);
            return Ok(response);
        }

        [HttpPut("salary/{id}")]
        public async Task<IActionResult> UpdateEmployeeSalary(int id, [FromBody] EmployeeSalariesDTO employeeSalariesDTO)
        {
            var response = await _employeeSalariesRepository.UpdateEmployeeSalary(id, employeeSalariesDTO);
            return Ok(response);
        }

        [HttpDelete("salary/{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeSalary(int salaryId)
        {
            var response = await _employeeSalariesRepository.DeleteEmployeeSalary(salaryId);
            return Ok(response);
        }

        [HttpGet("salary")]
        public async Task<IActionResult> GetAllEmployeeSalaries()
        {
            var response = await _employeeSalariesRepository.GetAllEmployeeSalaries();
            return Ok(response);
        }

        [HttpGet("salary/{employeeId}")]
        public async Task<IActionResult> GetEmployeeSalaryById(int salaryId)
        {
            var response = await _employeeSalariesRepository.GetEmployeeSalaryById(salaryId);
            return Ok(response);
        }
    }
}
