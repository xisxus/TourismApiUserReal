using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class EmployeeDocumentDTO
    {
        public int DocumentId { get; set; }
        public int EmployeeId { get; set; }
        public string DocumentUrl { get; set; }
        public string DocumentType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UploadDate { get; set; }
        public string? DocumentDescription { get; set; }
    }

}
