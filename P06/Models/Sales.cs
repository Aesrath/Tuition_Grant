using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P06.Models
{
    public class Sales
    {
        public List<Order> GetOrderHistorySimplified()
        {
            List<Order> orders = new List<Order>();
            orders.Add(new Order() { ProductId = "ST0001", Category = "A", Price = 12.50, Qty = 5 });
            orders.Add(new Order() { ProductId = "ST0032", Category = "B", Price = 11.60, Qty = 7 });
            orders.Add(new Order() { ProductId = "CU0686", Category = "C", Price = 10.70, Qty = 3 });
            orders.Add(new Order() { ProductId = "CU0773", Category = "A", Price = 22.90, Qty = 9 });
            orders.Add(new Order() { ProductId = "PP1094", Category = "B", Price = 3.10, Qty = 2 });
            orders.Add(new Order() { ProductId = "GU0773", Category = "C", Price = 22.90, Qty = 5 });
            orders.Add(new Order() { ProductId = "JU0773", Category = "A", Price = 22.90, Qty = 5 });

            return orders;
        }

    }
}