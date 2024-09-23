using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class EmployeeCreateDTO
    {
        public string EmployeeCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool MaritalStatus { get; set; }
        public string IdentityCard { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public string ApplicationUserId { get; set; }

        public List<EmpDeptDTO2> EmpDepts { get; set; }
        public List<EmployeeBankDTO2> EmployeeBanks { get; set; }
        public List<EmployeeDocumentDTO2> EmployeeDocuments { get; set; }
        public List<EmployeeSalaryDTO2> EmployeeSalaries { get; set; }
    }

    public class EmpDeptDTO2
    {
        public int DepartmentId { get; set; }
    }

    public class EmployeeBankDTO2
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string AccountType { get; set; }
    }

    public class EmployeeDocumentDTO2
    {
        public IFormFile formFile { get; set; }
        public string? DocumentUrl { get; set; }
        public string DocumentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string Description { get; set; }
    }

    public class EmployeeSalaryDTO2
    {
        public DateTime EffectiveDate { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? OvertimePay { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? Deductions { get; set; }
    }
}

