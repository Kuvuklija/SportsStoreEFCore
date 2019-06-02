using SportsStore.Abstract;
using SportsStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Concrete
{
    public class ProductRepository : IProductRepository
    {
        //private List<Product> data = new List<Product>();
        private DataContext context;
        public ProductRepository(DataContext dataContext) => context = dataContext;

        public IEnumerable<Product> Products => context.Products.ToArray();

        public void AddProduct(Product product) {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public Product GetProduct(long key) => context.Products.Find(key);

        public void UpdateProduct(Product product) {
            Product p = GetProduct(product.Id);
            p.Name = product.Name;
            p.Category = product.Category;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;
            //context.Products.Update(product);
            context.SaveChanges();
        }

        public void UpdateAll(Product[] products) {
            //context.Products.UpdateRange(products);
            Dictionary<long, Product> dataHttp = products.ToDictionary(p => p.Id);
            IEnumerable<Product> baseline = context.Products
                .Where(p => dataHttp.Keys.Contains(p.Id));
            foreach(Product databaseProduct in baseline) {
                Product requestProduct = dataHttp[databaseProduct.Id];
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.Category = requestProduct.Category;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;
            }
            context.SaveChanges();
        }

        public void Delete(Product product) {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
