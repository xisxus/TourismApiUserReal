using DataAccessLayer.Entites.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entites.Employees
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmployeeCode { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        public bool MaritalStatus { get; set; }

        [Required]
        [StringLength(17)]
        public string? IdentityCard { get; set; }

        // Foreign Keys
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }

        // Navigation Properties
        [ForeignKey("DesignationId")]
        public Designation Designation { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }




        public ICollection<EmpDept> EmpDepts { get; set; }
        public ICollection<EmployeeBank> EmployeeBanks { get; set; }

        public string ApplicationUserId { get; set; }  // Foreign key

        // Navigation properties
        public ApplicationUser ApplicationUser { get; set; }

        //public virtual ICollection<SystemSetting> SystemSettings { get; set; }
        //public virtual ICollection<VisaAssistanceService> VisaAssistanceServices { get; set; }


        public ICollection<EmployeeSalary> Salaries { get; set; }
        //public ICollection<EmployeeLeave> Leaves { get; set; }
        //public ICollection<EmployeeAttendance> Attendances { get; set; }
        //public ICollection<EmployeePerformance> Performances { get; set; }
        public ICollection<EmployeeDocument> Documents { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
