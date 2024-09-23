using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class DocumentUploadRequest
    {
        public int EmployeeId { get; set; }  // Add EmployeeId here
        public IFormFile File { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
    }

}
