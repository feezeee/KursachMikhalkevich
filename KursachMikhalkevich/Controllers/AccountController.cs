using KursachMikhalkevich.Data;
using KursachMikhalkevich.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KursachMikhalkevich.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                Worker user = await _context.Workers
                    .Include(t => t.Qualification)
                    .Include(t => t.AccessRight)
                    .Include(t => t.Position)
                    .Where(t => t.Email == model.Email && t.Password == model.Password)
                    .FirstOrDefaultAsync();

                if (user != null)
                {

                    await Authenticate(user); // аутентификация
                    AuthorizedUser.ClearUser();
                    AuthorizedUser.SetUser(user);
                    return RedirectToAction("Index", "Home");
                    
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await HttpContext.SignOutAsync();
            AuthorizedUser.ClearUser();
            return RedirectToAction("Login", "Account");
        }
        private async Task Authenticate(Worker worker)
        {
            // создаем один claim
            var claims = new List<Claim>
            {                
                new Claim(ClaimsIdentity.DefaultNameClaimType, worker.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, worker?.AccessRight?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
