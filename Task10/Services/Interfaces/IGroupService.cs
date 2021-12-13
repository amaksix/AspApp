using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task10.Models;

namespace Task10.Services.Interfaces
{
    public interface IGroupService : IDisposable
    {
        List<Group> GetGroupsList();
        Group GetGroupById(int id);
        void AddGroup(Group group);
        void UpdateGroup(Group group);
        bool DeleteGroupById(int id);
        IQueryable<Group> GroupsByCourse(int courseId);
        string GetCourseName(int id);
    }
}
