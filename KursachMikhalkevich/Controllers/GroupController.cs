using KursachMikhalkevich.Data;
using KursachMikhalkevich.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext _context;
        public GroupController(ApplicationDbContext context)
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
        public ViewResult Create()
        {
            Group group = new Group();
            return View(group);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Create(Group group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(group);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(group);
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
        public IActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("List");
            }
            Group group = _context.Groups.Include(t => t.Subjects).Include(t => t.Students).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            
            if (group != null)
            {
                return View(group);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(Group group)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(group).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }            
            return View(group);
        }



        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public IActionResult Delete(int? id)
        {
            Group group = _context.Groups.Include(t => t.Subjects).Include(t => t.Students).Where(t => t.Id == id).FirstOrDefault();
            if (group == null)
            {
                return RedirectToAction("List");
            }

            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Администратор")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Group group = _context.Groups.Include(t => t.Subjects).Include(t => t.Students).Where(t => t.Id == id).FirstOrDefault();
            if (group == null)
            {
                return RedirectToAction("List");
            }

            _context.Groups.Remove(group);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
