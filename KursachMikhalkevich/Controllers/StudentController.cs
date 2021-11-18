using KursachMikhalkevich.Data;
using KursachMikhalkevich.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult List()
        {
            var students = _context.Students.Include(t => t.Practice).Include(t => t.Group).Include(t => t.Worker).Select(t => t);

            return View(students);
        }


        [HttpGet]
        public ViewResult Create()
        {
            Student student = new Student();
            ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name");
            ViewBag.PartnerCompanies = new SelectList(_context.PartnerCompanies, "Id", "Name");
            ViewBag.Practices = new SelectList(_context.Practices, "Id", "Name");

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var el in _context.Workers.Select(t => t))
            {
                list.Add(new SelectListItem { Text = $"{el.FIO()}", Value = el.Id.ToString() });
            }

            ViewBag.Workers = list;
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name");
            ViewBag.PartnerCompanies = new SelectList(_context.PartnerCompanies, "Id", "Name");
            ViewBag.Practices = new SelectList(_context.Practices, "Id", "Name");

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var el in _context.Workers.Select(t => t))
            {
                list.Add(new SelectListItem { Text = $"{el.FIO()}", Value = el.Id.ToString() });
            }

            ViewBag.Workers = list;
            return View(student);
        }

        //public IActionResult CheckName(int? Id, string? Name)
        //{

        //    if (Id == null && Name != null)
        //    {
        //        var res1 = _context.Qualifications.Where(t => t.Name == Name).FirstOrDefault();
        //        if (res1 == null)
        //        {
        //            return Json(true);
        //        }
        //        return Json(false);
        //    }
        //    else if (Name != null)
        //    {
        //        var res1 = _context.Qualifications.Where(t => t.Id == Id).FirstOrDefault();
        //        var res2 = _context.Qualifications.Where(t => t.Name == Name).FirstOrDefault();
        //        if (res2 == null || res1.Id == res2.Id)
        //        {
        //            return Json(true);
        //        }
        //        return Json(false);
        //    }
        //    return Json(false);

        //}


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("List");
            }
            Student student = _context.Students.Include(t => t.Practice).Include(t => t.Group).Include(t=>t.Worker).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name");
            ViewBag.PartnerCompanies = new SelectList(_context.PartnerCompanies, "Id", "Name");
            ViewBag.Practices = new SelectList(_context.Practices, "Id", "Name");

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var el in _context.Workers.Select(t => t))
            {
                list.Add(new SelectListItem { Text = $"{el.FIO()}", Value = el.Id.ToString() });
            }

            ViewBag.Workers = list;
            if (student != null)
            {
                return View(student);
            }
            return RedirectToAction("List");

        }


        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name");
            ViewBag.PartnerCompanies = new SelectList(_context.PartnerCompanies, "Id", "Name");
            ViewBag.Practices = new SelectList(_context.Practices, "Id", "Name");

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var el in _context.Workers.Select(t => t))
            {
                list.Add(new SelectListItem { Text = $"{el.FIO()}", Value = el.Id.ToString() });
            }

            ViewBag.Workers = list;
            return View(student);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Student student = _context.Students.Include(t => t.Practice).Include(t => t.Group).Include(t => t.Worker).Where(t => t.Id == id).FirstOrDefault();
            if (student == null)
            {
                return RedirectToAction("List");
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Student student = _context.Students.Include(t => t.Practice).Include(t => t.Group).Include(t => t.Worker).Where(t => t.Id == id).FirstOrDefault();
            if (student == null)
            {
                return RedirectToAction("List");
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
