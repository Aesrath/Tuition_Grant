using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P12.Models;
using System.Security.Claims;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace P12.Controllers
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
                return RedirectToAction("Index", "Home");
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
            sql = $"SELECT * FROM Staff WHERE Id='{uid}' AND Password = HASHBYTES('SHA1','{pw}')";

            DbSet<Staff> dbs = _dbContext.Staff;
            Staff staff = dbs.FromSql(sql)
                                  .FirstOrDefault();


            principal = null;
            if (staff != null)
            {
                principal =
                   new ClaimsPrincipal(
                   new ClaimsIdentity(
                      new Claim[] {
                     new Claim(ClaimTypes.NameIdentifier,staff.Id.ToString()),
                     new Claim(ClaimTypes.Name, staff.Name),
                     new Claim(ClaimTypes.Role, "staff")
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