using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class BankAndSalaryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Stored Procedures
            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_InsertEmployeeBank1
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
            CREATE PROCEDURE SP_UpdateEmployeeBank1
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
            CREATE PROCEDURE SP_DeleteEmployeeBank1
            @EmployeeID INT
            AS
            BEGIN
                Update EmployeeSalaries set IsDeleted = 1  WHERE EmployeeID = @EmployeeID
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetAllEmployeeBanks1
            AS
            BEGIN
                SELECT * FROM EmployeeBank WHERE IsDeleted = 0
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetEmployeeBankById1
            @EmployeeID INT
            AS
            BEGIN
                SELECT * FROM EmployeeBank WHERE EmployeeID = @EmployeeID AND IsDeleted = 0
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_InsertEmployeeSalary1
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
            CREATE PROCEDURE SP_UpdateEmployeeSalary1
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
            CREATE PROCEDURE SP_DeleteEmployeeSalary1
            @EmployeeId INT
            AS
            BEGIN
                Update EmployeeSalaries set IsDeleted = 1 WHERE EmployeeId = @EmployeeId
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetAllEmployeeSalaries1
            AS
            BEGIN
                SELECT * FROM EmployeeSalaries WHERE IsDeleted = 0
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetEmployeeSalaryById1
            @EmployeeId INT
            AS
            BEGIN
                SELECT * FROM EmployeeSalaries WHERE EmployeeId = @EmployeeId AND IsDeleted = 0
            END
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetEmployeeSalaryById1");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetAllEmployeeSalaries1");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeleteEmployeeSalary1");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdateEmployeeSalary1");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertEmployeeSalary1");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetEmployeeBankById1");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetAllEmployeeBanks1");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeleteEmployeeBank1");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdateEmployeeBank1");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertEmployeeBank1");
        }
    }
}
