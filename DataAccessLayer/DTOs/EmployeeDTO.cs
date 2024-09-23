using DataAccessLayer.Entites.Employees;
using DataAccessLayer.Entites.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
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

        public string? DesignationTittle { get; set; }
        public string? DeptName { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }
    }
}
