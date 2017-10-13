using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.PlacesController
{
    [TestFixture]
    public class MyPlacesShould
    {
        [Test]
        public void MyPlacesShould_CallAuthProviderPropertyCurrentUserId()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);

            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);

            controller.MyPlaces();

            authProviderMock.Verify(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void MyPlacesShould_CallPlaceServiceMethodGetUserPlaces()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);

            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);

            controller.MyPlaces();

            placeServiceMock.Verify(f => f.GetUserPlaces(userId), Times.Once);
        }

        [Test]
        public void MyPlacesShould_ReturnDefaultView()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);

            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            var model = new List<Place>();
            placeServiceMock.Setup(s => s.GetUserPlaces(userId)).Returns(model.AsQueryable());

            controller
                .WithCallTo(c => c.MyPlaces())
                .ShouldRenderDefaultView();
        }
    }
}
