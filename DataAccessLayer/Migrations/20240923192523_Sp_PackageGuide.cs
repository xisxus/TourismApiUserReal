using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Sp_PackageGuide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
    //        migrationBuilder.Sql(@"
    //    CREATE PROCEDURE SP_InsertPackageGuide
    //        @PackageId INT,
    //        @GuideId INT
    //    AS
    //    BEGIN
    //        INSERT INTO PackageGuides (PackageId, GuideId, CreatedAt, UpdatedAt)
    //        VALUES (@PackageId, @GuideId, GETDATE(), GETDATE());
    //    END
    //");

    //        migrationBuilder.Sql(@"
    //    CREATE PROCEDURE SP_UpdatePackageGuide
    //        @PackageGuideId INT,
    //        @PackageId INT,
    //        @GuideId INT
    //    AS
    //    BEGIN
    //        UPDATE PackageGuides
    //        SET PackageId = @PackageId,
    //            GuideId = @GuideId,
    //            UpdatedAt = GETDATE()
    //        WHERE PackageGuideId = @PackageGuideId;
    //    END
    //");

    //        migrationBuilder.Sql(@"
    //    CREATE PROCEDURE SP_DeletePackageGuide
    //        @PackageGuideId INT
    //    AS
    //    BEGIN
    //        DELETE FROM PackageGuides
    //        WHERE PackageGuideId = @PackageGuideId;
    //    END
    //");

    //        migrationBuilder.Sql(@"
    //    CREATE PROCEDURE SP_GetAllPackageGuides
    //    AS
    //    BEGIN
    //        SELECT PackageGuideId, PackageId, GuideId, CreatedAt, UpdatedAt
    //        FROM PackageGuides;
    //    END
    //");

    //        migrationBuilder.Sql(@"
    //    CREATE PROCEDURE SP_GetPackageGuideByGuideId
    //        @GuideId INT
    //    AS
    //    BEGIN
    //        SELECT PackageGuideId, PackageId, GuideId, CreatedAt, UpdatedAt
    //        FROM PackageGuides
    //        WHERE GuideId = @GuideId;
    //    END
    //");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
{
    //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertPackageGuide");
    //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdatePackageGuide");
    //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeletePackageGuide");
    //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetAllPackageGuides");
    //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetPackageGuideByGuideId");
}

    }
}
