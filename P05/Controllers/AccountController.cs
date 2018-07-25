using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P05.Models;
using System.Security.Claims;
using System.Data;

namespace P05.Controllers
{
	public class AccountController : Controller
	{
		private static string AuthScheme = "UserSecurity";

		// TODO Task 1c: Examine the following Login HttpGet and HttpPost and write a description on how the Login process works in the worksheet
		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			if (returnUrl.Contains("SingRoom"))
				ViewData["Layout"] = "_SingRoomLayout";
			else
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
				if (returnUrl.Contains("SingRoom"))
				{
					HttpContext.Authentication.SignInAsync(AuthScheme, principal);
					return RedirectToAction("Index", "SingRoom");
				}
				else
				{
					HttpContext.Authentication.SignInAsync(AuthScheme, principal);
					return RedirectToAction("Index", "PetHotel");
				}
			}
			ViewData["Message"] = "Incorrect User ID or Password";

			if (returnUrl.Contains("SingRoom"))
				ViewData["Layout"] = "_SingRoomLayout";
			else
				ViewData["Layout"] = "_Layout";

			return View("Login");
		}

		// TODO Task 1g: Examine the Logoff code and describe how the code log off user. Answer in worksheet.
		public IActionResult Logoff(string returnUrl = null)
		{
			HttpContext.Authentication.SignOutAsync(AuthScheme);
			return RedirectToAction("Index", "SingRoom");
		}

		public IActionResult Forbidden()
		{
			return View();
		}

		// TODO Task 1e: Examine the code in SecureValidUser and describe how the code authenticates the SingRoom user. Answer in the worksheet.
		private bool SecureValidUser(string uid,
									 string pw,
									 out ClaimsPrincipal principal)
		{
			string sql = "";
			string returnUrl = ViewData["ReturnUrl"] as string;
			if (returnUrl.Contains("SingRoom"))
				sql = @"SELECT * FROM SRUser 
                         WHERE Email='{0}' 
                           AND Password = HASHBYTES('SHA1','{1}')";
			else
				sql = @"SELECT * FROM PHUser WHERE Email='{0}' AND Password = HASHBYTES('SHA','{1}')";

			DataTable ds = DBUtl.GetTable(sql, uid, pw);

			principal = null;
			if (ds.Rows.Count == 1)
			{
				string uname = ds.Rows[0]["Name"].ToString();
				string userid = ds.Rows[0]["Id"].ToString();
				principal =
				   new ClaimsPrincipal(
				   new ClaimsIdentity(
					  new Claim[] {
					 new Claim(ClaimTypes.NameIdentifier,userid),
					 new Claim(ClaimTypes.Name, uname),
					 new Claim(ClaimTypes.Role, "member")
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