using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Models;
using Task10.Services;
using Task10.Services.Interfaces;

namespace Task10.Controllers
{
    public class GroupsController : Controller
    {
        GroupService _service;

        public GroupsController(IGroupService service)
        {
            _service = (GroupService)service;
        }
        // GET: Groups
        public IActionResult Index(int? id)
        {
            if ((int?)TempData["CourseId"]!= null)
            {
                id = (int)TempData["CourseId"];
            }
            if (id == null)
            {
                return View(_service.GetGroupsList());
            }
            ViewBag.CourseID = id;

            ViewBag.CourseName = _service.GetCourseName((int)id);
            return View(_service.GroupsByCourse((int)id));
        }

        // GET: Groups/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = _service.GetGroupById((int)id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewBag.CourseID = group.CourseId;
            ViewBag.CourseName = _service.GetCourseName(group.CourseId);
            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create(int? id)
        {
            ViewBag.CourseID = id;
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int? id,[Bind("Name,CourseId")] Group @group)
        {
            ViewBag.CourseID = id;
            if (ModelState.IsValid)
            {
                group.CourseId = (int)id;
                _service.AddGroup(group);
                TempData["CourseId"] = id;
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = _service.GetGroupById((int) id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewBag.CourseID = group.CourseId;
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Group @group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }
            group.CourseId = (int)TempData["CourseID"];
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateGroup(group);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.GroupExists(@group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["CourseId"] = group.CourseId;
                return RedirectToAction(nameof(Index));
            }
            return View(@group);
        }

        // GET: Groups/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = _service.GetGroupById((int)id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewBag.CourseID = group.CourseId;
            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var @group = _service.GetGroupById((int)id);
            TempData["CourseId"] = group.CourseId;
            var deleted = _service.DeleteGroupById(id);
            if (deleted)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.ErrorMessage = "Delete failed. There are students left in the group";
                return View(group);
            }
            
        }
    }
}
