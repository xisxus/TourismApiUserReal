using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using DataAccessLayer.Entites.Employees;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(IPerson person ) : ControllerBase
    {
        [HttpPost("CreatePerson")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePeron(PersonDTO personDTO)
        {
            var response = await person.InsertPersonWithPicturesAsync(personDTO);
            return Ok(response);
        }

        [HttpPut("UpdatePerson/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePerson(int id, PersonDTO personDTO)
        {
            var response = await person.UpdatePersonWithPicturesAsync(id, personDTO);
            return Ok(response);
        }

        [HttpGet("GetPerson/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var response = await person.GetPersonWithPicturesAsync(id);
            return Ok(response);
        }
    }
}
