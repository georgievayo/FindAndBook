using System;
using System.Collections.Generic;
using System.Linq;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.BookingsController
{
    [TestFixture]
    public class GetBookingsShould
    {
        [Test]
        public void GetBookingsShould_ReturnErrorView_WhenPassedIdIsNull()
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
                .WithCallTo(c => c.GetBookings(null))
                .ShouldRenderView("Error");
        }

        [Test]
        public void GetBookingsShould_CallBookingServiceMethodGetBookingsOfPlace_WhenPassedIdIsNotNull()
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
            controller.GetBookings(placeId.ToString());

            bookingsServiceMock.Verify(s => s.GetBookingsOfPlace(placeId), Times.Once);
        }

        [Test]
        public void GetBookingsShould_ReturnViewWithCorrectModel_WhenPassedIdIsNotNull()
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
            var booking = new Booking() {PlaceId = placeId};
            var model = new List<Booking>() {booking};
            bookingsServiceMock.Setup(s => s.GetBookingsOfPlace(placeId)).Returns(model.AsQueryable());

            controller
                .WithCallTo(c => c.GetBookings(placeId.ToString()))
                .ShouldRenderPartialView("_PlaceBookings");

        }
    }
}
