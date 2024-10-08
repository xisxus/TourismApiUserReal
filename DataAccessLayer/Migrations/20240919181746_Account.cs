﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Stored Procedure for creating a role
                 migrationBuilder.Sql(@"
                    CREATE PROCEDURE CreateRole
                        @RoleName NVARCHAR(256)
                    AS
                    BEGIN
                        IF EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = @RoleName)
                        BEGIN
                            RETURN -1 -- Role already exists
                        END

                        INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                        VALUES (NEWID(), @RoleName, UPPER(@RoleName))

                        RETURN 0 -- Success
                    END
                ");

                    // Create Stored Procedure for assigning a role to a user
                migrationBuilder.Sql(@"
                    CREATE PROCEDURE AssignRoleToUser
                        @UserEmail NVARCHAR(256),
                        @RoleName NVARCHAR(256)
                    AS
                    BEGIN
                        DECLARE @UserId NVARCHAR(450)
                        DECLARE @RoleId NVARCHAR(450)

                        SELECT @UserId = Id FROM AspNetUsers WHERE Email = @UserEmail
                        IF @UserId IS NULL
                        BEGIN
                            RETURN -1 -- User not found
                        END

                        SELECT @RoleId = Id FROM AspNetRoles WHERE Name = @RoleName
                        IF @RoleId IS NULL
                        BEGIN
                            RETURN -2 -- Role not found
                        END

                        IF EXISTS (SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId)
                        BEGIN
                            RETURN -3 -- Role already assigned
                        END

                        INSERT INTO AspNetUserRoles (UserId, RoleId)
                        VALUES (@UserId, @RoleId)

                        RETURN 0 -- Success
                    END
                ");

                    // Create Stored Procedure for getting user roles
                    migrationBuilder.Sql(@"
                    CREATE PROCEDURE GetUserRoles
                        @UserEmail NVARCHAR(256)
                    AS
                    BEGIN
                        DECLARE @UserId NVARCHAR(450)

                        SELECT @UserId = Id FROM AspNetUsers WHERE Email = @UserEmail
                        IF @UserId IS NULL
                        BEGIN
                            RETURN -1 -- User not found
                        END

                        SELECT r.Name
                        FROM AspNetUserRoles ur
                        JOIN AspNetRoles r ON ur.RoleId = r.Id
                        WHERE ur.UserId = @UserId
                    END
                ");

                    // Create Stored Procedure for getting all users with roles
                    migrationBuilder.Sql(@"
                    CREATE PROCEDURE GetAllUsersWithRoles
                    AS
                    BEGIN
                        SELECT u.Id, u.Email, u.UserName, r.Name AS RoleName
                        FROM AspNetUsers u
                        LEFT JOIN AspNetUserRoles ur ON u.Id = ur.UserId
                        LEFT JOIN AspNetRoles r ON ur.RoleId = r.Id
                    END
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the stored procedures when rolling back the migration
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CreateRole");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AssignRoleToUser");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetUserRoles");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetAllUsersWithRoles");
        }
    }
}
