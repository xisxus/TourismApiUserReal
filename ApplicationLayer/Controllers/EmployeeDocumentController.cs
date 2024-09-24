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

        [HttpPost("UploadDocument")]
        public async Task<IActionResult> UploadDocuments([FromForm]List<DocumentUploadRequest> requests)
        {
            if (requests == null || requests.Count == 0)
                return BadRequest("No documents uploaded.");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uploadedDocuments = new List<EmployeeDocumentDTO>();

            foreach (var request in requests)
            {
                if (request.File == null || request.File.Length == 0)
                    continue; // Skip empty files

                var filePath = Path.Combine(uploadsFolder, request.File.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                var documentDTO = new EmployeeDocumentDTO
                {
                    EmployeeId = request.EmployeeId, // Now taken from the request DTO
                    DocumentUrl = Path.Combine("uploads", request.File.FileName), // Store the relative path
                    DocumentType = request.DocumentType, // Use the corresponding document type
                    Description = request.Description, // Use the corresponding description
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };

                uploadedDocuments.Add(documentDTO);
            }

            // Insert all documents into the database
            foreach (var document in uploadedDocuments)
            {
                await _repository.InsertEmployeeDocument(document);
            }

            return Ok("Documents uploaded successfully.");
        }



        [HttpPost("UploadDocumentSingle")]
        public async Task<IActionResult> UploadDocument([FromForm] DocumentUploadRequest request)
        {
            if (request == null || request.File == null || request.File.Length == 0)
                return BadRequest("No document uploaded.");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, request.File.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var documentDTO = new EmployeeDocumentDTO
            {
                EmployeeId = request.EmployeeId, // Taken from the request DTO
                DocumentUrl = $"uploads/{request.File.FileName}", // Store the relative path
                DocumentType = request.DocumentType, // Use the corresponding document type
                Description = request.Description, // Use the corresponding description
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            };

            // Insert the document into the database
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
