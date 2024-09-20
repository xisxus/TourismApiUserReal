using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class unAssignRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE UnassignRoleFromUser
                    @UserEmail NVARCHAR(256),
                    @RoleName NVARCHAR(256)
                AS
                BEGIN
                    DECLARE @UserId UNIQUEIDENTIFIER;
                    DECLARE @RoleId UNIQUEIDENTIFIER;

                    -- Fetch the User ID by Email
                    SELECT @UserId = Id FROM AspNetUsers WHERE Email = @UserEmail;
                    IF @UserId IS NULL
                    BEGIN
                        SELECT -1 AS Result; -- User not found
                        RETURN;
                    END

                    -- Fetch the Role ID by RoleName
                    SELECT @RoleId = Id FROM AspNetRoles WHERE Name = @RoleName;
                    IF @RoleId IS NULL
                    BEGIN
                        SELECT -2 AS Result; -- Role not found
                        RETURN;
                    END

                    -- Check if the user is assigned to the role
                    IF NOT EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId)
                    BEGIN
                        SELECT -3 AS Result; -- Role not assigned to the user
                        RETURN;
                    END

                    -- Unassign the role from the user
                    DELETE FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId;

                    SELECT 0 AS Result; -- Success
                END
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UnassignRoleFromUser");
        }
    }
}
