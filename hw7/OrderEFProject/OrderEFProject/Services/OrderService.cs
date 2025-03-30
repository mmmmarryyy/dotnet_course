using OrderEFProject.Model;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace OrderEFProject.Services
{
    public class OrderService
    {
        public void AddOrder(Order order)
        {
            using (var context = new OrderDBEntities())
            {
                context.Order.Add(order);
                context.SaveChanges();
            }
        }

        public Order GetOrderNoTracking(int id)
        {
            using (var context = new OrderDBEntities())
            {
                return context.Order.AsNoTracking().FirstOrDefault(o => o.o_id == id);
            }
        }

        public void UpdateOrder(Order order)
        {
            using (var context = new OrderDBEntities())
            {
                context.Order.Attach(order);
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteOrder(int id)
        {
            using (var context = new OrderDBEntities())
            {
                var order = context.Order.Find(id);
                if (order != null)
                {
                    context.Order.Remove(order);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteAllOrders()
        {
            using (var context = new OrderDBEntities())
            {
                context.Database.Log = log => {
                    try
                    {
                        System.IO.File.AppendAllText("../../ef_log.txt", log + Environment.NewLine);
                    } 
                    catch
                    {
                        Console.WriteLine("ERROR: can't write to file");
                    }
                    };
                var orders = context.Order.ToList();
                context.Order.RemoveRange(orders);
                context.SaveChanges();
            }
        }
    }
}
