using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entites.Employees
{
    public class EmployeeSalary
    {
        [Key]
        public int SalaryId { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }  // Navigation Property

        [Required]
        public DateTime EffectiveDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BaseSalary { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Allowances { get; set; } = 0;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OvertimePay { get; set; } = 0;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Bonus { get; set; } = 0;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Deductions { get; set; } = 0;

        [NotMapped]
        public decimal NetSalary => BaseSalary + (Bonus ?? 0) + (Allowances ?? 0) + (OvertimePay ?? 0) - (Deductions ?? 0);


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}