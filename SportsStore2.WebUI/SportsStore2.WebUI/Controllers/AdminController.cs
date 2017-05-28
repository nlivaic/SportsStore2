﻿using SportsStore2.Domain.Abstract;
using SportsStore2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore2.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo) {
            repository = repo;
        }

        // GET: Admin
        public ViewResult List() {
            return View(repository.Products);
        }

        // GET: Admin
        public RedirectToRouteResult Delete(int productId) {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (product != null) {
                //repository.DeleteProduct(product);
                TempData["message"] = String.Format("Product {0} removed.", product.Name);
            }
            return RedirectToAction("List");
        }
    }
}