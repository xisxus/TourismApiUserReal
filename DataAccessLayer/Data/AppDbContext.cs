using DataAccessLayer.Entites.Employees;
using DataAccessLayer.Entites.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class AppDbContext (DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmpDept> EmpDepts { get; set; }

        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<PersonPicture> PersonPictures { get; set; }

        public DbSet<Person> People { get; set; }
        public DbSet<City> Cities { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PersonPicture>()
                .HasKey(p => new { p.PersonId, p.PictureId });


            builder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });

            builder.Entity<EmpDept>()
               .HasOne(ed => ed.Employee)
               .WithMany(e => e.EmpDepts)
               .HasForeignKey(ed => ed.EmployeeId)
               .OnDelete(DeleteBehavior.Restrict); // This prevents cascading deletes for Employee

            builder.Entity<EmpDept>()
                .HasOne(ed => ed.Department)
                .WithMany(d => d.EmpDepts)
                .HasForeignKey(ed => ed.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);


        }


    }
}
