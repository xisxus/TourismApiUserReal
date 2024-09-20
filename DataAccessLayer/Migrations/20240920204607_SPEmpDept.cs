using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SPEmpDept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(@"CREATE PROCEDURE SP_InsertEmpDept
            //            @EmployeeId INT,
            //            @DepartmentId INT
            //        AS
            //        BEGIN
            //            INSERT INTO EmpDept (EmployeeId, DepartmentId, CreatedAt, UpdatedAt, IsDeleted)
            //            VALUES (@EmployeeId, @DepartmentId, GETDATE(), GETDATE(), 0)
            //        END
            //         ");

            //migrationBuilder.Sql(@"CREATE PROCEDURE SP_UpdateEmpDept
            //            @EmpDeptId INT,
            //            @EmployeeId INT,
            //            @DepartmentId INT
            //        AS
            //        BEGIN
            //            UPDATE EmpDept
            //            SET EmployeeId = @EmployeeId,
            //                DepartmentId = @DepartmentId,
            //                UpdatedAt = GETDATE()
            //            WHERE EmpDeptId = @EmpDeptId
            //        END
            //         ");

            //migrationBuilder.Sql(@"CREATE PROCEDURE SP_DeleteEmpDept
            //            @EmpDeptId INT
            //        AS
            //        BEGIN
            //            UPDATE EmpDept
            //            SET IsDeleted = 1
            //            WHERE EmpDeptId = @EmpDeptId
            //        END
            //         ");
            //migrationBuilder.Sql(@"
            //        CREATE PROCEDURE SP_GetEmpDeptById
            //            @EmpDeptId INT
            //        AS
            //        BEGIN
            //            SELECT EmpDeptId, EmployeeId, DepartmentId, CreatedAt, UpdatedAt
            //            FROM EmpDept
            //            WHERE EmpDeptId = @EmpDeptId AND IsDeleted = 0
            //        END
            //        ");
            //migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetAllEmpDepts
            //        AS
            //        BEGIN
            //            SELECT EmpDeptId, EmployeeId, DepartmentId, CreatedAt, UpdatedAt
            //            FROM EmpDept
            //            WHERE IsDeleted = 0
            //        END
            //         ");
         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertEmpDept");
            //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdateEmpDept");
            //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeleteEmpDept");
            //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetEmpDeptById");
            //migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_GetAllEmpDepts");
        }
    }
}
