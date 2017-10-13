using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.AccountController
{
    [TestFixture]
    public class LogoutShould
    {
        [Test]
        public void LogoutShould_CallProviderMethodSignOut()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller.LogOut();

            authProviderMock.Verify(p => p.SignOut(), Times.Once);
        }

        [Test]
        public void LogoutShould_ReturnRedirectToAction()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller
                .WithCallTo(c => c.LogOut())
                .ShouldRedirectTo((Web.Controllers.HomeController homeController) => homeController.Index());
        }
    }
}
