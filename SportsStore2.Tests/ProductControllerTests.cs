using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore2.WebUI.Controllers;
using SportsStore2.Domain.Abstract;
using Moq;
using SportsStore2.Domain.Entities;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using SportsStore2.WebUI.Models;
using SportsStore2.WebUI.HtmlHelpers;

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
                new Product { ProductId = 5, Name = "Name5", Description = "Desc5", Category = "Cat4", Price = 75000m},
                new Product { ProductId = 6, Name = "Name6", Description = "Desc6", Category = "Cat4", Price = 75.99m},
                new Product { ProductId = 7, Name = "Name7", Description = "Desc7", Category = "Cat3", Price = 137.99m},
                new Product { ProductId = 8, Name = "Name8", Description = "Desc8", Category = "Cat3", Price = 19.99m},
                new Product { ProductId = 9, Name = "Name9", Description = "Desc9", Category = "Cat1", Price = 76.99m},
            }.AsQueryable<Product>());
        }

        [TestMethod]
        public void Can_Display_All_Products() {
            // Arrange
            ProductController target = new ProductController(mock.Object);

            // Act
            Product[] result = ((ListViewModel)(target.List()).Model).Products.ToArray<Product>();

            // Assert
            Assert.IsTrue(result.Length == 4);
            Assert.IsTrue(result[0].ProductId == 1);
            Assert.IsTrue(result[1].ProductId == 2);
            Assert.IsTrue(result[2].ProductId == 3);
            Assert.IsTrue(result[3].ProductId == 4);
        }

        [TestMethod]
        public void Can_Create_Page_Links() {
            // Arrange
            PagingInfo pagingInfo = new PagingInfo {
                CurrentPage = 2,
                PageSize = 4,
                TotalItems = 7
            };

            // Act
            string result = HtmlHelpers.PageLinks(null, pagingInfo, (x => "someLink/Page" + x)).ToString();

            // Assert
            Assert.IsTrue(result == @"<a href=""someLink/Page1"">1</a><a class=""selected"" href=""someLink/Page2"">2</a>");
        }

        [TestMethod]
        public void Can_Paginate() {
            // Arrange
            ProductController target = new ProductController(mock.Object);

            // Act
            Product[] result = ((ListViewModel)(target.List(null, 2).Model)).Products.ToArray();

            // Assert
            Assert.IsTrue(result[0].ProductId == 5);
            Assert.IsTrue(result[1].ProductId == 6);
            Assert.IsTrue(result[2].ProductId == 7);
            Assert.IsTrue(result[3].ProductId == 8);
        }

        [TestMethod]
        public void Can_Pass_Correct_Paging_Info() {
            // Arrange
            ProductController target = new ProductController(mock.Object);

            // Act
            PagingInfo result = ((ListViewModel)(target.List(null, 2).Model)).PagingInfo;

            // Assert
            Assert.IsTrue(result.CurrentPage == 2);
            Assert.IsTrue(result.NrOfPages == 3);
            Assert.IsTrue(result.TotalItems == 9);
            Assert.IsTrue(result.PageSize == 4);
        }

        [TestMethod]
        public void Can_Display_Categories() {
            // Arrange
            ProductController target = new ProductController(mock.Object);

            // Act
            CategoryViewModel result = (CategoryViewModel)(target.ListCategories("Cat1").Model);
            string[] categories = result.Categories.ToArray();

            // Assert
            Assert.IsTrue(result.ChosenCategory == "Cat1");
            Assert.IsTrue(categories[0] == "Cat1");
            Assert.IsTrue(categories[1] == "Cat2");
            Assert.IsTrue(categories[2] == "Cat3");
            Assert.IsTrue(categories[3] == "Cat4");
        }

        [TestMethod]
        public void Can_Filter_By_Category() {
            // Arrange
            ProductController target = new ProductController(mock.Object);

            // Act
            ListViewModel result = ((ListViewModel)(target.List("Cat1", 1).Model));
            Product[] products = result.Products.ToArray();
            PagingInfo pagingInfo = result.PagingInfo;

            // Assert - Product List
            Assert.IsTrue(products[0].ProductId == 1);
            Assert.IsTrue(products[1].ProductId == 3);
            Assert.IsTrue(products[2].ProductId == 9);
            // Assert - Paging info
            Assert.IsTrue(pagingInfo.CurrentPage == 1);
            Assert.IsTrue(pagingInfo.NrOfPages == 1);
            Assert.IsTrue(pagingInfo.PageSize == 4);
            Assert.IsTrue(pagingInfo.TotalItems == 3);
        }
    }
}
