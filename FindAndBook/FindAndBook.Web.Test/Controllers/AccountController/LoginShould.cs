using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.AccountController
{
    [TestFixture]
    public class LoginShould
    {
        public void LoginShould_ReturnView()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller
                .WithCallTo(c => c.Login())
                .ShouldRenderDefaultView();
        }


        public void LoginShould_CallAuthProviderPropertyIsAuthenticated()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller.Login();

            authProviderMock.Verify(p => p.IsAuthenticated, Times.Once);
        }

        public void LoginShould_RedirectToActionHomeIndex_WhenProviderIsAuthenticatedIsTrue()
        {

            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);
            authProviderMock.Setup(p => p.IsAuthenticated).Returns(true);

            var result = controller.Login();

            Assert.IsInstanceOf<RedirectToRouteResult>(result);
        }
    }
}
