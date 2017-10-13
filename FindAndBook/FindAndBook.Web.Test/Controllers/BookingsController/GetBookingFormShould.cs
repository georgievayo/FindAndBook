using System;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.BookingsController
{
    [TestFixture]
    public class GetBookingFormShould
    {
        [Test]
        public void GetBookingFormShould_ReturnErrorView_WhenPassedIdIsNull()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var tableServiceMock = new Mock<ITablesService>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.BookingsController(authProviderMock.Object, placeServiceMock.Object,
                bookingsServiceMock.Object, factoryMock.Object, consumableServiceMock.Object,
                bookedTableServiceMock.Object, tableServiceMock.Object);

            controller
                .WithCallTo(c => c.GetBookingForm(null))
                .ShouldRenderView("Error");
        }

        [Test]
        public void GetBookingFormShould_CallFactoryMethodCreateBookingFormViewModel_WhenPassedIdIsNotNull()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var tableServiceMock = new Mock<ITablesService>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.BookingsController(authProviderMock.Object, placeServiceMock.Object,
                bookingsServiceMock.Object, factoryMock.Object, consumableServiceMock.Object,
                bookedTableServiceMock.Object, tableServiceMock.Object);

            var placeId = Guid.NewGuid();
            controller.GetBookingForm(placeId);

            factoryMock.Verify(f => f.CreateBookingFormViewModel(placeId), Times.Once);
        }

        [Test]
        public void GetBookingFormShould_ReturnBookingDateTimePartialView_WhenUserIsAuthenticated()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var tableServiceMock = new Mock<ITablesService>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.BookingsController(authProviderMock.Object, placeServiceMock.Object,
                bookingsServiceMock.Object, factoryMock.Object, consumableServiceMock.Object,
                bookedTableServiceMock.Object, tableServiceMock.Object);

            var placeId = Guid.NewGuid();
            var model = new BookingViewModel() {PlaceId = placeId};
            factoryMock.Setup(f => f.CreateBookingFormViewModel(placeId)).Returns(model);
            authProviderMock.Setup(ap => ap.IsAuthenticated).Returns(true);

            controller.WithCallTo(c => c.GetBookingForm(placeId))
                .ShouldRenderPartialView("_BookingDateTimeForm")
                .WithModel(model);
        }

        [Test]
        public void GetBookingFormShould_ReturnLoginMessageView_WhenUserIsNotAuthenticated()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var tableServiceMock = new Mock<ITablesService>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.BookingsController(authProviderMock.Object, placeServiceMock.Object,
                bookingsServiceMock.Object, factoryMock.Object, consumableServiceMock.Object,
                bookedTableServiceMock.Object, tableServiceMock.Object);

            var placeId = Guid.NewGuid();
            var model = new BookingViewModel() { PlaceId = placeId };
            factoryMock.Setup(f => f.CreateBookingFormViewModel(placeId)).Returns(model);
            authProviderMock.Setup(ap => ap.IsAuthenticated).Returns(false);

            controller.WithCallTo(c => c.GetBookingForm(placeId))
                .ShouldRenderPartialView("_LoginMessage");
        }
    }
}
