using System;
using System.Linq;
using System.Data;
using OrderEFProject;
using OrderEFProject.Services;
using OrderEFProject.Model;
using System.Security.Cryptography;

namespace OrderEFProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderService = new OrderService();
            var productService = new ProductService();
            var orderItemService = new OrderItemService();

            var order = new Order { o_id = 1, order_date = DateTime.Now };
            orderService.AddOrder(order);

            var prod = new product { p_id = 10, p_name = "Example Product", price = 99.99M };
            productService.AddProduct(prod);

            var orderItem = new orderitem { order_id = 1, product_id = 10, amount = 2, price = 99.99M };
            orderItemService.AddOrderItem(orderItem);

            Console.WriteLine("After adding objects:");
            PrintAllData();

            var fetchedOrder = orderService.GetOrderNoTracking(1);
            Console.WriteLine($"Fetched Order: ID = {fetchedOrder.o_id}, Date = {fetchedOrder.order_date}");

            var fetchedProduct = productService.GetProductNoTracking(10);
            Console.WriteLine($"Fetched Product: ID = {fetchedProduct.p_id}, Name = {fetchedProduct.p_name}, Price = {fetchedProduct.price}");

            var fetchedOrderItem = orderItemService.GetOrderItemNoTracking(1, 10);
            Console.WriteLine($"Fetched OrderItem: OrderID = {fetchedOrderItem.order_id}, ProductID = {fetchedOrderItem.product_id}, Amount = {fetchedOrderItem.amount}, Price = {fetchedOrderItem.price}");

            if (fetchedOrder.order_date.HasValue)
            {
                fetchedOrder.order_date = fetchedOrder.order_date.Value.AddDays(1);
            }
            orderService.UpdateOrder(fetchedOrder);
            Console.WriteLine("Order updated.");

            fetchedProduct.p_name = "Updated Product Name";
            productService.UpdateProduct(fetchedProduct);
            Console.WriteLine("Product updated.");

            fetchedOrderItem.amount = 3;
            orderItemService.UpdateOrderItem(fetchedOrderItem);
            Console.WriteLine("OrderItem updated.");

            Console.WriteLine("After updating objects:");
            PrintAllData();

            DemonstrateLoadingMechanisms();

            Console.WriteLine("Before deleting all records");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            orderItemService.DeleteAllOrderItems();
            productService.DeleteAllProducts();
            orderService.DeleteAllOrders();
            Console.WriteLine("All records deleted from all tables.");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void PrintAllData()
        {
            using (var context = new OrderDBEntities())
            {
                Console.WriteLine("Orders:");
                foreach (var o in context.Order)
                {
                    Console.WriteLine($"  Order ID: {o.o_id}, Date: {o.order_date}");
                }

                Console.WriteLine("Products:");
                foreach (var p in context.product)
                {
                    Console.WriteLine($"  Product ID: {p.p_id}, Name: {p.p_name}, Price: {p.price}");
                }

                Console.WriteLine("OrderItems:");
                foreach (var oi in context.orderitem)
                {
                    Console.WriteLine($"  OrderItem - OrderID: {oi.order_id}, ProductID: {oi.product_id}, Amount: {oi.amount}, Price: {oi.price}, Total: {oi.total}");
                }
            }
        }

        static void DemonstrateLoadingMechanisms()
        {
            Console.WriteLine("Demonstrating Eager and Lazy Loading:");

            using (var context = new OrderDBEntities())
            {
                var orders = context.Order.ToList();
                Console.WriteLine("Lazy Loading: Accessing navigation property triggers load:");
                foreach (var o in orders)
                {
                    Console.WriteLine($"  Order ID: {o.o_id}, Date: {o.order_date}");
                    foreach (var oi in o.orderitem)
                    {
                        Console.WriteLine($"    OrderItem - ProductID: {oi.product_id}, Amount: {oi.amount}");
                    }
                }
            }

            using (var context = new OrderDBEntities())
            {
                var ordersWithItems = context.Order.Include("orderitem").ToList();
                Console.WriteLine("Eager Loading: Orders with their OrderItems:");
                foreach (var o in ordersWithItems)
                {
                    Console.WriteLine($"  Order ID: {o.o_id}, Date: {o.order_date}");
                    foreach (var oi in o.orderitem)
                    {
                        Console.WriteLine($"    OrderItem - ProductID: {oi.product_id}, Amount: {oi.amount}");
                    }
                }
            }
        }
    }
}
