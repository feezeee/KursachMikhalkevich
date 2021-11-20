﻿using KursachMikhalkevich.Data;
using KursachMikhalkevich.Models;
using Microsoft.AspNetCore.Mvc;
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

        public ViewResult List()
        {
            var groups = _context.Groups.Include(t => t.Subjects).Include(t => t.Students).Select(t => t);
            return View(groups);
        }

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
        public ViewResult Create()
        {
            Group group = new Group();
            return View(group);
        }

        [HttpPost]
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
        public IActionResult Delete(int? id)
        {
            
            var subjectGroup = _context.SubjectGroups.Where(t => t.Id == id).FirstOrDefault();
            if (subjectGroup != null)
            {
                var studentCourse = group.SubjectsGroups.Where(t=>t.DateTimeStart == dateStart).FirstOrDefault(sc => sc.SubjectId == subject.Id);
                group.SubjectsGroups.Remove(studentCourse);
                _context.SaveChanges();
            }

            return RedirectToAction("Information","Group", new { id = groupId});
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? groupId, int? subjectId)
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
