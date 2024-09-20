using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entites.Employees
{
    public class EmployeeDocument
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }  // Navigation Property

        [Required]
        [MaxLength(255)]
        public string DocumentUrl { get; set; }

        [MaxLength(50)]
        public string DocumentType { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } 
    }
}