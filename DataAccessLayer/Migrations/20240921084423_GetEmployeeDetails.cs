using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class GetEmployeeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE sp_GetEmployeeFullDetailsById
                            @EmployeeId INT
                        AS
                        BEGIN
                            SET NOCOUNT ON;

                            SELECT 
                                e.EmployeeId,
                                e.EmployeeCode,
                                e.DateOfBirth,
                                e.JoiningDate,
                                e.MaritalStatus,
                                e.IdentityCard,
                                e.ApplicationUserId,
                                e.CreatedAt,
                                e.UpdatedAt,
                                e.IsDeleted,
        
                                d.DepartmentId,
                                d.DeptName,
                                d.Description AS DepartmentDescription,
        
                                des.DesignationId,
                                des.DesignationTitle,
        
                                eb.EmployeeBankID,
                                eb.BankName,
                                eb.AccountNumber,
                                eb.BranchName,
                                eb.AccountType,
        
                                ed.DocumentId,
                                ed.DocumentUrl,
                                ed.DocumentType,
                                ed.UploadDate,
                                ed.Description AS DocumentDescription
        
                            FROM dbo.Employees e
                            INNER JOIN dbo.Departments d ON e.DepartmentId = d.DepartmentId
                            INNER JOIN dbo.Designations des ON e.DesignationId = des.DesignationId
                            LEFT JOIN dbo.EmployeeBank eb ON e.EmployeeId = eb.EmployeeID
                            LEFT JOIN dbo.EmployeeDocuments ed ON e.EmployeeId = ed.EmployeeId
                            WHERE e.EmployeeId = @EmployeeId AND e.IsDeleted = 0
                        END
                        ");

            migrationBuilder.Sql(@"CREATE PROCEDURE sp_GetAllEmployeeFullDetails
                                AS
                                BEGIN
                                    SET NOCOUNT ON;

                                    SELECT 
                                        e.EmployeeId,
                                        e.EmployeeCode,
                                        e.DateOfBirth,
                                        e.JoiningDate,
                                        e.MaritalStatus,
                                        e.IdentityCard,
                                        e.ApplicationUserId,
                                        e.CreatedAt,
                                        e.UpdatedAt,
                                        e.IsDeleted,
        
                                        d.DepartmentId,
                                        d.DeptName,
                                        d.Description AS DepartmentDescription,
        
                                        des.DesignationId,
                                        des.DesignationTitle,
        
                                        eb.EmployeeBankID,
                                        eb.BankName,
                                        eb.AccountNumber,
                                        eb.BranchName,
                                        eb.AccountType,
        
                                        ed.DocumentId,
                                        ed.DocumentUrl,
                                        ed.DocumentType,
                                        ed.UploadDate,
                                        ed.Description AS DocumentDescription
        
                                    FROM dbo.Employees e
                                    INNER JOIN dbo.Departments d ON e.DepartmentId = d.DepartmentId
                                    INNER JOIN dbo.Designations des ON e.DesignationId = des.DesignationId
                                    LEFT JOIN dbo.EmployeeBank eb ON e.EmployeeId = eb.EmployeeID
                                    LEFT JOIN dbo.EmployeeDocuments ed ON e.EmployeeId = ed.EmployeeId
                                    WHERE e.IsDeleted = 0
                                END
                                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetEmployeeFullDetailsById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_GetAllEmployeeFullDetails");
        }
    }
}
