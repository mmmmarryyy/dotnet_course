using OrderEFProject.Model;
using System.Data.Entity;
using System.Linq;

namespace OrderEFProject.Services
{
    public class OrderItemService
    {
        public void AddOrderItem(orderitem orderItem)
        {
            using (var context = new OrderDBEntities())
            {
                context.orderitem.Add(orderItem);
                context.SaveChanges();
            }
        }

        public orderitem GetOrderItemNoTracking(int orderId, int productId)
        {
            using (var context = new OrderDBEntities())
            {
                return context.orderitem.AsNoTracking()
                    .FirstOrDefault(oi => oi.order_id == orderId && oi.product_id == productId);
            }
        }

        public void UpdateOrderItem(orderitem orderItem)
        {
            using (var context = new OrderDBEntities())
            {
                context.orderitem.Attach(orderItem);
                context.Entry(orderItem).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteOrderItem(int orderId, int productId)
        {
            using (var context = new OrderDBEntities())
            {
                var orderItem = context.orderitem.FirstOrDefault(oi => oi.order_id == orderId && oi.product_id == productId);
                if (orderItem != null)
                {
                    context.orderitem.Remove(orderItem);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteAllOrderItems()
        {
            using (var context = new OrderDBEntities())
            {
                var orderItems = context.orderitem.ToList();
                context.orderitem.RemoveRange(orderItems);
                context.SaveChanges();
            }
        }
    }
}
