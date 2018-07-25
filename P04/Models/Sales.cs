using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P04.Models
{
    public class Sales
    {
        public List<Order> GetOrderHistory()
        {
            List<Order> orders = new List<Order>();
            Order order1 = new Order() { ProductId = "ST0001", Price = 12.50, Qty = 5 };
            Order order2 = new Order() { ProductId = "ST0032", Price = 11.60, Qty = 7 };
            Order order3 = new Order() { ProductId = "CU0686", Price = 10.70, Qty = 3 };
            Order order4 = new Order() { ProductId = "CU0773", Price = 2.90, Qty = 9 };
            Order order5 = new Order() { ProductId = "PP1094", Price = 3.10, Qty = 2 };

            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);
            orders.Add(order4);
            orders.Add(order5);

            return orders;
        }

        public List<Order> GetOrderHistorySimplified()
        {
            List<Order> orders = new List<Order>();
            orders.Add(new Order() { ProductId = "ST0001", Price = 12.50, Qty = 5 });
            orders.Add(new Order() { ProductId = "ST0032", Price = 11.60, Qty = 7 });
            orders.Add(new Order() { ProductId = "CU0686", Price = 10.70, Qty = 3 });
            orders.Add(new Order() { ProductId = "CU0773", Price = 22.90, Qty = 9 });
            orders.Add(new Order() { ProductId = "PP1094", Price = 3.10, Qty = 2 });
            orders.Add(new Order() { ProductId = "GU0773", Price = 22.90, Qty = 5 });
            orders.Add(new Order() { ProductId = "JU0773", Price = 22.90, Qty = 5 });

            return orders;
        }

    }
}