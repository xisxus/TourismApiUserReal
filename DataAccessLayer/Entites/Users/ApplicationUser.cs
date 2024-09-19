using DataAccessLayer.Entites.Employees;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entites.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Person> People { get; set; }
    }
}
