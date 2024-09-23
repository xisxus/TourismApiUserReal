using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Sp_Guide_Real_Real : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.Sql(@"
        CREATE PROCEDURE SP_InsertGuide1
            @GuideName NVARCHAR(100),
            @GuideAddress NVARCHAR(255),
            @GuideCode NVARCHAR(50),
            @ApplicationUserID NVARCHAR(450)
        AS
        BEGIN
            INSERT INTO Guides (GuideName, GuideAddress, GuideCode, ApplicationUserID, CreatedAt,UpdatedAt , IsDeleted)
            VALUES (@GuideName, @GuideAddress, @GuideCode, @ApplicationUserID, GETDATE(), GETDATE(), 0);

            SELECT SCOPE_IDENTITY() AS GuideId;
        END;
    ");

            migrationBuilder.Sql("GO");

            migrationBuilder.Sql(@"
        CREATE PROCEDURE SP_UpdateGuide1
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
        CREATE PROCEDURE SP_DeleteGuide1
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
        CREATE PROCEDURE SP_GetAllGuides1
        AS
        BEGIN
            SELECT GuideId, GuideName, GuideAddress, GuideCode, ApplicationUserID, CreatedAt, UpdatedAt, IsDeleted
            FROM Guides
            WHERE IsDeleted = 0;
        END;
    ");

            migrationBuilder.Sql("GO");

            migrationBuilder.Sql(@"
        CREATE PROCEDURE SP_GetGuideById1
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
                DROP PROCEDURE IF EXISTS SP_InsertGuide1;
                DROP PROCEDURE IF EXISTS SP_UpdateGuide1;
                DROP PROCEDURE IF EXISTS SP_DeleteGuide1;
                DROP PROCEDURE IF EXISTS SP_GetAllGuides1;
                DROP PROCEDURE IF EXISTS SP_GetGuideById1;
            ");
        }
    }
}
