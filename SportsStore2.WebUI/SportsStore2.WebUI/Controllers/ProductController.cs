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
        private int pageSize = 4;

        public ProductController(IProductRepository repo) {
            repository = repo;
        }

        public PartialViewResult ListCategories(string category) {
            CategoryViewModel categoryVM = new CategoryViewModel {
                Categories = repository.Products.Select(p => p.Category).Distinct().AsEnumerable<string>(),
                ChosenCategory = category
            };
            return PartialView(categoryVM);
        }

        // GET: Product
        public ViewResult List(string category, int page = 1)
        {
            ListViewModel viewModel = new ListViewModel {
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = repository.Products.Where(p => category == null || p.Category == category).Count()
                },
                Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(x => x.ProductId).Skip((page - 1) * pageSize).Take(pageSize)
            };
            return View(viewModel);
        }
    }
}