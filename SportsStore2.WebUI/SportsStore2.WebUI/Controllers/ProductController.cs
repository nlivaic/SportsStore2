using SportsStore2.Domain.Abstract;
using SportsStore2.Domain.Entities;
using SportsStore2.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore2.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        private int pageSize = 4;

        public ProductController(IProductRepository repo) {
            repository = repo;
        }

        public PartialViewResult ListCategories(string category) {
            CategoryViewModel categoryVM = new CategoryViewModel {
                Categories = repository.Products.OrderBy(p => p.Category).Select(p => p.Category).Distinct().AsEnumerable<string>(),
                ChosenCategory = category
            };
            return PartialView(categoryVM);
        }

        // GET: Product
        public ViewResult List(string category = null, int page = 1)
        {
            ListViewModel viewModel = new ListViewModel {
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = repository.Products.Where(p => category == null || p.Category == category).Count()
                },
                Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(x => x.ProductId).Skip((page - 1) * pageSize).Take(pageSize),
                SelectedCategory = category
            };
            return View(viewModel);
        }

        public FileContentResult GetImage(int productId) {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (product != null && product.ImageData != null) {
                return new FileContentResult(product.ImageData, product.ImageMimeType);
            }
            return null;
        }
    }
}