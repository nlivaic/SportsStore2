using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore2.Domain.Abstract;
using SportsStore2.WebUI.Controllers;
using System.Web.Mvc;
using SportsStore2.WebUI.Models;

namespace SportsStore2.Tests {
    [TestClass]
    public class AccountControllerTests {
        [TestMethod]
        public void Cannot_Log_In_With_Invalid_Credentials() {
            // Arrange
            Mock<IAuthorize> mock = new Mock<IAuthorize>();
            mock.Setup(m => m.Authorize("badUsername", "badPassword")).Returns(false);
            AccountController target = new AccountController(mock.Object);

            // Act
            ViewResult result = target.LogOn(new LogOnViewModel {Username = "badUsername", Password = "badPassword" }) as ViewResult;

            // Assert - controller returns a View back.
            Assert.IsTrue(result != null);
            // Assert - controller returns the same View back (LogOn).
            Assert.IsTrue(result.ViewName == String.Empty);
            // Assert - model is invalid.
            Assert.IsTrue(result.ViewData.ModelState.IsValid == false);
            // Assert - an error message.
            Assert.IsTrue(result.ViewData.ModelState[""].Errors[0].ErrorMessage.ToString() == "Invalid credentials.");
        }

        [TestMethod]
        public void Can_Log_In_With_Valid_Credentials() {
            // Arrange
            Mock<IAuthorize> mock = new Mock<IAuthorize>();
            mock.Setup(m => m.Authorize("username", "password")).Returns(true);
            AccountController target = new AccountController(mock.Object);

            // Act
            RedirectResult result = target.LogOn(new LogOnViewModel {Username = "username", Password = "password", ReturnUrl = "someReturnUrl" }) as RedirectResult;

            // Assert - controller returns a Redirect back.
            Assert.IsTrue(result != null);
            // Assert - controller returns a redirect to originating URL.
            Assert.IsTrue(result.Url == "someReturnUrl");
        }
    }
}
