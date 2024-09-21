using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Person : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE TYPE PersonPicturesTableType AS TABLE
                            (
                                PictureUrl NVARCHAR(MAX),
                                Caption NVARCHAR(MAX)
                            );
                            GO
                            ");

            migrationBuilder.Sql(@"CREATE PROCEDURE usp_InsertPersonWithPictures
                    @FirstName NVARCHAR(50),
                    @LastName NVARCHAR(50),
                    @Gender NVARCHAR(6),
                    @IdentityCard NVARCHAR(17),
                    @PassportID NVARCHAR(9),
                    @Mobile NVARCHAR(50),
                    @Address NVARCHAR(500),
                    @CityId INT,
                    @ApplicationUserId NVARCHAR(450),
                    @DateofBirth DATETIME2(7),
                    @PersonType NVARCHAR(MAX),
                    @Pictures PersonPicturesTableType READONLY -- Table-Valued Parameter for pictures
                AS
                BEGIN
                    SET NOCOUNT ON;

                    -- Insert into People table
                    DECLARE @NewPersonId INT;
                    INSERT INTO [dbo].[People] 
                        ([FirstName], [LastName], [Gender], [IdentityCard], [PassportID], 
                         [Mobile], [Address], [CityId], [ApplicationUserId], [DateofBirth], 
                         [CreatedAt], [UpdatedAt], [PersonType])
                    VALUES 
                        (@FirstName, @LastName, @Gender, @IdentityCard, @PassportID, 
                         @Mobile, @Address, @CityId, @ApplicationUserId, @DateofBirth, 
                         GETDATE(), GETDATE(), @PersonType);

                    SET @NewPersonId = SCOPE_IDENTITY();

                    -- Insert related pictures if any
                    IF EXISTS (SELECT 1 FROM @Pictures)
                    BEGIN
                        INSERT INTO [dbo].[PersonPictures] ([PersonId], [PictureUrl], [Caption])
                        SELECT @NewPersonId, PictureUrl, Caption FROM @Pictures;
                    END
                END;
                GO
                ");

            migrationBuilder.Sql(@"CREATE PROCEDURE usp_UpdatePersonWithPictures
                    @PersonId INT,
                    @FirstName NVARCHAR(50),
                    @LastName NVARCHAR(50),
                    @Gender NVARCHAR(6),
                    @IdentityCard NVARCHAR(17),
                    @PassportID NVARCHAR(9),
                    @Mobile NVARCHAR(50),
                    @Address NVARCHAR(500),
                    @CityId INT,
                    @DateofBirth DATETIME2(7),
                    @PersonType NVARCHAR(MAX),
                    @Pictures PersonPicturesTableType READONLY -- Table-Valued Parameter for pictures
                AS
                BEGIN
                    SET NOCOUNT ON;

                    -- Update People table
                    UPDATE [dbo].[People]
                    SET 
                        [FirstName] = @FirstName,
                        [LastName] = @LastName,
                        [Gender] = @Gender,
                        [IdentityCard] = @IdentityCard,
                        [PassportID] = @PassportID,
                        [Mobile] = @Mobile,
                        [Address] = @Address,
                        [CityId] = @CityId,
                        [DateofBirth] = @DateofBirth,
                        [UpdatedAt] = GETDATE(),
                        [PersonType] = @PersonType
                    WHERE [PersonId] = @PersonId;

                    -- Delete existing pictures for the person
                    DELETE FROM [dbo].[PersonPictures] WHERE [PersonId] = @PersonId;

                    -- Insert new pictures if any
                    IF EXISTS (SELECT 1 FROM @Pictures)
                    BEGIN
                        INSERT INTO [dbo].[PersonPictures] ([PersonId], [PictureUrl], [Caption])
                        SELECT @PersonId, PictureUrl, Caption FROM @Pictures;
                    END
                END;
                GO
                ");

            migrationBuilder.Sql(@"CREATE PROCEDURE usp_GetPersonWithPictures
                    @PersonId INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    -- Retrieve the person details
                    SELECT 
                        p.[PersonId],
                        p.[FirstName],
                        p.[LastName],
                        p.[Gender],
                        p.[IdentityCard],
                        p.[PassportID],
                        p.[Mobile],
                        p.[Address],
                        p.[CityId],
                        p.[ApplicationUserId],
                        p.[DateofBirth],
                        p.[CreatedAt],
                        p.[UpdatedAt],
                        p.[PersonType],
                        c.[CityName] -- Assuming City table has a column named CityName
                    FROM 
                        [dbo].[People] p
                    INNER JOIN 
                        [dbo].[Cities] c ON p.[CityId] = c.[CityId]
                    WHERE 
                        p.[PersonId] = @PersonId;

                    -- Retrieve the associated pictures
                    SELECT 
                        pp.[PictureId],
                        pp.[PictureUrl],
                        pp.[Caption]
                    FROM 
                        [dbo].[PersonPictures] pp
                    WHERE 
                        pp.[PersonId] = @PersonId;
                END;
                GO
                ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TYPE IF EXISTS dbo.PersonPicturesTableType;");


            // Drop stored procedures
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.usp_InsertPersonWithPictures;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.usp_UpdatePersonWithPictures;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.usp_GetPersonWithPictures;");
        }
    }
}
