using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entites.Employees
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DeptName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }


        public ICollection<Employee> Employees { get; set; }
        public ICollection<EmpDept> EmpDepts { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}