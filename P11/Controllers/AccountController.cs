using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P11.Models;
using System.Security.Claims;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace P11.Controllers
{
    public class AccountController : Controller
    {
        private static string AuthScheme = "UserSecurity";
        private AppDbContext _dbContext;

        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["Layout"] = "_Layout";
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUser user,
                                    string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ClaimsPrincipal principal = null;
            if (SecureValidUser(user.UserId, user.Password, out principal))
            {
                HttpContext.Authentication.SignInAsync(AuthScheme, principal);
                return RedirectToAction("Index", "HESSAP");
            }
            else
            {
                ViewData["Message"] = "Incorrect User ID or Password";
                ViewData["Layout"] = "_Layout";
                return View("Login");
            }
        }

        public IActionResult Logoff(string returnUrl = null)
        {
            HttpContext.Authentication.SignOutAsync(AuthScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Forbidden(string returnUrl = null)
        {
            ViewData["Layout"] = "_Layout";
            return View();
        }

        private bool SecureValidUser(string uid,
                                     string pw,
                                     out ClaimsPrincipal principal)
        {
            string returnUrl = ViewData["ReturnUrl"] as string;
            
	    string sql = "";
            sql = $"SELECT * FROM Student WHERE Id='{uid}' AND Password = HASHBYTES('SHA1','{pw}')";

            DbSet<Student> dbs = _dbContext.Student;
            Student student = dbs.FromSql(sql)
                                  .FirstOrDefault();


            principal = null;
            if (student != null)
            {
                principal =
                   new ClaimsPrincipal(
                   new ClaimsIdentity(
                      new Claim[] {
                     new Claim(ClaimTypes.NameIdentifier,student.Id.ToString()),
                     new Claim(ClaimTypes.Name, student.Name),
                     new Claim(ClaimTypes.Role, student.IsAdmin ? "Admin":"Student")
                      },
                      "Basic"));
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}