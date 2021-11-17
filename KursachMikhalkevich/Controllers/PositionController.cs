using KursachMikhalkevich.Data;
using KursachMikhalkevich.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Controllers
{
    public class PositionController : Controller
    {
        private ApplicationDbContext _context;
        public PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ViewResult List()
        {
            var positions = _context.Positions.Include(t => t.Workers).Select(t => t);
            return View(positions);
        }


        [HttpGet]
        public ViewResult Create()
        {            
            Position position = new Position();
            return View(position);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Position position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(position);

                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(position);
        }

        public IActionResult CheckName(int? Id, string? Name)
        {

            if (Id == null && Name != null)
            {
                var res1 = _context.Positions.Where(t => t.Name == Name).FirstOrDefault();
                if (res1 == null)
                {
                    return Json(true);
                }
                return Json(false);
            }
            else if (Name != null)
            {
                var res1 = _context.Positions.Where(t => t.Id == Id).FirstOrDefault();
                var res2 = _context.Positions.Where(t => t.Name == Name).FirstOrDefault();
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
            Position position = _context.Positions.Include(t => t.Workers).Where(t => t.Id == id).Select(t => t).FirstOrDefault();
            if (position != null)
            {
                return View(position);
            }
            return RedirectToAction("List");

        }


        [HttpPost]
        public async Task<IActionResult> Edit(Position position)
        {
            if (ModelState.IsValid)
            {
                
                _context.Entry(position).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                if (AuthorizedUser.GetUser()?.PositionId == position.Id)
                {
                    var worker = AuthorizedUser.GetUser();
                    worker.Position = position;
                    AuthorizedUser.SetUser(worker);
                }
                return RedirectToAction("List");
            }            
            return View(position);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Position position = _context.Positions.Find(id);
            if (position == null)
            {
                return RedirectToAction("List");
            }
            else if (position?.Id == AuthorizedUser.GetUser()?.PositionId)
            {
                return RedirectToAction("List");
            }

            return View(position);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            Position position = _context.Positions.Find(id);
            if (position == null)
            {
                return RedirectToAction("List");
            }
            else if (position?.Id == AuthorizedUser.GetUser()?.PositionId)
            {
                return RedirectToAction("List");
            }
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
