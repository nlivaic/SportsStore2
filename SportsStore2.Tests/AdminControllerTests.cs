using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore2.Domain.Abstract;
using Moq;
using SportsStore2.Domain.Entities;
using System.Linq;
using SportsStore2.WebUI.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SportsStore2.Tests {
    [TestClass]
    public class AdminControllerTests {
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
        public void Can_List_All_Products() {
            // Arrange
            AdminController target = new AdminController(mockRepo.Object);

            // Act
            Product[] result = ((IEnumerable<SportsStore2.Domain.Entities.Product>)target.List().Model).ToArray<Product>();

            // Assert
            Assert.IsTrue(result.Count() == 9);
            Assert.IsTrue(result[0].ProductId == 1);
            Assert.IsTrue(result[4].ProductId == 5);
            Assert.IsTrue(result[7].ProductId == 8);
        }

        [TestMethod]
        public void Return_Empty_Edit_View_On_Create_New_Product() {
            // Arrange
            AdminController target = new AdminController(mockRepo.Object);

            // Act
            ViewResult result = target.Create();
            Product newProduct = (Product)(result.Model);

            // Assert
            Assert.IsTrue(result.ViewName == "Edit");
            Assert.IsTrue(newProduct.ProductId == 0);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Product() {
            // Arrange
            AdminController target = new AdminController(mockRepo.Object);

            // Act
            ViewResult result = target.Edit(100);

            // Assert - returns initial admin product list screen.
            Assert.IsTrue(result.ViewName == "List");
        }

        [TestMethod]
        public void Can_Edit_Existing_Product() {
            // Arrange
            AdminController target = new AdminController(mockRepo.Object);
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();

            // Act
            ViewResult result = target.Edit(1);

            // Assert - Return Edit view.
            Assert.IsTrue(result.ViewName == "");
            // Assert - View model is correct product.
            Assert.AreSame(((Product)result.Model), p1);
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Product() {
            // Arrange
            AdminController target = new AdminController(mockRepo.Object);
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            target.ModelState.AddModelError("", "someError");

            // Act - Save product details.
            ActionResult result = target.Edit(p1);

            // Assert - Return Edit view.
            Assert.IsTrue(((ViewResult)result).ViewName == "Edit");
            // Assert - View model is correct product.
            Assert.AreSame(((Product)((ViewResult)result).Model), p1);
            // Assert - product has not been saved.
            mockRepo.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void Can_Save_Valid_Product() {
            // Arrange
            AdminController target = new AdminController(mockRepo.Object);
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();

            // Act - Save product details.
            ActionResult result = target.Edit(p1);

            // Assert - Return List view.
            Assert.IsTrue(((RedirectToRouteResult)result).RouteValues["action"].ToString() == "List");
            // Assert - product has not been saved.
            mockRepo.Verify(m => m.SaveProduct(p1), Times.Once);
        }

        [TestMethod]
        public void Cannot_Delete_Nonexistent_Product() {
            // Arrange
            AdminController target = new AdminController(mockRepo.Object);

            // Act - Save product details.
            RedirectToRouteResult result = target.Delete(100);

            // Assert - Return List view.
            Assert.IsTrue(result.RouteValues["action"].ToString() == "List");
            // Assert - product has not been deleted.
            mockRepo.Verify(m => m.DeleteProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void Can_Delete_Existing_Product() {
            // Arrange
            AdminController target = new AdminController(mockRepo.Object);
            Product p1 = mockRepo.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();

            // Act - Save product details.
            RedirectToRouteResult result = target.Delete(1);

            // Assert - Return List view.
            Assert.IsTrue(result.RouteValues["action"].ToString() == "List");
            // Assert - product has not been deleted.
            mockRepo.Verify(m => m.DeleteProduct(p1), Times.Once);
        }
    }
}
