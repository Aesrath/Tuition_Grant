using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P13.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductId { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
    }
}


