using SportsStore2.Domain.Abstract;
using SportsStore2.Domain.Entities;
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
        public ViewResult Summary()
        {
            Cart cart = CreateShoppingCart();

            return View(cart);
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl = null) {
            Cart cart = CreateShoppingCart();
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (product != null) {
                cart.AddToCart(product);
            }
            return RedirectToAction("Summary");
        }

        private Cart CreateShoppingCart() {
            Cart cart = (Cart)Session["cart"];
            if (cart == null) {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return cart;
        }
    }
}