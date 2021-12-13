using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task10.Models;
namespace Task10.Services.Interfaces
{
    public interface IStudentService
    {
        List<Student> GetStudentsList();
        Student GetStudentById(int id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudentById(int id);
    }
}
