using System;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.BookingsController
{
    [TestFixture]
    public class BookTablesShould
    {
        [Test]
        public void BookTablesShould_ReturnSameView_WhenModelIsNotValid()
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
            controller.ModelState.AddModelError("key", "message");

            var model = new BookingFormViewModel() { DateTime = DateTime.Now, PlaceId = Guid.NewGuid() };

            controller
                .WithCallTo(c => c.BookTables(model))
                .ShouldRenderPartialView("_AvailableTables")
                .WithModel(model);
        }

        [Test]
        public void BookTablesShould_CallAuthProviderPropertyCurrentUserId_WhenModelIsValid()
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

            var model = new BookingFormViewModel()
            {
                DateTime = DateTime.Now,
                PlaceId = Guid.NewGuid(),
                TwoPeopleInput = 1,
                FourPeopleInput = 0,
                SixPeopleInput = 0
            };
            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            var booking = new Booking() {Id = Guid.NewGuid()};
            bookingsServiceMock.Setup(s => s.CreateBooking(model.PlaceId, userId, model.DateTime, 2)).Returns(booking);
            var table = new Table() {Id = Guid.NewGuid()};
            tableServiceMock.Setup(s => s.GetByPlaceAndNumberOfPeople(model.PlaceId, 2)).Returns(table);
            controller.BookTables(model);

            authProviderMock.Verify(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void BookTablesShould_CallBookingServiceMethodCreateBooking_WhenModelIsValid()
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

            var model = new BookingFormViewModel()
            {
                DateTime = DateTime.Now,
                PlaceId = Guid.NewGuid(),
                TwoPeopleInput = 1,
                FourPeopleInput = 0,
                SixPeopleInput = 0
            };
            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            var booking = new Booking() { Id = Guid.NewGuid() };
            bookingsServiceMock.Setup(s => s.CreateBooking(model.PlaceId, userId, model.DateTime, 2)).Returns(booking);
            var table = new Table() { Id = Guid.NewGuid() };
            tableServiceMock.Setup(s => s.GetByPlaceAndNumberOfPeople(model.PlaceId, 2)).Returns(table);
            controller.BookTables(model);

            bookingsServiceMock.Verify(s => s.CreateBooking(model.PlaceId, userId, model.DateTime, 2), Times.Once);
        }

        [Test]
        public void BookTablesShould_CallTableServiceMethodGetByPlaceAndNumberOfPeople_WhenModelIsValid()
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

            var model = new BookingFormViewModel()
            {
                DateTime = DateTime.Now,
                PlaceId = Guid.NewGuid(),
                TwoPeopleInput = 1,
                FourPeopleInput = 0,
                SixPeopleInput = 0
            };
            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            var booking = new Booking() { Id = Guid.NewGuid() };
            bookingsServiceMock.Setup(s => s.CreateBooking(model.PlaceId, userId, model.DateTime, 2)).Returns(booking);
            var table = new Table() { Id = Guid.NewGuid() };
            tableServiceMock.Setup(s => s.GetByPlaceAndNumberOfPeople(model.PlaceId, 2)).Returns(table);
            controller.BookTables(model);

            tableServiceMock.Verify(s => s.GetByPlaceAndNumberOfPeople(model.PlaceId, 2), Times.Once);
        }

        [Test]
        public void BookTablesShould_CallBookedTableServiceMethodAddBookedTables_WhenModelIsValid()
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

            var model = new BookingFormViewModel()
            {
                DateTime = DateTime.Now,
                PlaceId = Guid.NewGuid(),
                TwoPeopleInput = 1,
                FourPeopleInput = 0,
                SixPeopleInput = 0
            };
            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            var booking = new Booking() { Id = Guid.NewGuid() };
            bookingsServiceMock.Setup(s => s.CreateBooking(model.PlaceId, userId, model.DateTime, 2)).Returns(booking);
            var table = new Table() { Id = Guid.NewGuid() };
            tableServiceMock.Setup(s => s.GetByPlaceAndNumberOfPeople(model.PlaceId, 2)).Returns(table);
            controller.BookTables(model);

            bookedTableServiceMock.Verify(s => s.AddBookedTables(booking.Id, table.Id, model.TwoPeopleInput), Times.Once);
        }

        [Test]
        public void BookTablesShould_ReturnViewWithCorrectModel_WhenModelIsValid()
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

            var model = new BookingFormViewModel()
            {
                DateTime = DateTime.Now,
                PlaceId = Guid.NewGuid(),
                TwoPeopleInput = 1,
                FourPeopleInput = 0,
                SixPeopleInput = 0
            };
            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            var booking = new Booking() { Id = Guid.NewGuid() };
            bookingsServiceMock.Setup(s => s.CreateBooking(model.PlaceId, userId, model.DateTime, 2)).Returns(booking);
            var table = new Table() { Id = Guid.NewGuid() };
            tableServiceMock.Setup(s => s.GetByPlaceAndNumberOfPeople(model.PlaceId, 2)).Returns(table);

            controller
                .WithCallTo(c => c.BookTables(model))
                .ShouldRenderPartialView("_SuccessfullBooking")
                .WithModel(model);
        }
    }
}
