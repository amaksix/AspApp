using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task10.Data;
using Task10.Models;
using Microsoft.Extensions.DependencyInjection;
using Task10.Services.Interfaces;

namespace Task10.Services
{
    public class CoursesService : ICourseService
    {
        private CourseContext _context;
        public CoursesService(CourseContext context)
        {
            _context = context;
        }

        public List<Course> GetCoursesList()
        {
            return _context.Course.ToList();
        }

        public Course GetCourseById(int id)
        { 
            return _context.Course.Find(id);
        }

        public void AddCourse (Course course)
        {
            _context.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            _context.Update(course);
            _context.SaveChanges();
        }

        public void DeleteCourseById(int id)
        {
            var course =_context.Course.Find(id);
            _context.Course.Remove(course);
            _context.SaveChanges();
        }

        public bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
