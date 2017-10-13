using System;
using System.Collections.Generic;
using System.Linq;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Bookings;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.BookingsController
{
    [TestFixture]
    public class GetOrderShould
    {
        [Test]
        public void BookTablesShould_ReturnErrorView_WhenPassedPlaceIdIsNull()
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
            var bookingId = Guid.NewGuid();

            controller
                .WithCallTo(c => c.Order(null, bookingId))
                .ShouldRenderView("Error");
        }

        [Test]
        public void BookTablesShould_ReturnErrorView_WhenPassedBookingIdIsNull()
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

            controller
                .WithCallTo(c => c.Order(placeId, null))
                .ShouldRenderView("Error");
        }

        [Test]
        public void BookTablesShould_CallConsumableService_WhenPassedDataIsNotNull()
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
            var bookingId = Guid.NewGuid();
            var placeId = Guid.NewGuid();

            controller.Order(placeId, bookingId);

            consumableServiceMock.Verify(s => s.GetAllConsumablesOf(placeId), Times.Once);
        }

        [Test]
        public void BookTablesShould_CallFactoryMethodCreateOrderFormViewModel_WhenPassedDataIsNotNull()
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
            var bookingId = Guid.NewGuid();
            var placeId = Guid.NewGuid();
            var consumable = new Consumable();
            var list = new List<Consumable>() {consumable};
            consumableServiceMock.Setup(s => s.GetAllConsumablesOf(placeId)).Returns(list.AsQueryable());

            controller.Order(placeId, bookingId);

            factoryMock.Verify(f => f.CreateOrderFormViewModel(bookingId, list), Times.Once);
        }

        [Test]
        public void BookTablesShould_ReturnViewWithCorrectModel_WhenPassedDataIsNotNull()
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
            var bookingId = Guid.NewGuid();
            var placeId = Guid.NewGuid();
            var consumable = new Consumable();
            var list = new List<Consumable>() { consumable };
            consumableServiceMock.Setup(s => s.GetAllConsumablesOf(placeId)).Returns(list.AsQueryable());
            var model = new OrderFormViewModel() {BookingId = bookingId, PlaceMenu = list};
            factoryMock.Setup(f => f.CreateOrderFormViewModel(bookingId, list)).Returns(model);

            controller
                .WithCallTo(c => c.Order(placeId, bookingId))
                .ShouldRenderDefaultView()
                .WithModel(model);
        }
    }
}
