using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AccountCreateRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    CREATE PROCEDURE CreateRole3
                        @RoleName NVARCHAR(256)
                    AS
                    BEGIN
                        IF EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = @RoleName)
                        BEGIN
                            SELECT -1 AS Result -- Role already exists
                            RETURN
                        END

                        INSERT INTO AspNetRoles (Id, Name, NormalizedName)
                        VALUES (NEWID(), @RoleName, UPPER(@RoleName))

                        SELECT 0 AS Result -- Success
                    END
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CreateRole3");
        }
    }
}
