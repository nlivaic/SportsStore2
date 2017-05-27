using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore2.Domain.Entities;
using Moq;
using SportsStore2.Domain.Abstract;
using System.Linq;

namespace SportsStore2.Tests {
    [TestClass]
    public class CartTests {
        Mock<IProductRepository> mock;
        [TestInitialize]
        public void TestInitialize() {
            mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
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
        public void Can_Add_New_Item() {
            // Arrange
            Cart cart = new Cart();
            Product p1 = mock.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            Product p4 = mock.Object.Products.Where(p => p.ProductId == 4).FirstOrDefault();

            // Act
            cart.AddToCart(p1);
            cart.AddToCart(p4);

            // Assert
            Assert.AreSame(cart.Items.ToArray()[0].Item, p1);
            Assert.IsTrue(cart.Items.ToArray()[0].Quantity == 1);
            Assert.AreSame(cart.Items.ToArray()[1].Item, p4);
            Assert.IsTrue(cart.Items.ToArray()[1].Quantity == 1);
        }

        [TestMethod]
        public void Can_Add_Same_Item_Again() {
            // Arrange
            Cart cart = new Cart();
            Product p1 = mock.Object.Products.Where(p => p.ProductId == 1).FirstOrDefault();
            Product p4 = mock.Object.Products.Where(p => p.ProductId == 4).FirstOrDefault();

            // Act
            cart.AddToCart(p1);
            cart.AddToCart(p4);
            cart.AddToCart(p1);

            // Assert
            Assert.IsTrue(cart.Items.ToArray()[0].Item.ProductId == 1);
            Assert.IsTrue(cart.Items.ToArray()[0].Quantity == 2);
            Assert.IsTrue(cart.Items.ToArray()[1].Item.ProductId == 4);
            Assert.IsTrue(cart.Items.ToArray()[1].Quantity == 1);
        }
    }
}
