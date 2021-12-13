using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;
using Task10.Services.Interfaces;
using Task10.Services;

namespace Task10.Controllers
{
    public class StudentsController : Controller
    {
        StudentService _service;
        public StudentsController(IStudentService service)
        {
            _service = (StudentService)service;
        }

        // GET: Students
        public IActionResult Index(int? id)
        {
            if ((int?)TempData["GroupID"] != null)
            {
                id = (int)TempData["GroupID"];
            }
            if (id == null)
            {
                return View(_service.GetStudentsList());
            }
            ViewBag.GroupID = id;
            var group = _service.GetGroup((int)id);
            ViewBag.CourseId = group.CourseId;
            ViewBag.GroupName = group.Name;


            return View(_service.StudentsByGroup((int)id));
        }

        // GET: Students/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _service.GetStudentById((int)id);
            if (student == null)
            {
                return NotFound();
            }
            TempData["GroupId"] = student.GroupID;
            ViewBag.GroupName = _service.GetGroup(student.GroupID).Name;
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create(int? id)
        {
            ViewBag.GroupID = id;
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int? id,[Bind("Name,LastName,GroupID")] Student student)
        {
            ViewBag.GroupID = id;
            if (ModelState.IsValid)
            {
                student.GroupID = (int)id;
                _service.AddStudent(student);
                TempData["GroupID"] = id;
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student =_service.GetStudentById((int)id);
            if (student == null)
            {
                return NotFound();
            }
            TempData["GroupID"] = student.GroupID;
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,LastName,GroupID")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }
            student.GroupID = (int)TempData["GroupID"];
            if (ModelState.IsValid)
            {    
                try
                {
                    _service.UpdateStudent(student);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _service.GetStudentById((int)id);
            if (student == null)
            {
                return NotFound();
            }
            TempData["GroupID"] = student.GroupID;
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TempData["GroupID"] = _service.GetStudentById((int)id).GroupID;
            _service.DeleteStudentById((int)id);
            return RedirectToAction(nameof(Index));
        }


    }
}
