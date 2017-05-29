using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore2.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthentication authentication;

        public AccountController(IAuthentication auth) {
            authentication = auth;
        }

        // GET: Account
        public RedirectResult LogOn(string returnUrl)
        {
            return Redirect(returnUrl);
        }
    }
}