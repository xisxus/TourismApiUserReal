using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeInsertWDReal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create new table type for EmployeeBank
            migrationBuilder.Sql(@"
                    CREATE TYPE dbo.NewEmployeeBankType2 AS TABLE
                    (
                        BankName NVARCHAR(100),
                        AccountNumber NVARCHAR(50),
                        BranchName NVARCHAR(100),
                        AccountType NVARCHAR(50)
                    );
                ");

            // Create new table type for EmployeeSalaries
            migrationBuilder.Sql(@"
                    CREATE TYPE dbo.NewEmployeeSalariesType2 AS TABLE
                    (
                        EffectiveDate DATETIME2(7),
                        BaseSalary DECIMAL(18,2),
                        Allowances DECIMAL(18,2),
                        OvertimePay DECIMAL(18,2),
                        Bonus DECIMAL(18,2),
                        Deductions DECIMAL(18,2)
                    );
                ");

            // Create stored procedure InsertNewEmployee
            migrationBuilder.Sql(@"
                    CREATE PROCEDURE dbo.InsertNewEmployee2
                        @EmployeeCode NVARCHAR(50),
                        @FirstName NVARCHAR(100),       -- New parameter for First Name
                        @LastName NVARCHAR(100),        -- New parameter for Last Name
                        @DateOfBirth DATETIME2(7),
                        @JoiningDate DATETIME2(7),
                        @MaritalStatus BIT,
                        @IdentityCard NVARCHAR(17),
                        @DesignationId INT,
                        @DepartmentId INT,              -- DepartmentId directly passed as parameter
                        @ApplicationUserId NVARCHAR(450),
                        @EmployeeBank dbo.NewEmployeeBankType2 READONLY,
                        @EmployeeSalaries dbo.NewEmployeeSalariesType2 READONLY,
                        @NewEmployeeId INT OUTPUT       -- Output parameter for the new EmployeeId
                    AS
                    BEGIN
                        SET NOCOUNT ON;

                        -- Insert into Employees table
                        INSERT INTO dbo.Employees 
                        (
                            EmployeeCode, 
                            FirstName,                  -- Insert FirstName
                            LastName,                   -- Insert LastName
                            DateOfBirth, 
                            JoiningDate, 
                            MaritalStatus, 
                            IdentityCard, 
                            DesignationId, 
                            DepartmentId, 
                            ApplicationUserId, 
                            CreatedAt, 
                            UpdatedAt, 
                            IsDeleted
                        )
                        VALUES 
                        (
                            @EmployeeCode, 
                            @FirstName,                 -- Value for FirstName
                            @LastName,                  -- Value for LastName
                            @DateOfBirth, 
                            @JoiningDate, 
                            @MaritalStatus, 
                            @IdentityCard, 
                            @DesignationId, 
                            @DepartmentId, 
                            @ApplicationUserId, 
                            GETDATE(), 
                            GETDATE(), 
                            0
                        );

                        -- Capture the newly created EmployeeId
                        SET @NewEmployeeId = SCOPE_IDENTITY();

                        -- Automatically insert into EmpDepts using the same DepartmentId provided in the parameter
                        INSERT INTO dbo.EmpDepts (EmployeeId, DepartmentId, CreatedAt, UpdatedAt, IsDeleted)
                        VALUES (@NewEmployeeId, @DepartmentId, GETDATE(), GETDATE(), 0);

                        -- Insert into EmployeeBank table
                        INSERT INTO dbo.EmployeeBank (EmployeeID, BankName, AccountNumber, BranchName, AccountType, CreatedAt, UpdatedAt, IsDeleted)
                        SELECT @NewEmployeeId, BankName, AccountNumber, BranchName, AccountType, GETDATE(), GETDATE(), 0 FROM @EmployeeBank;

                        -- Insert into EmployeeSalaries table
                        INSERT INTO dbo.EmployeeSalaries (EmployeeId, EffectiveDate, BaseSalary, Allowances, OvertimePay, Bonus, Deductions, CreatedAt, UpdatedAt, IsDeleted)
                        SELECT @NewEmployeeId, EffectiveDate, BaseSalary, Allowances, OvertimePay, Bonus, Deductions, GETDATE(), GETDATE(), 0 FROM @EmployeeSalaries;
                    END
                ");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop stored procedure InsertNewEmployee
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.InsertNewEmployee2;");

            // Drop table types
            migrationBuilder.Sql("DROP TYPE IF EXISTS dbo.NewEmployeeBankType2;");
            migrationBuilder.Sql("DROP TYPE IF EXISTS dbo.NewEmployeeSalariesType2;");
        }
    }
}
