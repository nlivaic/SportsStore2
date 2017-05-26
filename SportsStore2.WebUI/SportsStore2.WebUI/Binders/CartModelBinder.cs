using SportsStore2.Domain.Entities;
using System.Web.Mvc;

namespace SportsStore2.WebUI.Binders {
    public class CartModelBinder : IModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            Cart cart = (Cart)controllerContext.HttpContext.Session["cart"];
            if (cart == null) {
                cart = new Cart();
                controllerContext.HttpContext.Session["cart"] = cart;
            }
            return cart;
        }
    }
}