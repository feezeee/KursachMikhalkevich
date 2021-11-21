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
    public class QualificationController : Controller
    {
        private ApplicationDbContext _context;
        public QualificationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public ViewResult List()
        {
            var positions = _context.Qualifications.Include(t => t.Workers).Select(t => t);
            return View(positions);
        }


        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ViewResult Create()
        {
            Qualification qualification = new Qualification();
            return View(qualification);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Create(Qualification qualification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qualification);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(qualification);
        }

        public IActionResult CheckName(int? Id, string? Name)
        {

            if (Id == null && Name != null)
            {
                var res1 = _context.Qualifications.Where(t => t.Name == Name).FirstOrDefault();
                if (res1 == null)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else if (Name != null)
            {
                var res1 = _context.Qualifications.Where(t => t.Id == Id).FirstOrDefault();
                var res2 = _context.Qualifications.Where(t => t.Name == Name).FirstOrDefault();
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
            Qualification qualification = _context.Qualifications.Include(t => t.Workers).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            if (qualification != null)
            {
                return View(qualification);
            }
            return RedirectToAction("List");

        }


        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(Qualification qualification)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(qualification).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                if (AuthorizedUser.GetUser()?.QualificationId == qualification.Id)
                {
                    var worker = AuthorizedUser.GetUser();
                    worker.Qualification = qualification;
                    AuthorizedUser.SetUser(worker);
                }
                return RedirectToAction("List");
            }
            qualification.Workers = _context.Workers.Where(t => t.QualificationId == qualification.Id).ToList();
            return View(qualification);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public IActionResult Delete(int? id)
        {
            Qualification qualification = _context.Qualifications.Find(id);
            if (qualification == null)
            {
                return RedirectToAction("List");
            }
            else if (qualification?.Id == AuthorizedUser.GetUser()?.QualificationId)
            {
                return RedirectToAction("List");
            }

            return View(qualification);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Администратор")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Qualification qualification = _context.Qualifications.Find(id);
            if (qualification == null)
            {
                return RedirectToAction("List");
            }
            else if (qualification?.Id == AuthorizedUser.GetUser()?.QualificationId)
            {
                return RedirectToAction("List");
            }
            _context.Qualifications.Remove(qualification);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
