using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.AccountController
{
    [TestFixture]
    public class LogoffShould
    {
        [Test]
        public void LogoffShould_CallProviderMethodSignOut()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller.LogOff();

            authProviderMock.Verify(p => p.SignOut(), Times.Once);
        }

        [Test]
        public void LogoffShould_ReturnRedirectToAction()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller
                .WithCallTo(c => c.LogOff())
                .ShouldRedirectTo((Web.Controllers.HomeController homeController) => homeController.Index());
        }
    }
}
