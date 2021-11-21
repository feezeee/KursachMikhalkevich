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
    public class PartnerCompanyController : Controller
    {
        private ApplicationDbContext _context;
        public PartnerCompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public ViewResult List()
        {
            var partnerCompanies = _context.PartnerCompanies.Include(t => t.Practices).Select(t => t);
            return View(partnerCompanies);
        }


        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ViewResult Create()
        {
            var partnerCompany = new PartnerCompany();
            return View(partnerCompany);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Create(PartnerCompany partnerCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partnerCompany);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(partnerCompany);
        }

        public IActionResult CheckName(int? Id, string? Name)
        {

            if (Id == null && Name != null)
            {
                var res1 = _context.PartnerCompanies.Where(t => t.Name == Name).FirstOrDefault();
                if (res1 == null)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else if (Name != null)
            {
                var res1 = _context.PartnerCompanies.Where(t => t.Id == Id).FirstOrDefault();
                var res2 = _context.PartnerCompanies.Where(t => t.Name == Name).FirstOrDefault();
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
            var partnerCompany = _context.PartnerCompanies.Include(t => t.Practices).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            if (partnerCompany != null)
            {
                return View(partnerCompany);
            }
            return RedirectToAction("List");

        }


        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(PartnerCompany partnerCompany)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(partnerCompany).State = EntityState.Modified;
                await _context.SaveChangesAsync();                
                return RedirectToAction("List");
            }
            partnerCompany.Practices = _context.Practices.Where(t => t.PartnerCompanyId == partnerCompany.Id).ToList();
            return View(partnerCompany);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public IActionResult Delete(int? id)
        {
            var partnerCompany = _context.PartnerCompanies.Find(id);
            if (partnerCompany == null)
            {
                return RedirectToAction("List");
            }

            return View(partnerCompany);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Администратор")]
        public IActionResult DeleteConfirmed(int? id)
        {
            var partnerCompany = _context.PartnerCompanies.Find(id);
            if (partnerCompany == null)
            {
                return RedirectToAction("List");
            }
            _context.PartnerCompanies.Remove(partnerCompany);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
