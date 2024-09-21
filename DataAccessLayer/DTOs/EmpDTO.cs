using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class EmpDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool MaritalStatus { get; set; }
        public string IdentityCard { get; set; }
        public int DesignationId { get; set; }
        public string DesignationTittle { get; set; }
        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string DepartmentDescription { get; set; }
        public bool isDeleted { get; set; } 

        // Bank Details
        public List<EmployeeBankDTO> EmployeeBanks { get; set; }

        // Document Details
        public List<EmployeeDocumentDTO> EmployeeDocuments { get; set; }
    }
}
