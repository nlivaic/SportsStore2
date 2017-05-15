using Moq;
using Ninject;
using SportsStore2.Domain.Abstract;
using SportsStore2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace SportsStore2.WebUI.Infrastructure {
    public class NinjectControllerFactory : DefaultControllerFactory {
        private IKernel ninjectKernel;

        public NinjectControllerFactory() {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            IController controller = ninjectKernel.Get(controllerType) as IController;
            return controller;
        }

        public void AddBindings() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product { ProductId = 1, Name = "Name1", Description = "Desc1", Category = "Cat1", Price = 12m},
                new Product { ProductId = 2, Name = "Name2", Description = "Desc2", Category = "Cat2", Price = 12.99m},
                new Product { ProductId = 3, Name = "Name3", Description = "Desc3", Category = "Cat1", Price = 99.99m},
                new Product { ProductId = 4, Name = "Name4", Description = "Desc4", Category = "Cat2", Price = 109.99m},
                new Product { ProductId = 5, Name = "Name5", Description = "Desc5", Category = "Cat4", Price = 75000m}
            }.AsQueryable<Product>());

            ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}