using FindAndBook.Authentication.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.AccountController
{
    [TestFixture]
    public class RegisterShould
    {
        [Test]
        public void RegisterShould_ReturnDefaultView()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            controller
                .WithCallTo(c => c.Register())
                .ShouldRenderDefaultView();
        }

        [TestCase("pesho@pesho.com", "pesho", "1234qwerty", "Pesho", "Peshov", "0854721436")]
        public void RegisterShould_CallUserFactoryCreateUserCorrectly_WhenModelStateIsValid(string email, 
            string username, string password, string firstName, string lastName, string phoneNumber)
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            authProviderMock.Setup(
                    p =>
                        p.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Success);
            var user = new User();

            userServiceMock.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(user);

            var model = new RegisterViewModel
            {
                Email = email,
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

          controller.Register(model);

            userServiceMock.Verify(f => f.CreateUser(username, email, firstName, lastName, phoneNumber), Times.Once);
        }

        [TestCase("pesho@pesho.com", "pesho", "1234qwerty", "Pesho", "Peshov", "0854721436")]
        public void RegisterShould_CallProviderRegisterAndLoginUserCorrectly_WhenModelStateIsValid(string email,
            string username, string password, string firstName, string lastName, string phoneNumber)
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);
            authProviderMock.Setup(
                    p =>
                        p.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Success);

            var user = new User();

           userServiceMock.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(user);

            var model = new RegisterViewModel
            {
                Email = email,
                Username = username,
                Password = password
            };

           controller.Register(model);

            authProviderMock.Verify(p => p.RegisterAndLoginUser(user, password, It.IsAny<bool>(), It.IsAny<bool>()));
        }

        [TestCase("pesho@pesho.com", "pesho", "1234qwerty", "Pesho", "Peshov", "0854721436")]
        public void RegisterShould_ReturnRedirectToRouteResult_WhenResultSuccess(string email,
            string username, string password, string firstName, string lastName, string phoneNumber)
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            authProviderMock.Setup(
                    p =>
                        p.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Success);

            var user = new User();

          
            userServiceMock.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(user);

            var model = new RegisterViewModel
            {
                Email = email,
                Username = username,
                Password = password
            };

            controller
                .WithCallTo(c => c.Register(model))
                .ShouldRedirectTo((Web.Controllers.HomeController c) => c.Index());
        }

        [TestCase("pesho@pesho.com", "pesho", "1234qwerty", "Pesho", "Peshov", "0854721436")]
        public void RegisterShould_ReturnViewWithModel_WhenResultNotSuccess(string email,
            string username, string password, string firstName, string lastName, string phoneNumber)
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            authProviderMock.Setup(
                    p =>
                        p.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Failed(null));

            var user = new User();

            userServiceMock.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(user);

            var model = new RegisterViewModel
            {
                Email = email,
                Username = username,
                Password = password
            };

           controller
                .WithCallTo(c => c.Register(model))
                .ShouldRenderDefaultView()
                .WithModel<RegisterViewModel>(m => Assert.AreSame(model, m));
        }

        [TestCase("pesho@pesho.com", "pesho", "1234qwerty", "Pesho", "Peshov", "0854721436")]
        public void RegisterShould_ReturnViewWithModel_WhenModelIsNotValid(string email,
            string username, string password, string firstName, string lastName, string phoneNumber)
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var userServiceMock = new Mock<IUserService>();

            var controller = new Web.Controllers.AccountController(authProviderMock.Object, userServiceMock.Object);

            authProviderMock.Setup(
                    p =>
                        p.RegisterAndLoginUser(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(),
                            It.IsAny<bool>()))
                .Returns(IdentityResult.Success);

            var user = new User();

            var mockedFactory = new Mock<IUserService>();
            mockedFactory.Setup(f => f.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(user);

            var model = new RegisterViewModel
            {
                Email = email,
                Username = username,
                Password = password
            };

           controller.ModelState.AddModelError("key", "message");

            controller
                .WithCallTo(c => c.Register(model))
                .ShouldRenderDefaultView()
                .WithModel<RegisterViewModel>(m => Assert.AreSame(model, m));
        }
    }
}
