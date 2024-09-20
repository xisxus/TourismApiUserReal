using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class GetDeptDesigEMPDept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Departments
            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetAllDepartments
            AS
            BEGIN
                SELECT DepartmentId, DeptName, Description, CreatedAt, UpdatedAt
                FROM Departments
                WHERE IsDeleted = 0
            END");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetDepartmentById
            @DepartmentId INT
            AS
            BEGIN
                SELECT DepartmentId, DeptName, Description, CreatedAt, UpdatedAt
                FROM Departments
                WHERE DepartmentId = @DepartmentId
                AND IsDeleted = 0
            END");

            // Designations
            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetAllDesignations
            AS
            BEGIN
                SELECT DesignationId, DesignationTitle, CreatedAt, UpdatedAt
                FROM Designations
                WHERE IsDeleted = 0
            END");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetDesignationById
            @DesignationId INT
            AS
            BEGIN
                SELECT DesignationId, DesignationTitle, CreatedAt, UpdatedAt
                FROM Designations
                WHERE DesignationId = @DesignationId
                AND IsDeleted = 0
            END");

            // Employee-Department
            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetAllEmpDepts
            AS
            BEGIN
                SELECT EmpDeptId, EmployeeId, DepartmentId, CreatedAt, UpdatedAt
                FROM EmpDept
                WHERE IsDeleted = 0
            END");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetEmpDeptById
            @EmpDeptId INT
            AS
            BEGIN
                SELECT EmpDeptId, EmployeeId, DepartmentId, CreatedAt, UpdatedAt
                FROM EmpDepts
                WHERE EmpDeptId = @EmpDeptId
                AND IsDeleted = 0
            END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Departments
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetAllDepartments");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetDepartmentById");

            // Designations
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetAllDesignations");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetDesignationById");

            // Employee-Department
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetAllEmpDepts");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetEmpDeptById");
        }
    }
}
