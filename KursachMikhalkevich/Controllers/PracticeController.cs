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
    public class PracticeController : Controller
    {
        private ApplicationDbContext _context;
        public PracticeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public ViewResult List()
        {
            var practices = _context.Practices.Include(t => t.Students).Include(t=>t.PartnerCompany).Select(t => t);
           
            return View(practices);
        }


        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ViewResult Create()
        {
            Practice practice = new Practice();
            ViewBag.PartnerCompanies = new SelectList(_context.PartnerCompanies, "Id", "Name");
            return View(practice);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Create(Practice practice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(practice);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewBag.PartnerCompanies = new SelectList(_context.PartnerCompanies, "Id", "Name");
            return View(practice);
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
        [Authorize(Roles = "Администратор")]
        public IActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("List");
            }
            Practice practice = _context.Practices.Include(t => t.Students).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            ViewBag.PartnerCompanies = new SelectList(_context.PartnerCompanies, "Id", "Name");
            if (practice != null)
            {
                return View(practice);
            }
            return RedirectToAction("List");

        }


        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(Practice practice)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(practice).State = EntityState.Modified;
                await _context.SaveChangesAsync();                
                return RedirectToAction("List");
            }
            practice.Students = _context.Students.Where(t => t.PracticeId == practice.Id).ToList();
            ViewBag.PartnerCompanies = new SelectList(_context.PartnerCompanies, "Id", "Name");
            return View(practice);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public IActionResult Delete(int? id)
        {
            Practice practice = _context.Practices.Include(t => t.Students).Where(t => t.Id == id).FirstOrDefault();
            if (practice == null)
            {
                return RedirectToAction("List");
            }
            else if (practice.Students?.Count != 0)
            {
                return RedirectToAction("List");
            }

            return View(practice);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Администратор")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Practice practice = _context.Practices.Include(t => t.Students).Where(t => t.Id == id).FirstOrDefault();
            if (practice == null)
            {
                return RedirectToAction("List");
            }
            else if (practice.Students?.Count != 0)
            {
                return RedirectToAction("List");
            }

            _context.Practices.Remove(practice);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
