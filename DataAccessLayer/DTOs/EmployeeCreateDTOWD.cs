using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class EmployeeCreateDTOWD
    {
        public int? EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool MaritalStatus { get; set; }
        public string IdentityCard { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public string ApplicationUserId { get; set; }

        public List<EmpDeptDTO2WD>? EmpDepts { get; set; }
        public List<EmployeeBankDTO2WD> EmployeeBanks { get; set; }
      
        public List<EmployeeSalaryDTO2WD> EmployeeSalaries { get; set; }
    }

    public class EmpDeptDTO2WD
    {
        public int DepartmentId { get; set; }
    }

    public class EmployeeBankDTO2WD
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string AccountType { get; set; }
    }

    

    public class EmployeeSalaryDTO2WD
    {
        public DateTime EffectiveDate { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? OvertimePay { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? Deductions { get; set; }
    }
}
