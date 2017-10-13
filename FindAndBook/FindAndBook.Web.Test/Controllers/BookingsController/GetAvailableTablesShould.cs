using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetAvailableTablesShould
    {
        [Test]
        public void GetAvailableTablesShould_ReturnSameView_WhenModelIsNotValid()
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

            var model = new BookingViewModel();

            controller
                .WithCallTo(c => c.GetAvailableTables(model))
                .ShouldRenderPartialView("_BookingDateTimeForm")
                .WithModel(model);
        }

        [Test]
        public void GetAvailableTablesShould_CallBookingServiceMethodFindAllOn_WhenModelIsValid()
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

            var model = new BookingViewModel() {DateTime = DateTime.Now, PlaceId = Guid.NewGuid()};

            controller.GetAvailableTables(model);

            bookingsServiceMock.Verify(s => s.FindAllOn(model.DateTime, model.PlaceId), Times.Once);
        }

        [Test]
        public void GetAvailableTablesShould_CallBookedTableServiceMethodGetBookedTable_WhenModelIsValid()
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

            var model = new BookingViewModel() { DateTime = DateTime.Now, PlaceId = Guid.NewGuid() };
            var booking = new Booking() {Id = Guid.NewGuid()};
            var list = new List<Booking>() {booking};
            var table = new Table() {Id = Guid.NewGuid(), NumberOfPeople = 2};
            var bookedTable = new BookedTables() {BookingId = booking.Id, TableId = table.Id, Table = table, TablesCount = 3};
            bookingsServiceMock.Setup(s => s.FindAllOn(model.DateTime, model.PlaceId)).Returns(list.AsQueryable());
            bookedTableServiceMock.Setup(s => s.GetBookedTable(booking.Id)).Returns(bookedTable);

            controller.GetAvailableTables(model);

            bookedTableServiceMock.Verify(s => s.GetBookedTable(booking.Id), Times.Once);
        }

        [TestCase(2)]
        [TestCase(4)]
        [TestCase(6)]
        public void GetAvailableTablesShould_CallTableServiceMethodGetTablesCount_WhenModelIsValid(int peoplePerTable)
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

            var model = new BookingViewModel() { DateTime = DateTime.Now, PlaceId = Guid.NewGuid() };
            var booking = new Booking() { Id = Guid.NewGuid() };
            var list = new List<Booking>() { booking };
            var table = new Table() { Id = Guid.NewGuid(), NumberOfPeople = peoplePerTable };
            var bookedTable = new BookedTables() { BookingId = booking.Id, TableId = table.Id, Table = table, TablesCount = 3 };
            bookingsServiceMock.Setup(s => s.FindAllOn(model.DateTime, model.PlaceId)).Returns(list.AsQueryable());
            bookedTableServiceMock.Setup(s => s.GetBookedTable(booking.Id)).Returns(bookedTable);
            tableServiceMock.Setup(s => s.GetTablesCount(model.PlaceId, peoplePerTable)).Returns(10);

            controller.GetAvailableTables(model);

            tableServiceMock.Verify(s => s.GetTablesCount(model.PlaceId, 2), Times.Once);
            tableServiceMock.Verify(s => s.GetTablesCount(model.PlaceId, 4), Times.Once);
            tableServiceMock.Verify(s => s.GetTablesCount(model.PlaceId, 6), Times.Once);
        }

        [TestCase(2)]
        public void GetAvailableTablesShould_CallFactoryMethodCreateBookingFormViewModel_WhenModelIsValid(int peoplePerTable)
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

            var model = new BookingViewModel() { DateTime = DateTime.Now, PlaceId = Guid.NewGuid() };
            var booking = new Booking() { Id = Guid.NewGuid() };
            var list = new List<Booking>() { booking };
            var table = new Table() { Id = Guid.NewGuid(), NumberOfPeople = peoplePerTable };
            var bookedTable = new BookedTables() { BookingId = booking.Id, TableId = table.Id, Table = table, TablesCount = 3 };
            bookingsServiceMock.Setup(s => s.FindAllOn(model.DateTime, model.PlaceId)).Returns(list.AsQueryable());
            bookedTableServiceMock.Setup(s => s.GetBookedTable(booking.Id)).Returns(bookedTable);
            tableServiceMock.Setup(s => s.GetTablesCount(model.PlaceId, peoplePerTable)).Returns(10);

            controller.GetAvailableTables(model);

            factoryMock.Verify(f => f.CreateBookingFormViewModel(7, 0, 0, model.PlaceId, model.DateTime));
        }

        [TestCase(2)]
        public void GetAvailableTablesShould_ReturnViewWithCorrectModel_WhenModelIsValid(int peoplePerTable)
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

            var model = new BookingViewModel() { DateTime = DateTime.Now, PlaceId = Guid.NewGuid() };
            var booking = new Booking() { Id = Guid.NewGuid() };
            var list = new List<Booking>() { booking };
            var table = new Table() { Id = Guid.NewGuid(), NumberOfPeople = peoplePerTable };
            var bookedTable = new BookedTables() { BookingId = booking.Id, TableId = table.Id, Table = table, TablesCount = 3 };
            bookingsServiceMock.Setup(s => s.FindAllOn(model.DateTime, model.PlaceId)).Returns(list.AsQueryable());
            bookedTableServiceMock.Setup(s => s.GetBookedTable(booking.Id)).Returns(bookedTable);
            tableServiceMock.Setup(s => s.GetTablesCount(model.PlaceId, peoplePerTable)).Returns(10);
            var returnModel = new BookingFormViewModel()
            {
                TwoPeopleTablesCount = 7,
                FourPeopleTablesCount = 0,
                SixPeopleTablesCount = 0,
                DateTime = model.DateTime,
                PlaceId = model.PlaceId
            };

            factoryMock.Setup(f => f.CreateBookingFormViewModel(7, 0, 0, model.PlaceId, model.DateTime)).Returns(returnModel);

            controller
                .WithCallTo(c => c.GetAvailableTables(model))
                .ShouldRenderPartialView("_AvailableTables")
                .WithModel(returnModel);
        }
    }
}
