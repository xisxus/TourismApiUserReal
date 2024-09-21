using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class EmployeeDetailsDTO
    {
       
            public int EmployeeId { get; set; }
            public string EmployeeCode { get; set; }
            public DateTime DateOfBirth { get; set; }
            public DateTime JoiningDate { get; set; }
            public bool MaritalStatus { get; set; }
            public string IdentityCard { get; set; }
            public string ApplicationUserId { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public bool IsDeleted { get; set; }

            public int DepartmentId { get; set; }
            public string DeptName { get; set; }
            public string DepartmentDescription { get; set; }

            public int DesignationId { get; set; }
            public string DesignationTitle { get; set; }

            public int? EmployeeBankID { get; set; }
            public string BankName { get; set; }
            public string AccountNumber { get; set; }
            public string BranchName { get; set; }
            public string AccountType { get; set; }

            public int? DocumentId { get; set; }
            public string DocumentUrl { get; set; }
            public string DocumentType { get; set; }
            public DateTime UploadDate { get; set; }
            public string DocumentDescription { get; set; }
        

    }
}
