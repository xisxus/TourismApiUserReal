using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SPDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Department
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_InsertDepartment
                    @DeptName NVARCHAR(100),
                    @Description NVARCHAR(500)
                    
                AS
                BEGIN
                    INSERT INTO Departments (DeptName, Description, CreatedAt, UpdatedAt, IsDeleted)
                    VALUES (@DeptName, @Description, GETDATE(), GETDATE(), 0);
                END
                 ");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_UpdateDepartment
                    @DepartmentId INT,
                    @DeptName NVARCHAR(100),
                    @Description NVARCHAR(500)
                   
                AS
                BEGIN
                    UPDATE Departments
                    SET DeptName = @DeptName,
                        Description = @Description,
                        UpdatedAt = GETDATE()
                    WHERE DepartmentId = @DepartmentId AND IsDeleted = 0;
                END
                 ");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_DeleteDepartment
                    @DepartmentId INT
                AS
                BEGIN
                    UPDATE Departments
                    SET IsDeleted = 1, UpdatedAt = GETDATE()
                    WHERE DepartmentId = @DepartmentId;
                END
                    ");

            //Designation 
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_InsertDesignation
                    @DesignationTitle NVARCHAR(100)
                   
                AS
                BEGIN
                    INSERT INTO Designations (DesignationTitle, CreatedAt, UpdatedAt, IsDeleted)
                    VALUES (@DesignationTitle, GETDATE(), GETDATE(), 0);
                END
                 ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE SP_UpdateDesignation
                    @DesignationId INT,
                    @DesignationTitle NVARCHAR(100)
                   
                AS
                BEGIN
                    UPDATE Designations
                    SET DesignationTitle = @DesignationTitle,
                        UpdatedAt = GETDATE()
                    WHERE DesignationId = @DesignationId AND IsDeleted = 0;
                END
                ");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_DeleteDesignation
                    @DesignationId INT
                AS
                BEGIN
                    UPDATE Designations
                    SET IsDeleted = 1, UpdatedAt = GETDATE()
                    WHERE DesignationId = @DesignationId;
                END
                 ");

            //EmpDept
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_InsertEmpDept
                    @EmployeeId INT,
                    @DepartmentId INT
                
                AS
                BEGIN
                    INSERT INTO EmpDepts (EmployeeId, DepartmentId, CreatedAt, UpdatedAt, IsDeleted)
                    VALUES (@EmployeeId, @DepartmentId, GETDATE(), GETDATE(), 0);
                END
                 ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE SP_UpdateEmpDept
                    @EmpDeptId INT,
                    @EmployeeId INT,
                    @DepartmentId INT
                   
                AS
                BEGIN
                    UPDATE EmpDepts
                    SET EmployeeId = @EmployeeId,
                        DepartmentId = @DepartmentId,
                        UpdatedAt = GETDATE()
                    WHERE EmpDeptId = @EmpDeptId AND IsDeleted = 0;
                END
                ");

            migrationBuilder.Sql(@"CREATE PROCEDURE SP_DeleteEmpDept
                    @EmpDeptId INT
                AS
                BEGIN
                    UPDATE EmpDepts
                    SET IsDeleted = 1, UpdatedAt = GETDATE()
                    WHERE EmpDeptId = @EmpDeptId;
                END
                 ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertDepartment");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdateDepartment");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeleteDepartment");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertDesignation");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdateDesignation");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeleteDesignation");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_InsertEmpDept");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_UpdateEmpDept");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS SP_DeleteEmpDept");

        }
    }
}
