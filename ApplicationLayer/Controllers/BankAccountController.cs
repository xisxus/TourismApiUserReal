using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IEmployeeBank _employeeBankRepository;

        public BankAccountController(IEmployeeBank employeeBankRepository)
        {
            _employeeBankRepository = employeeBankRepository;
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

        [HttpGet("bank/{employeeID}")]
        public async Task<IActionResult> GetEmployeeBankById(int employeeID)
        {
            var response = await _employeeBankRepository.GetEmployeeBankById(employeeID);
            return Ok(response);
        }
    }
}
