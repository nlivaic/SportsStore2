using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore2.WebUI.Controllers;
using Moq;
using SportsStore2.Domain.Abstract;
using SportsStore2.Domain.Entities;
using System.Linq;
using System.Web.Mvc;
using SportsStore2.WebUI.Models;

namespace SportsStore2.Tests {
    [TestClass]
    public class CartControllerTests {
        Mock<IProductRepository> mockRepo;

        [TestInitialize]
        public void TestInitialize() {
            mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductId = 1, Name = "Name1", Description = "Desc1", Category = "Cat1", Price = 12m},
                new Product { ProductId = 2, Name = "Name2", Description = "Desc2", Category = "Cat2", Price = 12.99m},
                new Product { ProductId = 3, Name = "Name3", Description = "Desc3", Category = "Cat1", Price = 99.99m},
                new Product { ProductId = 4, Name = "Name4", Description = "Desc4", Category = "Cat2", Price = 109.99m},
                new Product { ProductId = 5, Name = "Name5", Description = "Desc5", Category = "Cat4", Price = 75000m},
                new Product { ProductId = 6, Name = "Name6", Description = "Desc6", Category = "Cat4", Price = 75.99m},
                new Product { ProductId = 7, Name = "Name7", Description = "Desc7", Category = "Cat3", Price = 137.99m},
                new Product { ProductId = 8, Name = "Name8", Description = "Desc8", Category = "Cat3", Price = 19.99m},
                new Product { ProductId = 9, Name = "Name9", Description = "Desc9", Category = "Cat1", Price = 76.99m},
            }.AsQueryable<Product>());
        }

        [TestMethod]
        public void Can_Add_Item() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            Product p4 = mockRepo.Object.Products.Where(p => p.ProductId == 4).FirstOrDefault();
            Cart cart = new Cart();

            // Act
            target.AddToCart(cart, 1, "someUrl");
            target.AddToCart(cart, 4, "someUrl");

            // Assert - correct item is in cart.
            Assert.AreSame(cart.Items.ToArray()[0].Item, p1);
            Assert.AreSame(cart.Items.ToArray()[1].Item, p4);
            // Assert - correct number of items is in cart.
            Assert.IsTrue(cart.Items.Count() == 2);
        }

        [TestMethod]
        public void Can_Redirect_On_Add_Item() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            Cart cart = new Cart();

            // Act
            RedirectToRouteResult result = target.AddToCart(cart, 1, "someUrl");

            // Assert - redirects to Summary
            Assert.IsTrue(result.RouteValues["action"].ToString() == "Summary");
            // Assert - redirects and passes along correct returnUrl.
            Assert.IsTrue(result.RouteValues["returnUrl"].ToString() == "someUrl");
        }

        [TestMethod]
        public void Can_Create_Correct_Summary_View_Model() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            Cart cart = new Cart();
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            cart.AddToCart(p1);

            // Act
            CartSummaryViewModel result = (CartSummaryViewModel)(target.Summary(cart, "someUrl").Model);

            // Assert - correct cart is passed from Session.
            Assert.AreSame(result.Cart, cart);
            // Assert - return url is maintained.
            Assert.IsTrue(result.ReturnUrl == "someUrl");
        }

        [TestMethod]
        public void Can_Remove_Item() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            Product p4 = mockRepo.Object.Products.Where(p => p.ProductId == 4).FirstOrDefault();
            Cart cart = new Cart();
            cart.AddToCart(p1);
            cart.AddToCart(p4);

            // Act
            target.DeleteItem(cart, 1, "someUrl");

            // Assert - correct cart item is removed.
            Assert.IsTrue(cart.Items.Where(p => p.Item.ProductId == 1).FirstOrDefault() == null);
            Assert.IsTrue(cart.Items.Where(p => p.Item.ProductId == 4).FirstOrDefault() != null);
        }

        [TestMethod]
        public void Can_Redirect_On_Remove_Item() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            Cart cart = new Cart();
            cart.AddToCart(p1);

            // Act
            RedirectToRouteResult result = target.DeleteItem(cart, 1, "someUrl");

            // Assert - Redirect to Summary.
            Assert.IsTrue(result.RouteValues["action"].ToString() == "Summary");
            // Assert - Redirects and passes correct return Url along.
            Assert.IsTrue(result.RouteValues["returnUrl"].ToString() == "someUrl");
        }

        [TestMethod]
        public void Can_Pass_Cart_To_Quick_Summary() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            Cart cart = new Cart();

            // Act
            Cart result = (Cart)(target.QuickSummary(cart).Model);

            // Assert
            Assert.AreSame(result, cart);
        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            Cart cart = new Cart();

            // Act
            ViewResult result = target.Checkout(cart);

            // Assert - an error message exists.
            Assert.IsTrue(result.ViewData.ModelState["emptyCart"] != null);
        }

        [TestMethod]
        public void Can_Checkout_Cart_With_Items() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            Cart cart = new Cart();
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            cart.AddToCart(p1);

            // Act
            ViewResult result = target.Checkout(cart);

            // Assert - an error message exists.
            Assert.IsTrue(result.ViewData.ModelState["emptyCart"] == null);
        }

        [TestMethod]
        public void Cannot_Ship_With_Invalid_Details() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            target.ModelState.AddModelError("invalidShippingDetails", "Invalid Shipping Details.");
            Cart cart = new Cart();
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            cart.AddToCart(p1);
            ShippingDetails shippingDetails = new ShippingDetails();

            // Act
            ViewResult result = target.Checkout(cart, shippingDetails);

            // Assert - Order processor has NOT been called

            // Assert - Model is invalid.
            Assert.IsTrue(result.ViewData.ModelState.IsValid == false);
            // Assert - Default view is returned.
            Assert.IsTrue(result.ViewName == String.Empty);
        }

        [TestMethod]
        public void Can_Ship_With_Valid_Details() {
            // Arrange
            CartController target = new CartController(mockRepo.Object);
            Cart cart = new Cart();
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            cart.AddToCart(p1);
            ShippingDetails shippingDetails = new ShippingDetails();

            // Act
            ViewResult result = target.Checkout(cart, shippingDetails);

            // Assert - Order processor has been called. 

            // Assert - Model is valid.
            Assert.IsTrue(result.ViewData.ModelState.IsValid == true);
            // Assert - Completed view has been returned.
            Assert.IsTrue(result.ViewName == "Completed");
        }
    }
}
