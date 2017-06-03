using SportsStore2.Domain.Abstract;
using SportsStore2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore2.WebUI.Controllers
{
    [Authorize]
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

        public ViewResult Create() {
            return View("Edit", new Product());
        }

        [HttpGet]
        public ViewResult Edit(int productId) {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (product != null) {
                return View(product);
            } else {
                return View("List");
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase uploadFile) {
            if (ModelState.IsValid) {
                if (uploadFile != null) {
                    product.ImageMimeType = uploadFile.ContentType;
                    product.ImageData = new byte[uploadFile.ContentLength];
                    uploadFile.InputStream.Read(product.ImageData, 0, uploadFile.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["message"] = String.Format("Product {0} edited/created.", product.Name);
                return RedirectToAction("List");
            } else {
                return View("Edit", product);
            }
        }

        // GET: Admin
        public RedirectToRouteResult Delete(int productId) {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (product != null) {
                repository.DeleteProduct(product);
                TempData["message"] = String.Format("Product {0} removed.", product.Name);
            }
            return RedirectToAction("List");
        }
    }
}