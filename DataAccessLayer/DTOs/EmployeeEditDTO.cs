using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class EmployeeEditDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool MaritalStatus { get; set; }
        public string IdentityCard { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public string ApplicationUserId { get; set; }

        public List<EmpDeptDTO3> EmpDepts { get; set; }
        public List<EmployeeBankDTO3> EmployeeBanks { get; set; }
        public List<EmployeeDocumentDTO3> EmployeeDocuments { get; set; }
        public List<EmployeeSalaryDTO3> EmployeeSalaries { get; set; }
    }

    public class EmpDeptDTO3
    {
        public int DepartmentId { get; set; }
    }

    public class EmployeeBankDTO3
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string AccountType { get; set; }
    }

    public class EmployeeDocumentDTO3
    {
        public string DocumentUrl { get; set; }
        public string DocumentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string Description { get; set; }
    }

    public class EmployeeSalaryDTO3
    {
        public DateTime EffectiveDate { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
    }

}
