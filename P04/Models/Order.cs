﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P04.Models
{
    public class Order
    {
        public string ProductId { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
    }
}