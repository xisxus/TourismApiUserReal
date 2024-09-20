using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class assignRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                Create PROCEDURE AssignRoleToUser2
                    @UserEmail NVARCHAR(256),
                    @RoleName NVARCHAR(256)
                AS
                BEGIN
                    DECLARE @UserId NVARCHAR(450);
                    DECLARE @RoleId NVARCHAR(450);

                    -- Fetch the User ID by Email
                    SELECT @UserId = Id FROM AspNetUsers WHERE Email = @UserEmail;
                    IF @UserId IS NULL
                    BEGIN
                        SELECT -1 AS Status, 'User not found' AS Message;
                        RETURN;
                    END

                    -- Fetch the Role ID by RoleName
                    SELECT @RoleId = Id FROM AspNetRoles WHERE Name = @RoleName;
                    IF @RoleId IS NULL
                    BEGIN
                        SELECT -2 AS Status, 'Role not found' AS Message;
                        RETURN;
                    END

                    -- Check if the user is already assigned to the role
                    IF EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId)
                    BEGIN
                        SELECT -3 AS Status, 'Role already assigned' AS Message;
                        RETURN;
                    END

                    -- Assign the role to the user
                    INSERT INTO AspNetUserRoles (UserId, RoleId)
                    VALUES (@UserId, @RoleId);

                    SELECT 0 AS Status, 'Success' AS Message;
                END

            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AssignRoleToUser2");
        }
    }
}
