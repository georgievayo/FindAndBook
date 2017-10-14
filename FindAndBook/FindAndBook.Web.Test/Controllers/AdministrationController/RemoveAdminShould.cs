using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.AdministrationController
{
    [TestFixture]
    public class RemoveAdminShould
    {
        [Test]
        public void RemoveAdminShould_CallAuthProviderMethodRemoveFromRole()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, factoryMock.Object);

            var id = Guid.NewGuid().ToString();
            controller.RemoveAdmin(id);

            authProviderMock.Verify(ap => ap.RemoveFromRole(id, "Admin"), Times.Once);
        }

        [Test]
        public void RemoveAdminShould_ReturnPartialViewWithCorrectModel()
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
                .WithCallTo(c => c.RemoveAdmin(id))
                .ShouldRenderPartialView("_AddToRole")
                .WithModel(id);
        }
    }
}
