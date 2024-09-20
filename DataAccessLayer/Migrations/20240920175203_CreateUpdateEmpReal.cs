using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateUpdateEmpReal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_InsertEmployee
                        @EmployeeCode NVARCHAR(50),
                        @DateOfBirth DATE,
                        @JoiningDate DATE,
                        @MaritalStatus BIT,
                        @IdentityCard NVARCHAR(17),
                        @DesignationId INT,
                        @DepartmentId INT,
                        @ApplicationUserId NVARCHAR(450)
                    AS
                    BEGIN
                        INSERT INTO Employees
                        (
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
                        )
                        VALUES 
                        (
                            @EmployeeCode,
                            @DateOfBirth,
                            @JoiningDate,
                            @MaritalStatus,
                            @IdentityCard,
                            @DesignationId,
                            @DepartmentId,
                            @ApplicationUserId,
                            GETDATE(),  -- Set CreatedAt dynamically
                            GETDATE()   -- Set UpdatedAt dynamically
                        );
                    END;
                    ");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_UpdateEmployee
                            @EmployeeId INT,
                            @EmployeeCode NVARCHAR(50),
                            @DateOfBirth DATE,
                            @JoiningDate DATE,
                            @MaritalStatus BIT,
                            @IdentityCard NVARCHAR(17),
                            @DesignationId INT,
                            @DepartmentId INT,
                            @ApplicationUserId NVARCHAR(450)
                        AS
                        BEGIN
                            UPDATE Employees
                            SET 
                                EmployeeCode = @EmployeeCode,
                                DateOfBirth = @DateOfBirth,
                                JoiningDate = @JoiningDate,
                                MaritalStatus = @MaritalStatus,
                                IdentityCard = @IdentityCard,
                                DesignationId = @DesignationId,
                                DepartmentId = @DepartmentId,
                                ApplicationUserId = @ApplicationUserId,
                                UpdatedAt = GETDATE() -- Set UpdatedAt dynamically
                            WHERE EmployeeId = @EmployeeId;
                        END;
                        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertEmployee");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdateEmployee");
        }
    }
}
