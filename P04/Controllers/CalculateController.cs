using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace P04.Controllers
{
    public class CalculateController : Controller
    {
        delegate string fS(string s1, string s2);

        public string StringOp(string op, string a, string b)
        {
            fS f = null;

            if (op.Equals("c", StringComparison.InvariantCultureIgnoreCase))
                f = (s1, s2) => s1 + s2;
            else if (op.Equals("r", StringComparison.InvariantCultureIgnoreCase))
                f = (s1, s2) => s1.Replace(s2, "");
            if (f == null)
                return "No such operation!";
            else
            {
                string result = f(a, b);
                return result;
            }
        }

		// TODO Task 1: Create a delegate that takes in 2 double parameters and return 1 double value
		// TODO Task 2: Implement the Evaluate action to perform the 4 basic arithmetic operation based on op:
		//  When op is sum, perform p1 + p2
		//  When op is dif, perform p1 - p2
		//  When op is prd, perform p1 * p2
		//  When op is div, perform p1 / p2 
		delegate double operation(double p1, double p2);

        public string Evaluate(string op, double x, double y)
        {
			operation f = null;

			if (op.Equals("sum"))
				f = (p1, p2) => p1 + p2;
			else if (op.Equals("dif"))
				f = (p1, p2) => p1 - p2;
			else if (op.Equals("prd"))
				f = (p1, p2) => p1 * p2;
			else if (op.Equals("div"))
				f = (p1, p2) => p1 / p2;
			if (f == null)
			{
				return "No such operation";
			}
			else
			{
				double result = f(x, y);
				return result.ToString();
			}
        }
    }
}
