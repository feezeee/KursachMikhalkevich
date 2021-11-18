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
    public class SubjectController : Controller
    {
        private ApplicationDbContext _context;
        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult List()
        {
            var students = _context.Subjects.Include(t => t.Worker).Include(t => t.Groups).Select(t => t);

            return View(students);
        }


        [HttpGet]
        public ViewResult Create()
        {
            Subject subject = new Subject();

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var el in _context.Workers.Select(t => t))
            {
                list.Add(new SelectListItem { Text = $"{el.FIO()}", Value = el.Id.ToString() });
            }

            ViewBag.Workers = list;
            return View(subject);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var el in _context.Workers.Select(t => t))
            {
                list.Add(new SelectListItem { Text = $"{el.FIO()}", Value = el.Id.ToString() });
            }

            ViewBag.Workers = list;
            return View(subject);
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
            Subject subject = _context.Subjects.Include(t => t.Worker).Include(t => t.Groups).Where(t => t.Id == id).Select(t => t).FirstOrDefault();

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var el in _context.Workers.Select(t => t))
            {
                list.Add(new SelectListItem { Text = $"{el.FIO()}", Value = el.Id.ToString() });
            }

            ViewBag.Workers = list;

            if (subject != null)
            {
                return View(subject);
            }
            return RedirectToAction("List");

        }


        [HttpPost]
        public async Task<IActionResult> Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(subject).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var el in _context.Workers.Select(t => t))
            {
                list.Add(new SelectListItem { Text = $"{el.FIO()}", Value = el.Id.ToString() });
            }

            ViewBag.Workers = list;
            return View(subject);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Subject subject = _context.Subjects.Include(t => t.Worker).Include(t => t.Groups).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            if (subject == null)
            {
                return RedirectToAction("List");
            }

            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Subject subject = _context.Subjects.Include(t => t.Worker).Include(t => t.Groups).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            if (subject == null)
            {
                return RedirectToAction("List");
            }

            _context.Subjects.Remove(subject);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
