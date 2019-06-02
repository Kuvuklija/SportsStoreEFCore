using Microsoft.AspNetCore.Mvc;
using SportsStore.Abstract;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository repository;

        public HomeController(IProductRepository repo) {
            repository = repo;
        }

        public IActionResult Index() {
            //System.Console.Clear();
            return View(repository.Products);
        }

        //[HttpPost]
        //public IActionResult AddProduct(Product product) {
        //    repository.AddProduct(product);
        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult UpdateProduct(long key) {
            return View(key==0 ? new Product():repository.GetProduct(key));
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product) {
            //System.Console.Clear();
            if (product.Id == 0) {
                repository.AddProduct(product);
            } else {
                repository.UpdateProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateAll() {
            ViewBag.UpdateAll = true;
            return View(nameof(Index), repository.Products);
        }

        [HttpPost]
        public IActionResult UpdateAll(Product[] products) {
            repository.UpdateAll(products);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Product product) {
            repository.Delete(product);
            return RedirectToAction(nameof(Index));
        }
    }
}