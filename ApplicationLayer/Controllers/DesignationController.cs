using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController(IDesignation designation) : ControllerBase
    {
        [HttpPost("CreateDesignation")]
        public async Task<IActionResult> CreateDesignation(DesignationDTO designationDTO)
        {
            var response = await designation.InsertDesignation(designationDTO);
            return Ok(response);
        }

        [HttpPut("UpdateDesignation/{id}")]
        public async Task<IActionResult> UpdateDesignation(int id, DesignationDTO designationDTO)
        {
            var response = await designation.UpdateDesignation(id, designationDTO);
            return Ok(response);
        }

        [HttpDelete("DeleteDesignation/{id}")]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            var response = await designation.DeleteDesignation(id);
            return Ok(response);
        }

        [HttpGet("GetDesignationById/{id}")]
        public async Task<IActionResult> GetDesignationById(int id)
        {
            var response = await designation.GetDesignationById(id);
            return Ok(response);
        }

        [HttpGet("GetAllDesignations")]
        public async Task<IActionResult> GetAllDesignations()
        {
            var response = await designation.GetAllDesignations();
            return Ok(response);
        }
    }
}
