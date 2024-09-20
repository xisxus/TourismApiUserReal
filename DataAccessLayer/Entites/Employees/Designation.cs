using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entites.Employees
{
    public class Designation
    {
        [Key]
        public int DesignationId { get; set; }

        [Required]
        [StringLength(100)]
        public string DesignationTitle { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}