using BusinessLogicLayer.Service;
using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageGuideController : ControllerBase
    {
        private readonly IPackageGuide _packageGuideRepository;

        public PackageGuideController(IPackageGuide packageGuideRepository)
        {
            _packageGuideRepository = packageGuideRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(PackageGuideDTO packageGuideDTO)
        {
            var response = await _packageGuideRepository.InsertPackageGuide(packageGuideDTO);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PackageGuideDTO packageGuideDTO)
        {
            var response = await _packageGuideRepository.UpdatePackageGuide(id, packageGuideDTO);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _packageGuideRepository.DeletePackageGuide(id);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _packageGuideRepository.GetAllPackageGuides();
            return response.Flag ? Ok(response) : NotFound(response);
        }

        [HttpGet("{id}")]
        [HttpGet("guide/{guideId}")]
        public async Task<IActionResult> GetByGuideId(int guideId)
        {
            var response = await _packageGuideRepository.GetPackageGuideByGuideId(guideId);
            return response.Flag ? Ok(response) : NotFound(response);
        }

    }
}
