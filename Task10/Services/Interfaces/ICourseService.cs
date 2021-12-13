using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task10.Models;

namespace Task10.Services.Interfaces
{
    public interface ICourseService : IDisposable
    {
        List<Course> GetCoursesList();
        Course GetCourseById(int id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourseById(int id);
    }
}
