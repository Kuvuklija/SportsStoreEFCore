using Microsoft.EntityFrameworkCore;
using SportsStore.Abstract;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Concrete
{
    public class OrderRepository:IOrdersRepository{

        private DataContext context;
        public OrderRepository(DataContext ctx) => context = ctx;

        public IEnumerable<Order> Orders => context.Orders
            .Include(l => l.Lines).ThenInclude(l => l.Product).ToArray();

        public Order GetOrder(long key) => context.Orders
            .Include(l => l.Lines).First(z => z.Id == key);

        public void AddOrder(Order order) {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void UpdateOrder(Order order) {
            context.Orders.Update(order);
            context.SaveChanges();
        }

        public void DeleteOrder(Order order) {
            context.Orders.Remove(order);
            context.SaveChanges();
        }
    }
}
