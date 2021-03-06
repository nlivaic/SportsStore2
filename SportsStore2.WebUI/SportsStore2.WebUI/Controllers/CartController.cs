﻿using SportsStore2.Domain.Abstract;
using SportsStore2.Domain.Entities;
using SportsStore2.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore2.WebUI.Controllers
{
    public class CartController : Controller {
        private IProductRepository repository;
        public IOrderProcessor processor;

        public CartController(IProductRepository repo, IOrderProcessor proc) {
            repository = repo;
            processor = proc;
        }

        // GET: Cart
        public ViewResult Summary(Cart cart, string returnUrl = null) {
            CartSummaryViewModel summaryVM = new CartSummaryViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(summaryVM);
        }

        public PartialViewResult QuickSummary(Cart cart) {
            return PartialView(cart);
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl = null) {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (product != null) {
                cart.AddToCart(product);
            }
            return RedirectToAction("Summary", new { returnUrl });
        }

        public RedirectToRouteResult DeleteItem(Cart cart, int productId, string returnUrl = null) {
            Product product = repository.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            if (product != null) {
                cart.RemoveFromCart(product);
            }
            return RedirectToAction("Summary", new { returnUrl });
        }
        [HttpGet]
        public ViewResult Checkout(Cart cart) {
            if (cart.Items.Count() == 0) {
                ModelState.AddModelError("emptyCart", "Sorry, your cart is empty!");
            }
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails) {
            if (ModelState.IsValid) {
                // Process the order.
                processor.ProcessOrder(cart, shippingDetails);
                // Reset cart.
                cart.Clear();
            } else {
                return View(shippingDetails);
            }
            return View("Completed", shippingDetails);
        }
    }
}