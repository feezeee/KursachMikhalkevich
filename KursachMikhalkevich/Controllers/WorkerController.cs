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
            return View();
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


        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (id == 0)
        //    {
        //        return RedirectToAction("List");
        //    }
        //    Worker worker = _context.Workers.Include(t => t.Position).Include(t => t.Qualification).Include(t => t.AccessRight).Include(t => t.Lessons).Include(t => t.Classes).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
        //    if (worker != null)
        //    {
        //        ViewBag.Positions = new SelectList(_context.Positions, "Id", "Name");
        //        ViewBag.Qualifications = new SelectList(_context.Qualifications, "Id", "Name");
        //        ViewBag.AccessRights = new SelectList(_context.AccessRights, "Id", "Name");
        //        return View(worker);
        //    }
        //    return RedirectToAction("List");

        //}


        //[HttpPost]
        //public async Task<IActionResult> Edit(Worker worker)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (worker.Id == AuthorizedUser.GetInstance().GetWorker().Id)
        //        {
        //            worker.Position = _context.Positions.Find(worker.PositionId);
        //            worker.AccessRight = _context.AccessRights.Find(worker.AccessRightId);
        //            AuthorizedUser.GetInstance().ClearUser();
        //            AuthorizedUser.GetInstance().SetUser(worker);
        //        }
        //        _context.Entry(worker).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("List");
        //    }

        //    ViewBag.Positions = new SelectList(_context.Positions, "Id", "Name");
        //    ViewBag.Qualifications = new SelectList(_context.Qualifications, "Id", "Name");
        //    ViewBag.AccessRights = new SelectList(_context.AccessRights, "Id", "Name");
        //    worker.Lessons = _context.Subjects.Where(t => t.WorkerId == worker.Id).Select(t => t).ToList();
        //    worker.Classes = _context.Classes.Where(t => t.ClassroomTeacherId == worker.Id).Select(t => t).ToList();
        //    return View(worker);
        //}

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    Worker worker = _context.Workers.Find(id);
        //    if (worker == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    else if (worker?.Id == AuthorizedUser.GetInstance().GetWorker()?.Id)
        //    {
        //        return RedirectToRoute("default", new { controller = "Worker", action = "Edit", id = id });
        //    }

        //    return View(worker);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int? id)
        //{
        //    Worker worker = _context.Workers.Find(id);
        //    if (worker == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    _context.Workers.Remove(worker);
        //    _context.SaveChanges();
        //    return RedirectToAction("List");
        //}
    }
}
