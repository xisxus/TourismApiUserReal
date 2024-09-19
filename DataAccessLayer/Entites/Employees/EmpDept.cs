﻿namespace DataAccessLayer.Entites.Employees
{
    public class EmpDept
    {
        public int EmpDeptId { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }

        // Navigation properties
        public Employee Employee { get; set; }
        public Department Department { get; set; }
    }
}