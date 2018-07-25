using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P11.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using Microsoft.AspNetCore.Authorization;

namespace Week11.Controllers
{
    [Route("api/HESSAP")]
    public class HESSAPApiController : Controller
    {
        private AppDbContext _dbContext;

        public HESSAPApiController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("Semesters/{Id}")]
	[Authorize()]
        public IActionResult GetAllSemesters(int id)
        {
            //To be implemented: Return list of semesters where there are results, should be a list of integer, in ascending order
	    //HINT: Use id to filter results belongs to a specific student id
            //HINT: Query against _dbContext.Result
            //HINT: Use group by to achieve the result
            //HINT: Use Select to project to key of the grouping

            //To be implemented: Return list of semester with Http response code ok
            return BadRequest(); // this line has to be modified
        }

    }
}