using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class EmployeeSalariesDTO
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? OvertimePay { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? Deductions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

}
