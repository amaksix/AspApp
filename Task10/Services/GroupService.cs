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
    public class GroupService : IGroupService 
    {
        private CourseContext _context;
       // private CoursesService _courseService;
        public GroupService(CourseContext context)
        {
            _context = context;
          //  _courseService = courseService;
        }

        public List<Group> GetGroupsList()
        {
            return _context.Group.ToList();
        }

        public Group GetGroupById(int id)
        {
            return _context.Group.Find(id);
        }

        public void AddGroup(Group group)
        {
            _context.Add(group);
            _context.SaveChanges();
        }

        public void UpdateGroup(Group group)
        {
            _context.Update(group);
            _context.SaveChanges();
        }

        public bool DeleteGroupById(int id)
        {
           if ( _context.Student.FirstOrDefault(g => g.GroupID == id) == null)
           {
                var group = _context.Group.Find(id);
                _context.Group.Remove(group);
                _context.SaveChanges();
                return true;
            }
           else
            {
                return false;
            }
           
        }

        public string GetCourseName(int id)
        {
            var course = _context.Course.Find(id);
            return course.Name;
        }

        public IQueryable<Group> GroupsByCourse(int courseId)
        {
            var groups = from g in _context.Group
                         select g;
            groups = groups.Where(s => s.CourseId == courseId);
            return groups;
        }
        public bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.Id == id);
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
