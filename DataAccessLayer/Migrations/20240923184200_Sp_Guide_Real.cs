using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Sp_Guide_Real : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.Sql(@"
        CREATE PROCEDURE SP_InsertGuide
            @GuideName NVARCHAR(100),
            @GuideAddress NVARCHAR(255),
            @GuideCode NVARCHAR(50),
            @ApplicationUserID NVARCHAR(450)
        AS
        BEGIN
            INSERT INTO Guides (GuideName, GuideAddress, GuideCode, ApplicationUserID, CreatedAt, IsDeleted)
            VALUES (@GuideName, @GuideAddress, @GuideCode, @ApplicationUserID, GETDATE(), 0);

            SELECT SCOPE_IDENTITY() AS GuideId;
        END;
    ");

            migrationBuilder.Sql("GO");

            migrationBuilder.Sql(@"
        CREATE PROCEDURE SP_UpdateGuide
            @GuideId INT,
            @GuideName NVARCHAR(100),
            @GuideAddress NVARCHAR(255),
            @GuideCode NVARCHAR(50),
            @ApplicationUserID NVARCHAR(450)
        AS
        BEGIN
            UPDATE Guides
            SET GuideName = @GuideName,
                GuideAddress = @GuideAddress,
                GuideCode = @GuideCode,
                ApplicationUserID = @ApplicationUserID,
                UpdatedAt = GETDATE()
            WHERE GuideId = @GuideId AND IsDeleted = 0;
        END;
    ");

            migrationBuilder.Sql("GO");

            migrationBuilder.Sql(@"
        CREATE PROCEDURE SP_DeleteGuide
            @GuideId INT
        AS
        BEGIN
            UPDATE Guides
            SET IsDeleted = 1
            WHERE GuideId = @GuideId;
        END;
    ");

            migrationBuilder.Sql("GO");

            migrationBuilder.Sql(@"
        CREATE PROCEDURE SP_GetAllGuides
        AS
        BEGIN
            SELECT GuideId, GuideName, GuideAddress, GuideCode, ApplicationUserID, CreatedAt, UpdatedAt, IsDeleted
            FROM Guides
            WHERE IsDeleted = 0;
        END;
    ");

            migrationBuilder.Sql("GO");

            migrationBuilder.Sql(@"
        CREATE PROCEDURE SP_GetGuideById
            @GuideId INT
        AS
        BEGIN
            SELECT GuideId, GuideName, GuideAddress, GuideCode, ApplicationUserID, CreatedAt, UpdatedAt, IsDeleted
            FROM Guides
            WHERE GuideId = @GuideId AND IsDeleted = 0;
        END;
    ");

            migrationBuilder.Sql("GO");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP PROCEDURE IF EXISTS SP_InsertGuide;
                DROP PROCEDURE IF EXISTS SP_UpdateGuide;
                DROP PROCEDURE IF EXISTS SP_DeleteGuide;
                DROP PROCEDURE IF EXISTS SP_GetAllGuides;
                DROP PROCEDURE IF EXISTS SP_GetGuideById;
            ");
        }
    }
}
