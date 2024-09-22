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


        [HttpPost("insertBanks")]
        public async Task<IActionResult> InsertEmployeeBanks([FromBody] List<EmployeeBankDTO> employeeBankDTOs)
        {
            if (employeeBankDTOs == null || employeeBankDTOs.Count == 0)
            {
                return BadRequest("No bank details provided.");
            }

            var response = await _employeeBankRepository.InsertEmployeeBanks(employeeBankDTOs);

            if (response.Flag)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }



        [HttpPost("bank")]
        public async Task<IActionResult> CreateEmployeeBank([FromBody] EmployeeBankDTO employeeBankDTO)
        {
            var response = await _employeeBankRepository.InsertEmployeeBank(employeeBankDTO);
            return Ok(response);
        }

        [HttpPut("bank/{id}")]
        public async Task<IActionResult> UpdateEmployeeBank(int id, [FromBody] EmployeeBankDTO employeeBankDTO)
        {
            var response = await _employeeBankRepository.UpdateEmployeeBank(id, employeeBankDTO);
            return Ok(response);
        }

        [HttpDelete("bank/{employeeBankID}")]
        public async Task<IActionResult> DeleteEmployeeBank(int employeeBankID)
        {
            var response = await _employeeBankRepository.DeleteEmployeeBank(employeeBankID);
            return Ok(response);
        }

        [HttpGet("bank")]
        public async Task<IActionResult> GetAllEmployeeBanks()
        {
            var response = await _employeeBankRepository.GetAllEmployeeBanks();
            return Ok(response);
        }

        [HttpGet("bank/{employeeBankID}")]
        public async Task<IActionResult> GetEmployeeBankById(int employeeBankID)
        {
            var response = await _employeeBankRepository.GetEmployeeBankById(employeeBankID);
            return Ok(response);
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

        [HttpDelete("salary/{salaryId}")]
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

        [HttpGet("salary/{salaryId}")]
        public async Task<IActionResult> GetEmployeeSalaryById(int salaryId)
        {
            var response = await _employeeSalariesRepository.GetEmployeeSalaryById(salaryId);
            return Ok(response);
        }
    }
}
