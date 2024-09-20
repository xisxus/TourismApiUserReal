using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class GetALLempReal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetAllEmployee
                                AS
                                BEGIN
                                    SELECT 
                                        e.EmployeeId,
                                        e.EmployeeCode,
                                        e.DateOfBirth,
                                        e.JoiningDate,
                                        e.MaritalStatus,
                                        e.IdentityCard,
                                        e.DesignationId,
                                        d.DesignationTitle,  -- Assuming Designation has a name
                                        e.DepartmentId,
                                        dep.DeptName, -- Assuming Department has a name
                                        e.ApplicationUserId,
                                        u.UserName,         -- Assuming ApplicationUser has a UserName
                                        e.CreatedAt,
                                        e.UpdatedAt
                                    FROM Employees e
                                    JOIN Departments dep ON e.DepartmentId = dep.DepartmentId
                                    JOIN Designations d ON e.DesignationId = d.DesignationId
                                    JOIN AspNetUsers u ON e.ApplicationUserId = u.Id
                                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllEmployee");
        }
    }
}
