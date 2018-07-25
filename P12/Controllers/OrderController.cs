using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P12.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;
namespace P12.Controllers
{
    public class OrderController : Controller
    {
        private List<Order> orders = new List<Order>()
        {
            new Order { OrderId="4L9718",
                OrderDate =Utils.ConvertDate("01-01-2017"),
                ProductId ="MO051",
                Price =142.6,
                Qty =1 },

new Order { OrderId="9K4726", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="PF979", Price=97.7, Qty=23 },
new Order { OrderId="9L0330", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="SF815", Price=67.0, Qty=14 },
new Order { OrderId="2L5481", OrderDate=Utils.ConvertDate("02-01-2017"), ProductId="CZ854", Price=29.8, Qty=20 },
new Order { OrderId="9I4742", OrderDate=Utils.ConvertDate("02-01-2017"), ProductId="KZ663", Price=111.9, Qty=8 },
new Order { OrderId="9L8034", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="SF815", Price=171.0, Qty=18 },
new Order { OrderId="1L5522", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="YW373", Price=166.2, Qty=1 },
new Order { OrderId="0L8013", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="YW373", Price=167.1, Qty=44 },
new Order { OrderId="8I6046", OrderDate=Utils.ConvertDate("01-01-2017"), ProductId="CZ854", Price=135.1, Qty=11 },
new Order { OrderId="8J8888", OrderDate=Utils.ConvertDate("02-01-2017"), ProductId="MO051", Price=121.2, Qty=21 }
        };

        public string Index()
        {
            string result = "All Orders\n\n";
            foreach (Order order in orders)
            {
                result += string.Format("Order ID: {0}\nOrder Date: {1:dd/MM/yyyy}\nProduct Id: {2}\nPrice:{3:c}\nQty: {4}\n\n", order.OrderId, order.OrderDate, order.ProductId, order.Price, order.Qty);
            }
            return result;
        }

        public string GetOrder(string Id)
        {
            Order order = (from o in orders
                           where o.OrderId == Id
                           select o).FirstOrDefault();

            if (order == null)
            {
                return string.Format("Order {0} not found!", Id);
            }
            else
            {
                return string.Format("Order ID: {0}\nOrder Date: {1:dd/MM/yyyy}\nProduct Id: {2}\nPrice:{3:c}\nQty: {4}", order.OrderId, order.OrderDate, order.ProductId, order.Price, order.Qty);
            }

        }

        public string GetOrders(string Id)
        {
            DateTime orderDate;
            if (Utils.TryParseDate(Id, out orderDate))
            {
                List<Order> orders_list = (from o in orders
                                           where o.OrderDate == orderDate
                                           select o).ToList<Order>();
                string result = "Result: ";
                foreach (Order order in orders_list)
                {
                    result += string.Format("Order ID: {0}\nOrder Date: {1:dd/MM/yyyy}\nProduct Id: {2}\nPrice:{3:c}\nQty: {4}\n\n", order.OrderId, order.OrderDate, order.ProductId, order.Price, order.Qty);
                }
                return result;
            }
            else
            {
                return string.Format("Orders not found for {0}!", Id);
            }
        }

        public string GetOrdersByDate(int oyear, int omonth, int oday)
        {
            return GetOrders(string.Format("{0:00}-{1:00}-{2:00}", oday, omonth, oyear));
        }
    }
}