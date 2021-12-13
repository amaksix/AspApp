using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task10.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Task10.Models
{
    public class SetData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
           

            using (var context = new CourseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CourseContext>>()))
            {
                context.Database.EnsureCreated();
                // Look for any movies.
                if (context.Course.Any())
                {
                    return;   // DB has been seeded
                }
                var courses = new Course[] {
                    new Course { Name = "C#" },
                    new Course { Name = "Java" },
                    new Course { Name = "Python" },
                    new Course { Name = "C++" }
                };
                foreach (Course c in courses)
                {
                    context.Course.Add(c);
                }
                context.SaveChanges();

                var groups = new Group[]{
                   new Group { Name = "C#_1", CourseId = courses.Single(c => c.Name == "C#").Id },
                   new Group { Name = "C#_2", CourseId = courses.Single(c => c.Name == "C#").Id },
                   new Group { Name = "Java_1", CourseId = courses.Single(c => c.Name == "Java").Id },
                   new Group { Name = "Java_2", CourseId = courses.Single(c => c.Name == "Java").Id },
                   new Group { Name = "Python_1", CourseId = courses.Single(c => c.Name == "Python").Id },
                   new Group { Name = "Python_2", CourseId = courses.Single(c => c.Name == "Python").Id },
                   new Group { Name = "C++_1", CourseId = courses.Single(c => c.Name == "C++").Id },
                   new Group { Name = "C++_2", CourseId = courses.Single(c => c.Name == "C++").Id }
                };

                foreach (Group g in groups)
                {
                    context.Group.Add(g);
                }
                context.SaveChanges();
                context.Student.AddRange(
                    new Student { Name = "Andrei", LastName = "Morozov",GroupID = groups.Single(c => c.Name == "C#_1").Id },
                    new Student { Name = "Andrei", LastName = "Kuznetsov", GroupID = groups.Single(c => c.Name == "C#_1").Id },
                    new Student { Name = "Anna", LastName = "Kuznetsova", GroupID = groups.Single(c => c.Name == "C#_1").Id },
                    new Student { Name = "Maksim", LastName = "Ulanov", GroupID = groups.Single(c => c.Name == "C#_2").Id },
                    new Student { Name = "Ivan", LastName = "Ivanov", GroupID = groups.Single(c => c.Name == "C#_2").Id },
                    new Student { Name = "Anna", LastName = "Andrevna", GroupID = groups.Single(c => c.Name == "Java_1").Id }
                );
                context.SaveChanges();
            }
        }
    }
}
