using SportsStore2.Domain.Abstract;
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

        public ProductController(IProductRepository repo) {
            repository = repo;
        }

        // GET: Product
        public ViewResult List(int page = 1)
        {
            ListViewModel viewModel = new ListViewModel {
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    PageSize = 4,
                    TotalItems = repository.Products.Count()
                },
                Products = repository.Products
            };
            return View(viewModel);
        }
    }
}