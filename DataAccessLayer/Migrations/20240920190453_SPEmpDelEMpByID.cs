using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SPEmpDelEMpByID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE SP_DeleteEmployee
                    @EmployeeId INT
                AS
                BEGIN
                    -- Check if the employee exists before attempting to delete
                    IF EXISTS (SELECT 1 FROM Employees WHERE EmployeeId = @EmployeeId)
                    BEGIN
                        DELETE FROM Employees WHERE EmployeeId = @EmployeeId;
                    END
                    ELSE
                    BEGIN
                        -- You could also raise an error or return a custom response if necessary
                        RAISERROR('Employee not found', 16, 1);
                    END
                END;

            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE SP_GetEmployeeById
                    @EmployeeId INT
                AS
                BEGIN
                    -- Retrieve the employee details from the Employees table
                    SELECT 
                        EmployeeId,
                        EmployeeCode,
                        DateOfBirth,
                        JoiningDate,
                        MaritalStatus,
                        IdentityCard,
                        DesignationId,
                        DepartmentId,
                        ApplicationUserId,
                        CreatedAt,
                        UpdatedAt
                    FROM 
                        Employees
                    WHERE 
                        EmployeeId = @EmployeeId;
    
                    -- You can add error handling here if needed
                END;


            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeleteEmployee");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetEmployeeById");
        }
    }
}
