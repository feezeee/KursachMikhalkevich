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
    public class WorkerController : Controller
    {
        private ApplicationDbContext _context;
        public WorkerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult List()
        {
            var workers = _context.Workers.Include(t => t.Position).Include(t => t.Qualification).Include(t => t.AccessRight).Include(t=>t.Subjects).Select(t => t);
            return View(workers);
        }


        [HttpGet]
        public ViewResult Create()
        {
            ViewBag.Positions = new SelectList(_context.Positions, "Id", "Name");
            ViewBag.Qualifications = new SelectList(_context.Qualifications, "Id", "Name");
            ViewBag.AccessRights = new SelectList(_context.AccessRights, "Id", "Name");
            Worker worker = new Worker();
            worker.Password = "admin";
            return View(worker);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewBag.Positions = new SelectList(_context.Positions, "Id", "Name");
            ViewBag.Qualifications = new SelectList(_context.Qualifications, "Id", "Name");
            ViewBag.AccessRights = new SelectList(_context.AccessRights, "Id", "Name");
            return View(worker);
        }

        public IActionResult CheckEmail(int? Id, string? Email)
        {
            
            if (Id == null && Email != null)
            {
                var res1 = _context.Workers.Where(t => t.Email == Email).FirstOrDefault();
                if (res1 == null)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else if(Email != null)
            {
                var res1 = _context.Workers.Where(t => t.Id == Id).FirstOrDefault();
                var res2 = _context.Workers.Where(t => t.Email == Email).FirstOrDefault();
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
            Worker worker = _context.Workers.Include(t => t.Position).Include(t => t.Qualification).Include(t => t.AccessRight).Include(t => t.Subjects).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            if (worker != null)
            {
                ViewBag.Positions = new SelectList(_context.Positions, "Id", "Name");
                ViewBag.Qualifications = new SelectList(_context.Qualifications, "Id", "Name");
                ViewBag.AccessRights = new SelectList(_context.AccessRights, "Id", "Name");
                return View(worker);
            }
            return RedirectToAction("List");

        }


        [HttpPost]
        public async Task<IActionResult> Edit(Worker worker)
        {
            if (ModelState.IsValid)
            {
                if (worker.Id == AuthorizedUser.GetUser()?.Id)
                {
                    worker.Position = _context.Positions.Find(worker.PositionId);
                    worker.AccessRight = _context.AccessRights.Find(worker.AccessRightId);
                    worker.Qualification = _context.Qualifications.Find(worker.QualificationId);
                    AuthorizedUser.SetUser(worker);
                }
                _context.Entry(worker).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            ViewBag.Positions = new SelectList(_context.Positions, "Id", "Name");
            ViewBag.Qualifications = new SelectList(_context.Qualifications, "Id", "Name");
            ViewBag.AccessRights = new SelectList(_context.AccessRights, "Id", "Name");
            worker.Subjects = _context.Subjects.Where(t => t.WorkerId == worker.Id).Select(t => t).ToList();
            return View(worker);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Worker worker = _context.Workers.Find(id);
            if (worker == null)
            {
                return RedirectToAction("List");
            }
            else if (worker?.Id == AuthorizedUser.GetUser()?.Id)
            {
                return RedirectToAction("List");
            }

            return View(worker);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Worker worker = _context.Workers.Find(id);
            if (worker == null)
            {
                return RedirectToAction("List");
            }
            else if (worker?.Id == AuthorizedUser.GetUser()?.Id)
            {
                return RedirectToAction("List");
            }
            _context.Workers.Remove(worker);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
