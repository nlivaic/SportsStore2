using SportsStore2.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore2.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthorize authentication;

        public AccountController(IAuthorize auth) {
            authentication = auth;
        }

        [HttpGet]
        public ViewResult LogOn(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string username, string password, string returnUrl) {
            if (authentication.Authorize(username, password)) {
                return Redirect(returnUrl);
            } else {
                ModelState.AddModelError("", "Invalid credentials.");
            }
            return View();
        }
    }
}