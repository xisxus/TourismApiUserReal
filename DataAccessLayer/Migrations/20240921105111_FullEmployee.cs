using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FullEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                    CREATE TYPE dbo.EmpDeptsType AS TABLE
                    (
                        DepartmentId INT
                    );
                ");

                    migrationBuilder.Sql(@"
                    CREATE TYPE dbo.EmployeeBankType AS TABLE
                    (
                        BankName NVARCHAR(100),
                        AccountNumber NVARCHAR(50),
                        BranchName NVARCHAR(100),
                        AccountType NVARCHAR(50)
                    );
                ");

                    migrationBuilder.Sql(@"
                    CREATE TYPE dbo.EmployeeDocumentsType AS TABLE
                    (
                        DocumentUrl NVARCHAR(MAX),
                        DocumentType NVARCHAR(50),
                        UploadDate DATETIME2(7),
                        Description NVARCHAR(MAX)
                    );
                ");

                    migrationBuilder.Sql(@"
                    CREATE TYPE dbo.EmployeeSalariesType AS TABLE
                    (
                        EffectiveDate DATETIME2(7),
                        BaseSalary DECIMAL(18,2),
                        Allowances DECIMAL(18,2),
                        OvertimePay DECIMAL(18,2),
                        Bonus DECIMAL(18,2),
                        Deductions DECIMAL(18,2)
                    );
                ");


            // Create stored procedure for inserting an employee
            migrationBuilder.Sql(@"
            CREATE PROCEDURE dbo.InsertEmployeeFull
                @EmployeeCode NVARCHAR(50),
                @DateOfBirth DATETIME2(7),
                @JoiningDate DATETIME2(7),
                @MaritalStatus BIT,
                @IdentityCard NVARCHAR(17),
                @DesignationId INT,
                @DepartmentId INT,
                @ApplicationUserId NVARCHAR(450),
                @EmpDepts dbo.EmpDeptsType READONLY,
                @EmployeeBank dbo.EmployeeBankType READONLY,
                @EmployeeDocuments dbo.EmployeeDocumentsType READONLY,
                @EmployeeSalaries dbo.EmployeeSalariesType READONLY
            AS
            BEGIN
                SET NOCOUNT ON;

                DECLARE @EmployeeId INT;

                INSERT INTO dbo.Employees (EmployeeCode, DateOfBirth, JoiningDate, MaritalStatus, IdentityCard, DesignationId, DepartmentId, ApplicationUserId, CreatedAt, UpdatedAt, IsDeleted)
                VALUES (@EmployeeCode, @DateOfBirth, @JoiningDate, @MaritalStatus, @IdentityCard, @DesignationId, @DepartmentId, @ApplicationUserId, SYSDATETIME(), SYSDATETIME(), 0);

                SET @EmployeeId = SCOPE_IDENTITY();

                INSERT INTO dbo.EmpDepts (EmployeeId, DepartmentId, CreatedAt, UpdatedAt, IsDeleted)
                SELECT @EmployeeId, DepartmentId, SYSDATETIME(), SYSDATETIME(), 0 FROM @EmpDepts;

                INSERT INTO dbo.EmployeeBank (EmployeeID, BankName, AccountNumber, BranchName, AccountType, CreatedAt, UpdatedAt, IsDeleted)
                SELECT @EmployeeId, BankName, AccountNumber, BranchName, AccountType, SYSDATETIME(), SYSDATETIME(), 0 FROM @EmployeeBank;

                INSERT INTO dbo.EmployeeDocuments (EmployeeId, DocumentUrl, DocumentType, UploadDate, Description, CreatedAt, UpdatedAt, IsDeleted)
                SELECT @EmployeeId, DocumentUrl, DocumentType, UploadDate, Description, SYSDATETIME(), SYSDATETIME(), 0 FROM @EmployeeDocuments;

                INSERT INTO dbo.EmployeeSalaries (EmployeeId, EffectiveDate, BaseSalary, Allowances, OvertimePay, Bonus, Deductions, CreatedAt, UpdatedAt, IsDeleted)
                SELECT @EmployeeId, EffectiveDate, BaseSalary, Allowances, OvertimePay, Bonus, Deductions, SYSDATETIME(), SYSDATETIME(), 0 FROM @EmployeeSalaries;

            END
        ");

            // Create stored procedure for updating an employee
            migrationBuilder.Sql(@"
            CREATE PROCEDURE dbo.UpdateEmployeeFull
                @EmployeeId INT,
                @EmployeeCode NVARCHAR(50),
                @DateOfBirth DATETIME2(7),
                @JoiningDate DATETIME2(7),
                @MaritalStatus BIT,
                @IdentityCard NVARCHAR(17),
                @DesignationId INT,
                @DepartmentId INT,
                @ApplicationUserId NVARCHAR(450),
                @EmpDepts dbo.EmpDeptsType READONLY,
                @EmployeeBank dbo.EmployeeBankType READONLY,
                @EmployeeDocuments dbo.EmployeeDocumentsType READONLY,
                @EmployeeSalaries dbo.EmployeeSalariesType READONLY
            AS
            BEGIN
                SET NOCOUNT ON;

                UPDATE dbo.Employees
                SET EmployeeCode = @EmployeeCode,
                    DateOfBirth = @DateOfBirth,
                    JoiningDate = @JoiningDate,
                    MaritalStatus = @MaritalStatus,
                    IdentityCard = @IdentityCard,
                    DesignationId = @DesignationId,
                    DepartmentId = @DepartmentId,
                    ApplicationUserId = @ApplicationUserId,
                    UpdatedAt = SYSDATETIME()
                WHERE EmployeeId = @EmployeeId;

                DELETE FROM dbo.EmpDepts WHERE EmployeeId = @EmployeeId;
                INSERT INTO dbo.EmpDepts (EmployeeId, DepartmentId, CreatedAt, UpdatedAt, IsDeleted)
                SELECT @EmployeeId, DepartmentId, SYSDATETIME(), SYSDATETIME(), 0 FROM @EmpDepts;

                DELETE FROM dbo.EmployeeBank WHERE EmployeeID = @EmployeeId;
                INSERT INTO dbo.EmployeeBank (EmployeeID, BankName, AccountNumber, BranchName, AccountType, CreatedAt, UpdatedAt, IsDeleted)
                SELECT @EmployeeId, BankName, AccountNumber, BranchName, AccountType, SYSDATETIME(), SYSDATETIME(), 0 FROM @EmployeeBank;

                DELETE FROM dbo.EmployeeDocuments WHERE EmployeeId = @EmployeeId;
                INSERT INTO dbo.EmployeeDocuments (EmployeeId, DocumentUrl, DocumentType, UploadDate, Description, CreatedAt, UpdatedAt, IsDeleted)
                SELECT @EmployeeId, DocumentUrl, DocumentType, UploadDate, Description, SYSDATETIME(), SYSDATETIME(), 0 FROM @EmployeeDocuments;

                DELETE FROM dbo.EmployeeSalaries WHERE EmployeeId = @EmployeeId;
                INSERT INTO dbo.EmployeeSalaries (EmployeeId, EffectiveDate, BaseSalary, Allowances, OvertimePay, Bonus, Deductions, CreatedAt, UpdatedAt, IsDeleted)
                SELECT @EmployeeId, EffectiveDate, BaseSalary, Allowances, OvertimePay, Bonus, Deductions, SYSDATETIME(), SYSDATETIME(), 0 FROM @EmployeeSalaries;

            END
        ");

            // Create stored procedure for deleting an employee
            migrationBuilder.Sql(@"
            CREATE PROCEDURE dbo.DeleteEmployeeFull
                @EmployeeId INT
            AS
            BEGIN
                SET NOCOUNT ON;

                UPDATE dbo.Employees SET IsDeleted = 1 WHERE EmployeeId = @EmployeeId;
                UPDATE dbo.EmpDepts SET IsDeleted = 1 WHERE EmployeeId = @EmployeeId;
                UPDATE dbo.EmployeeBank SET IsDeleted = 1 WHERE EmployeeID = @EmployeeId;
                UPDATE dbo.EmployeeDocuments SET IsDeleted = 1 WHERE EmployeeId = @EmployeeId;
                UPDATE dbo.EmployeeSalaries SET IsDeleted = 1 WHERE EmployeeId = @EmployeeId;
            END
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("DROP TYPE IF EXISTS dbo.EmpDeptsType;");
            migrationBuilder.Sql("DROP TYPE IF EXISTS dbo.EmployeeBankType;");
            migrationBuilder.Sql("DROP TYPE IF EXISTS dbo.EmployeeDocumentsType;");
            migrationBuilder.Sql("DROP TYPE IF EXISTS dbo.EmployeeSalariesType;");


            // Drop stored procedures
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.InsertEmployeeFull;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.UpdateEmployeeFull;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.DeleteEmployeeFull;");
        }
    }
}
