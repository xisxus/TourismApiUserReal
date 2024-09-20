using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeDocumentProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_InsertEmployeeDocument
                @EmployeeId INT,
                @DocumentUrl NVARCHAR(MAX),
                @DocumentType NVARCHAR(50),
                @Description NVARCHAR(255),
                @CreatedAt DATETIME2,
                @UpdatedAt DATETIME2,
                @IsDeleted BIT
            AS
            BEGIN
                INSERT INTO EmployeeDocuments (EmployeeId, DocumentUrl, DocumentType, UploadDate, Description, CreatedAt, UpdatedAt, IsDeleted)
                VALUES (@EmployeeId, @DocumentUrl, @DocumentType, GETDATE(), @Description, @CreatedAt, @UpdatedAt, @IsDeleted)
            END
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE SP_GetAllEmployeeDocuments
                @EmployeeId INT
            AS
            BEGIN
                SELECT DocumentId, EmployeeId, DocumentUrl, DocumentType, Description, CreatedAt, UpdatedAt, IsDeleted
                FROM EmployeeDocuments
                WHERE EmployeeId = @EmployeeId AND IsDeleted = 0
            END
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("IF OBJECT_ID('SP_InsertEmployeeDocument', 'P') IS NOT NULL DROP PROCEDURE SP_InsertEmployeeDocument;");
            migrationBuilder.Sql("IF OBJECT_ID('SP_GetAllEmployeeDocuments', 'P') IS NOT NULL DROP PROCEDURE SP_GetAllEmployeeDocuments;");
        }
    }
}
