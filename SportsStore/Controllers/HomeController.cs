﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Abstract;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository repository;
        private ICategoryRepository catRepository;

        public HomeController(IProductRepository repo, ICategoryRepository catRepo) {
            repository = repo;
            catRepository = catRepo;
        }

        public IActionResult Index(QueryOptions options) {
            //System.Console.Clear();
            //return View(repository.Products);
            return View(repository.GetProducts(options)); //with paging
        }

        //[HttpPost]
        //public IActionResult AddProduct(Product product) {
        //    repository.AddProduct(product);
        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult UpdateProduct(long key) {
            ViewBag.Categories = catRepository.Categories;
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