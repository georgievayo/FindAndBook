using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Navigation;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.NavigationController
{
    [TestFixture]
    public class IndexShould
    {
        [Test]
        public void IndexShould_CallAuthProviderPropertyIsAuthenticated()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var model = new NavigationViewModel(false, false, false, "", "");
            authProviderMock.Setup(p => p.IsAuthenticated).Returns(false);
            factoryMock.Setup(f => f.CreateNavigationViewModel(false, false, false, "", "")).Returns(model);

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);
            controller.Index();

            authProviderMock.Verify(ap => ap.IsAuthenticated, Times.Once);
        }

        [Test]
        public void IndexShould_CallFactoryMethodCreateNavigationViewModel()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var model = new NavigationViewModel(false, false, false, "", "");
            authProviderMock.Setup(p => p.IsAuthenticated).Returns(false);
            factoryMock.Setup(f => f.CreateNavigationViewModel(false, false, false, "", "")).Returns(model);

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);
            controller.Index();

            factoryMock.Verify(f => f.CreateNavigationViewModel(false, false, false, "", ""), Times.Once);
        }

        [Test]
        public void IndexShould_ReturnPartialViewWithCorrectModel()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var model = new NavigationViewModel(false, false, false, "", "");
            authProviderMock.Setup(p => p.IsAuthenticated).Returns(false);
            factoryMock.Setup(f => f.CreateNavigationViewModel(false, false, false, "", "")).Returns(model);

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderPartialView("Navigation")
                .WithModel(model);
        }

        [Test]
        public void IndexShould_CallAuthProviderPropertyCurrentUserId_WhenUserIsAdmin()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var model = new NavigationViewModel(true, false, true, "", "");
            authProviderMock.Setup(p => p.IsAuthenticated).Returns(true);
            var userId = Guid.NewGuid().ToString();
            var username = "admin";
            authProviderMock.Setup(p => p.CurrentUserId).Returns(userId);
            authProviderMock.Setup(p => p.CurrentUserUsername).Returns(username);
            factoryMock.Setup(f => f.CreateNavigationViewModel(true, false, true, username, userId)).Returns(model);

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);
            controller.Index();

            authProviderMock.Verify(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void IndexShould_CallAuthProviderPropertyCurrentUsername_WhenUserIsAdmin()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var model = new NavigationViewModel(true, false, true, "", "");
            authProviderMock.Setup(p => p.IsAuthenticated).Returns(true);
            var userId = Guid.NewGuid().ToString();
            var username = "admin";
            authProviderMock.Setup(p => p.CurrentUserId).Returns(userId);
            authProviderMock.Setup(p => p.CurrentUserUsername).Returns(username);
            factoryMock.Setup(f => f.CreateNavigationViewModel(true, false, true, username, userId)).Returns(model);

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);
            controller.Index();

            authProviderMock.Verify(ap => ap.CurrentUserUsername, Times.Once);
        }

        [Test]
        public void IndexShould_ReturnPartialViewWithCorrectModel_WhenUserIsAdmin()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            authProviderMock.Setup(p => p.IsAuthenticated).Returns(true);
            var userId = Guid.NewGuid().ToString();
            var username = "admin";
            authProviderMock.Setup(p => p.CurrentUserId).Returns(userId);
            authProviderMock.Setup(p => p.CurrentUserUsername).Returns(username);
            authProviderMock.Setup(p => p.IsInRole(userId, "Admin")).Returns(true);
            authProviderMock.Setup(p => p.IsInRole(userId, "Manager")).Returns(false);

            var model = new NavigationViewModel(true, false, true, username, userId);

            factoryMock.Setup(f => f.CreateNavigationViewModel(true, false, true, username, userId))
                .Returns(model);

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderPartialView("Navigation")
                .WithModel(model);
        }

        [Test]
        public void IndexShould_CallAuthProviderPropertyCurrentUserId_WhenUserIsManager()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var model = new NavigationViewModel(true, false, true, "", "");
            authProviderMock.Setup(p => p.IsAuthenticated).Returns(true);
            var userId = Guid.NewGuid().ToString();
            var username = "manager";
            authProviderMock.Setup(p => p.CurrentUserId).Returns(userId);
            authProviderMock.Setup(p => p.CurrentUserUsername).Returns(username);
            factoryMock.Setup(f => f.CreateNavigationViewModel(true, true, false, username, userId))
                .Returns(model);

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);
            controller.Index();

            authProviderMock.Verify(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void IndexShould_CallAuthProviderPropertyCurrentUsername_WhenUserIsManager()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var model = new NavigationViewModel(true, false, true, "", "");
            authProviderMock.Setup(p => p.IsAuthenticated).Returns(true);
            var userId = Guid.NewGuid().ToString();
            var username = "manager";
            authProviderMock.Setup(p => p.CurrentUserId).Returns(userId);
            authProviderMock.Setup(p => p.CurrentUserUsername).Returns(username);
            factoryMock.Setup(f => f.CreateNavigationViewModel(true, true, false, username, userId))
                .Returns(model);

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);
            controller.Index();

            authProviderMock.Verify(ap => ap.CurrentUserUsername, Times.Once);
        }

        [Test]
        public void IndexShould_ReturnPartialViewWithCorrectModel_WhenUserIsManager()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            authProviderMock.Setup(p => p.IsAuthenticated).Returns(true);
            var userId = Guid.NewGuid().ToString();
            var username = "manager";
            authProviderMock.Setup(p => p.CurrentUserId).Returns(userId);
            authProviderMock.Setup(p => p.CurrentUserUsername).Returns(username);
            authProviderMock.Setup(p => p.IsInRole(userId, "Admin")).Returns(false);
            authProviderMock.Setup(p => p.IsInRole(userId, "Manager")).Returns(true);

            var model = new NavigationViewModel(true, true, false, username, userId);

            factoryMock.Setup(f => f.CreateNavigationViewModel(true, true, false, username, userId))
                .Returns(model);

            var controller = new Web.Controllers.NavigationController(authProviderMock.Object, factoryMock.Object);
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderPartialView("Navigation")
                .WithModel(model);
        }
    }
}
