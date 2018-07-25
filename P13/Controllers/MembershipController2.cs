using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P13.Models;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P13.Controllers
{
    public class MembershipController : Controller
    {
        private AppDbContext _dbContext;

        public MembershipController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            DbSet<Member> dbs = _dbContext.Member;
            List<Member> model = dbs.ToList();
            return View(model);
        }

        public IActionResult Update(int id)
        {
            // To be implemented: Follow the requirments stated below:
            // Assuming id = 186
            // 1. If the member with id cannot be found then redirect to index action with message "Member 186 not found!"
            // 2. If the member with id can be found then
            //    a. Prepare a list of text and value for the purpose of allowing selection of happiness levels. The value maps to Id while text maps to Level of each HappinessLevel object. This list should be stored in ViewData under the key "levels"
            //    b. Return the Update view and pass the found member object to it

            // The code below has to be replaced
            ViewData["levels"] = new List<dynamic>();
            return View();
        }

        [HttpPost]
        public ActionResult Update(Member selMember)
        {
            // To be implemented: Follow the requirements stated in the worksheet

            // The code below has to be replaced
            TempData["Msg"] = "Pending implementation";
            return RedirectToAction("Index");
        }
    }
}

