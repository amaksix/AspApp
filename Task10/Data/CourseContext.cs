using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task10.Models;

namespace Task10.Data
{
    public class CourseContext : DbContext
    {
        public CourseContext (DbContextOptions<CourseContext> options)
            : base(options)
        {
        }

        public DbSet<Task10.Models.Course> Course { get; set; }
        public DbSet<Task10.Models.Group> Group { get; set; }
        public DbSet<Task10.Models.Student> Student { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
