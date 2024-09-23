using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Sp_Guide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(@"
            //    CREATE PROCEDURE SP_InsertGuide
            //        @GuideName NVARCHAR(100),
            //        @GuideAddress NVARCHAR(255),
            //        @GuideCode NVARCHAR(50),
            //        @ApplicationUserID NVARCHAR(450)
            //    AS
            //    BEGIN
            //        INSERT INTO Guides (GuideName, GuideAddress, GuideCode, ApplicationUserID)
            //        VALUES (@GuideName, @GuideAddress, @GuideCode, @ApplicationUserID);

                    
            
            //        SELECT SCOPE_IDENTITY() AS GuideId;
            //    END;

            //    CREATE PROCEDURE SP_UpdateGuide
            //        @GuideId INT,
            //        @GuideName NVARCHAR(100),
            //        @GuideAddress NVARCHAR(255),
            //        @GuideCode NVARCHAR(50),
            //        @ApplicationUserID NVARCHAR(450)
            //    AS
            //    BEGIN
            //        UPDATE Guides
            //        SET GuideName = @GuideName,
            //            GuideAddress = @GuideAddress,
            //            GuideCode = @GuideCode,
            //            ApplicationUserID = @ApplicationUserID
            //        WHERE GuideId = @GuideId;
            //    END;

            //    CREATE PROCEDURE SP_DeleteGuide
            //        @GuideId INT
            //    AS
            //    BEGIN
            //        DELETE FROM Guides
            //        WHERE GuideId = @GuideId;
            //    END;

            //    CREATE PROCEDURE SP_GetAllGuides
            //    AS
            //    BEGIN
            //        SELECT GuideId, GuideName, GuideAddress, GuideCode, ApplicationUserID
            //        FROM Guides;
            //    END;

            //    CREATE PROCEDURE SP_GetGuideById
            //        @GuideId INT
            //    AS
            //    BEGIN
            //        SELECT GuideId, GuideName, GuideAddress, GuideCode, ApplicationUserID
            //        FROM Guides
            //        WHERE GuideId = @GuideId;
            //    END;
            //");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(@"
            //    DROP PROCEDURE IF EXISTS SP_InsertGuide;
            //    DROP PROCEDURE IF EXISTS SP_UpdateGuide;
            //    DROP PROCEDURE IF EXISTS SP_DeleteGuide;
            //    DROP PROCEDURE IF EXISTS SP_GetAllGuides;
            //    DROP PROCEDURE IF EXISTS SP_GetGuideById;
            //   ");
        }

    }
}
