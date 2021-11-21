using KursachMikhalkevich.Data;
using KursachMikhalkevich.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Controllers
{
    public class SubjectGroupController : Controller
    {
        private ApplicationDbContext _context;
        public SubjectGroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public ViewResult List()
        {
            var groups = _context.Groups.Include(t => t.Subjects).Include(t => t.Students).Select(t => t);
            return View(groups);
        }

        [Authorize]
        public IActionResult Information(int? id)
        {
            Group group = _context.Groups.Where(t => t.Id == id).Include(t => t.SubjectsGroups).Include(t => t.Subjects).Include(t => t.Students).FirstOrDefault();
            if (group == null)
            {
                return RedirectToAction("List");
            }

            return View(group);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ViewResult Create(int groupId)
        {
            SubjectGroup subjectGroup = new SubjectGroup();
            subjectGroup.GroupId = groupId;
            ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
            return View(subjectGroup);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Create(SubjectGroup subjectGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectGroup);

                await _context.SaveChangesAsync();
                return RedirectToAction("Information", "Group", new { id = subjectGroup.GroupId });
            }
            return View(subjectGroup);
        }

        public IActionResult CheckName(int? Id, string? Name)
        {

            if (Id == null && Name != null)
            {
                var res1 = _context.Groups.Where(t => t.Name == Name).FirstOrDefault();
                if (res1 == null)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else if (Name != null)
            {
                var res1 = _context.Groups.Where(t => t.Id == Id).FirstOrDefault();
                var res2 = _context.Groups.Where(t => t.Name == Name).FirstOrDefault();
                if (res2 == null || res1.Id == res2.Id)
                {
                    return Json(true);
                }
                return Json(false);
            }
            return Json(false);

        }


        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public IActionResult Edit(int? id, int? groupId)
        {
            if (id == 0 || id == null)
            {
                return RedirectToAction("Information", "Group", new { id = groupId });
            }
            var subjectGroup = _context.SubjectGroups.Where(t => t.Id == id).FirstOrDefault();
            if (subjectGroup != null)
            {

                ViewBag.Subjects = new SelectList(_context.Subjects, "Id", "Name");
                return View(subjectGroup);
            }

            return RedirectToAction("Information", "Group", new { id = groupId });
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(SubjectGroup subjectGroup)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(subjectGroup).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Information", "Group", new { id = subjectGroup.GroupId });
            }
            ViewBag.Subjects = _context.Subjects;
            return View(subjectGroup);
        }



        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public IActionResult Delete(int? id, int? groupId)
        {

            var subjectGroup = _context.SubjectGroups.Where(t => t.Id == id).FirstOrDefault();
            if (subjectGroup == null)
            {
                return RedirectToAction("Information","Group", new {id = groupId});
            }

            return View(subjectGroup);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Администратор")]
        public IActionResult DeleteConfirmed(int? id, int? groupId)
        {
            var subjectGroup = _context.SubjectGroups.Where(t => t.Id == id).FirstOrDefault();
            if (subjectGroup == null)
            {
                return RedirectToAction("Information", "Group", new { id = groupId });
            }

            _context.SubjectGroups.Remove(subjectGroup);
            _context.SaveChanges();
            return RedirectToAction("Information", "Group", new { id = groupId });
        }
    }
}
