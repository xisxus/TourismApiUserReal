using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class BankAndSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Stored Procedures
            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_InsertEmployeeBank
            @EmployeeID INT,
            @BankName NVARCHAR(MAX),
            @AccountNumber NVARCHAR(MAX),
            @BranchName NVARCHAR(MAX),
            @AccountType NVARCHAR(MAX)
            AS
            BEGIN
                INSERT INTO EmployeeBank (EmployeeID, BankName, AccountNumber, BranchName, AccountType, CreatedAt, UpdatedAt)
                VALUES (@EmployeeID, @BankName, @AccountNumber, @BranchName, @AccountType, GETDATE(), GETDATE())
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_UpdateEmployeeBank
            @EmployeeBankID INT,
            @EmployeeID INT,
            @BankName NVARCHAR(MAX),
            @AccountNumber NVARCHAR(MAX),
            @BranchName NVARCHAR(MAX),
            @AccountType NVARCHAR(MAX)
            AS
            BEGIN
                UPDATE EmployeeBank
                SET EmployeeID = @EmployeeID,
                    BankName = @BankName,
                    AccountNumber = @AccountNumber,
                    BranchName = @BranchName,
                    AccountType = @AccountType,
                    UpdatedAt = GETDATE()
                WHERE EmployeeBankID = @EmployeeBankID
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_DeleteEmployeeBank
            @EmployeeBankID INT
            AS
            BEGIN
                DELETE FROM EmployeeBank WHERE EmployeeBankID = @EmployeeBankID
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetAllEmployeeBanks
            AS
            BEGIN
                SELECT * FROM EmployeeBank WHERE IsDeleted = 0
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetEmployeeBankById
            @EmployeeBankID INT
            AS
            BEGIN
                SELECT * FROM EmployeeBank WHERE EmployeeBankID = @EmployeeBankID AND IsDeleted = 0
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_InsertEmployeeSalary
            @EmployeeId INT,
            @EffectiveDate DATETIME2,
            @BaseSalary DECIMAL(18,2),
            @Allowances DECIMAL(18,2) = NULL,
            @OvertimePay DECIMAL(18,2) = NULL,
            @Bonus DECIMAL(18,2) = NULL,
            @Deductions DECIMAL(18,2) = NULL
            AS
            BEGIN
                INSERT INTO EmployeeSalaries (EmployeeId, EffectiveDate, BaseSalary, Allowances, OvertimePay, Bonus, Deductions, CreatedAt, UpdatedAt)
                VALUES (@EmployeeId, @EffectiveDate, @BaseSalary, @Allowances, @OvertimePay, @Bonus, @Deductions, GETDATE(), GETDATE())
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_UpdateEmployeeSalary
            @SalaryId INT,
            @EmployeeId INT,
            @EffectiveDate DATETIME2,
            @BaseSalary DECIMAL(18,2),
            @Allowances DECIMAL(18,2) = NULL,
            @OvertimePay DECIMAL(18,2) = NULL,
            @Bonus DECIMAL(18,2) = NULL,
            @Deductions DECIMAL(18,2) = NULL
            AS
            BEGIN
                UPDATE EmployeeSalaries
                SET EmployeeId = @EmployeeId,
                    EffectiveDate = @EffectiveDate,
                    BaseSalary = @BaseSalary,
                    Allowances = @Allowances,
                    OvertimePay = @OvertimePay,
                    Bonus = @Bonus,
                    Deductions = @Deductions,
                    UpdatedAt = GETDATE()
                WHERE SalaryId = @SalaryId
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_DeleteEmployeeSalary
            @SalaryId INT
            AS
            BEGIN
                DELETE FROM EmployeeSalaries WHERE SalaryId = @SalaryId
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetAllEmployeeSalaries
            AS
            BEGIN
                SELECT * FROM EmployeeSalaries WHERE IsDeleted = 0
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetEmployeeSalaryById
            @SalaryId INT
            AS
            BEGIN
                SELECT * FROM EmployeeSalaries WHERE SalaryId = @SalaryId AND IsDeleted = 0
            END
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetEmployeeSalaryById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetAllEmployeeSalaries");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeleteEmployeeSalary");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdateEmployeeSalary");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertEmployeeSalary");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetEmployeeBankById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetAllEmployeeBanks");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeleteEmployeeBank");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdateEmployeeBank");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertEmployeeBank");
        }
    }
}
