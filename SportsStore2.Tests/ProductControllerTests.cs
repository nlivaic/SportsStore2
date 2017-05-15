using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore2.WebUI.Controllers;
using SportsStore2.Domain.Abstract;
using Moq;
using SportsStore2.Domain.Entities;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace SportsStore2.Tests {
    [TestClass]
    public class ProductControllerTests {
        Mock<IProductRepository> mock;
        [TestInitialize]
        public void TestInitialize() {
            mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductId = 1, Name = "Name1", Description = "Desc1", Category = "Cat1", Price = 12m},
                new Product { ProductId = 2, Name = "Name2", Description = "Desc2", Category = "Cat2", Price = 12.99m},
                new Product { ProductId = 3, Name = "Name3", Description = "Desc3", Category = "Cat1", Price = 99.99m},
                new Product { ProductId = 4, Name = "Name4", Description = "Desc4", Category = "Cat2", Price = 109.99m},
                new Product { ProductId = 5, Name = "Name5", Description = "Desc5", Category = "Cat4", Price = 75000m}
            }.AsQueryable<Product>());
        }

        [TestMethod]
        public void Can_Display_All_Products() {
            // Arrange
            ProductController target = new ProductController(mock.Object);

            // Act
            Product[] result = ((IEnumerable<Product>)(target.List()).Model).ToArray<Product>();

            // Assert
            Assert.IsTrue(result.Length == 5);
            Assert.IsTrue(result[0].ProductId == 1);
            Assert.IsTrue(result[1].ProductId == 2);
            Assert.IsTrue(result[2].ProductId == 3);
            Assert.IsTrue(result[3].ProductId == 4);
            Assert.IsTrue(result[4].ProductId == 5);
        }
    }
}
