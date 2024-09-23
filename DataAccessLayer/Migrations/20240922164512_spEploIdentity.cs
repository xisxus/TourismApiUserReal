using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class spEploIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_InsertEmployeeIdentity
                        @EmployeeCode NVARCHAR(50),
                        @DateOfBirth DATE,
                        @JoiningDate DATE,
                        @MaritalStatus BIT,
                        @IdentityCard NVARCHAR(17),
                        @DesignationId INT,
                        @DepartmentId INT,
                        @ApplicationUserId NVARCHAR(450),
                        @NewEmployeeId INT OUTPUT
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

                        -- Return the newly created EmployeeId
                        SET @NewEmployeeId = SCOPE_IDENTITY();
                    END;
                    ");

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertEmployeeIdentity");
           
        }
    }
}
