using SportsStore2.Domain.Abstract;
using SportsStore2.WebUI.Models;
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
        public ActionResult LogOn(LogOnViewModel logOn) {
            if (authentication.Authorize(logOn.Username, logOn.Password)) {
                return Redirect(logOn.ReturnUrl);
            } else {
                ModelState.AddModelError("", "Invalid credentials.");
            }
            return View();
        }
    }
}