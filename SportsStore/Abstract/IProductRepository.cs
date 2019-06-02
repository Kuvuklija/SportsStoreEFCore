using SportsStore.Models;
using System.Collections.Generic;

namespace SportsStore.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Product GetProduct(long key);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void UpdateAll(Product[] products);
        void Delete(Product product);
    }
}
