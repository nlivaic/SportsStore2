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
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository repo) {
            repository = repo;
        }

        // GET: Cart
        public ViewResult Summary(Cart cart, string returnUrl = null)
        {
            CartSummaryViewModel summaryVM = new CartSummaryViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(summaryVM);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl = null) {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (product != null) {
                cart.AddToCart(product);
            }
            return RedirectToAction("Summary", new { returnUrl});
        }
    }
}