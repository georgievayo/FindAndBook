using System;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.AdministrationController
{
    [TestFixture]
    public class AddAdminShould
    {
        [Test]
        public void AddAdminShould_CallAuthProviderMethodAddToRole()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, factoryMock.Object);

            var id = Guid.NewGuid().ToString();
            controller.AddAdmin(id);

            authProviderMock.Verify(ap => ap.AddToRole(id, "Admin"), Times.Once);
        }

        [Test]
        public void AddAdminShould_ReturnPartialViewWithCorrectModel()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, factoryMock.Object);

            var id = Guid.NewGuid().ToString();
            controller
                .WithCallTo(c => c.AddAdmin(id))
                .ShouldRenderPartialView("_RemoveFromRole")
                .WithModel(id);
        }
    }
}
