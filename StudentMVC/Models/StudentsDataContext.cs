using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMVC.Models
{
    public class StudentsDataContext : DbContext
    {

        public DbSet<Student> Students { get; set; }

        public StudentsDataContext(DbContextOptions<StudentsDataContext> contextOptions)
            : base(contextOptions)
        {
            Database.EnsureCreated();
        }
    }
}
