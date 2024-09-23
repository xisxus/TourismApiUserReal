using BusinessLogicLayer.Service;
using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideController : ControllerBase
    {
        private readonly IGuide _guideRepository;

        public GuideController(IGuide guideRepository)
        {
            _guideRepository = guideRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsertGuide([FromBody] GuideDTO guideDTO)
        {
            var response = await _guideRepository.InsertGuide(guideDTO);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuide(int id, [FromBody] GuideDTO guideDTO)
        {
            var response = await _guideRepository.UpdateGuide(id, guideDTO);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuide(int id)
        {
            var response = await _guideRepository.DeleteGuide(id);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuides()
        {
            var response = await _guideRepository.GetAllGuides();
            return response.Flag ? Ok(response) : NotFound(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuideById(int id)
        {
            var response = await _guideRepository.GetGuideById(id);
            return response.Flag ? Ok(response) : NotFound(response);
        }
    }
}
