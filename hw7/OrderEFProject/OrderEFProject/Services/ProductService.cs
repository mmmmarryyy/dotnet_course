using OrderEFProject.Model;
using System.Data.Entity;
using System.Linq;

namespace OrderEFProject.Services
{
    public class ProductService
    {
        public void AddProduct(product prod)
        {
            using (var context = new OrderDBEntities())
            {
                context.product.Add(prod);
                context.SaveChanges();
            }
        }

        public product GetProductNoTracking(int id)
        {
            using (var context = new OrderDBEntities())
            {
                return context.product.AsNoTracking().FirstOrDefault(p => p.p_id == id);
            }
        }

        public void UpdateProduct(product prod)
        {
            using (var context = new OrderDBEntities())
            {
                context.product.Attach(prod);
                context.Entry(prod).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = new OrderDBEntities())
            {
                var prod = context.product.Find(id);
                if (prod != null)
                {
                    context.product.Remove(prod);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteAllProducts()
        {
            using (var context = new OrderDBEntities())
            {
                var prods = context.product.ToList();
                context.product.RemoveRange(prods);
                context.SaveChanges();
            }
        }
    }
}
