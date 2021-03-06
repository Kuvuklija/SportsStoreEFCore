﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Abstract;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository repository;
        public CategoryController(ICategoryRepository repo) => repository = repo;

        public IActionResult IndexCategory(){
            return View(repository.Categories);
        }

        [HttpPost]
        public IActionResult AddCategory(Category category) {
            repository.AddCategory(category);
            return RedirectToAction(nameof(IndexCategory));
        }

        public IActionResult EditCategory(long id) {
            ViewBag.EditId = id;
            return View(nameof(IndexCategory), repository.Categories); //all categories in order to select EditId inside circul (foreach...)
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category) {
            repository.UpdateCategory(category);
            return RedirectToAction("IndexCategory");
        }

        [HttpPost]
        public IActionResult DeleteCategory(Category category) {
            repository.DeleteCategory(category);
            return RedirectToAction(nameof(IndexCategory));
        }
    }
}