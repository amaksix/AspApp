using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task10.Models;
using Task10.Data;
using Task10.Services.Interfaces;
namespace Task10.Services
{
    public class StudentService : IStudentService
    {
        private CourseContext _context;
        public StudentService(CourseContext context)
        {
            _context = context;
        }

        public List<Student> GetStudentsList()
        {
            return _context.Student.ToList();
        }

        public Student GetStudentById(int id)
        {
            return _context.Student.Find(id);
        }

        public void AddStudent(Student student)
        {
            _context.Add(student);
            _context.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            _context.Update(student);
            _context.SaveChanges();
        }

        public void DeleteStudentById(int id)
        {
            var student = _context.Student.Find(id);
            _context.Student.Remove(student);
            _context.SaveChanges();
        }

        public bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }

        public IQueryable<Student> StudentsByGroup(int groupId)
        {
            var students = from s in _context.Student
                         select s;
            students = students.Where(s => s.GroupID == groupId);
            return students;
        }
        public Group GetGroup(int id)
        {
            var group = _context.Group.Find(id);
            return group;
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
