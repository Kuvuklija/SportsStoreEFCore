using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Abstract;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrdersController : Controller{

        private IOrdersRepository orderRepository;
        private IProductRepository productRepository;

        public OrdersController(IProductRepository repoProducts,IOrdersRepository repoOrders) {
            orderRepository = repoOrders;
            productRepository = repoProducts;
        }

        public IActionResult IndexOrder() => View(orderRepository.Orders);

        public IActionResult EditOrder(long id) {
            Order order = id == 0 ? new Order() : orderRepository.GetOrder(id);
            IDictionary<long, OrderLine> linesMap = order.Lines?.ToDictionary(l => l.ProductId) ?? new Dictionary<long, OrderLine>();
            ViewBag.Lines = productRepository.Products
                .Select(p => linesMap.ContainsKey(p.Id) ? linesMap[p.Id] : new OrderLine { Product = p, ProductId = p.Id, Quantity = 0 });
            return View(order);
        }

        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order) {
            return RedirectToAction(nameof(IndexOrder));
        }

        [HttpPost]
        public IActionResult DeleteOrder(Order order) {
            orderRepository.DeleteOrder(order);
            return RedirectToAction(nameof(IndexOrder));
        }
    }
}