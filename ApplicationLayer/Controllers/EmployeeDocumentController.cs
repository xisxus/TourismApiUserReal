using BusinessLogicLayer.Service;
using DataAccessLayer.Contacts;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDocumentController : ControllerBase
    {
        private readonly IEmployeeDocument _repository;
        private readonly IWebHostEnvironment _env;

        public EmployeeDocumentController(IEmployeeDocument repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        [HttpPost("UploadDocument/{employeeId}")]
        public async Task<IActionResult> UploadDocument(int employeeId, IFormFile file, string documentType, string description)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var documentDTO = new EmployeeDocumentDTO
            {
                EmployeeId = employeeId,
                DocumentUrl = Path.Combine("uploads", file.FileName), // Store the relative path
                DocumentType = documentType,
                Description = description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            };

            await _repository.InsertEmployeeDocument(documentDTO);
            return Ok("Document uploaded successfully.");
        }

        [HttpGet("GetDocuments/{employeeId}")]
        public async Task<IActionResult> GetDocuments(int employeeId)
        {
            var documents = await _repository.GetAllEmployeeDocuments(employeeId);
            return Ok(documents);
        }
    }
}
