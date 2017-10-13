using System;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.BookingsController
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectly_WhenDataIsNotNull()
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

            Assert.IsNotNull(controller);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenAuthProviderIsNull()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var tableServiceMock = new Mock<ITablesService>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.BookingsController(null, placeServiceMock.Object,
                bookingsServiceMock.Object, factoryMock.Object, consumableServiceMock.Object,
                bookedTableServiceMock.Object, tableServiceMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPlaceServiceIsNull()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var tableServiceMock = new Mock<ITablesService>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.BookingsController(authProviderMock.Object, null,
                bookingsServiceMock.Object, factoryMock.Object, consumableServiceMock.Object,
                bookedTableServiceMock.Object, tableServiceMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenBookingServiceIsNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var placeServiceMock = new Mock<IPlaceService>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var tableServiceMock = new Mock<ITablesService>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.BookingsController(authProviderMock.Object, placeServiceMock.Object,
                null, factoryMock.Object, consumableServiceMock.Object,
                bookedTableServiceMock.Object, tableServiceMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenFactoryIsNull()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var tableServiceMock = new Mock<ITablesService>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.BookingsController(authProviderMock.Object, placeServiceMock.Object,
                bookingsServiceMock.Object, null, consumableServiceMock.Object,
                bookedTableServiceMock.Object, tableServiceMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenConsumableServiceIsNull()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var tableServiceMock = new Mock<ITablesService>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.BookingsController(authProviderMock.Object, placeServiceMock.Object,
                bookingsServiceMock.Object, factoryMock.Object, null,
                bookedTableServiceMock.Object, tableServiceMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenBookedTableServiceIsNull()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var tableServiceMock = new Mock<ITablesService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.BookingsController(authProviderMock.Object, placeServiceMock.Object,
                bookingsServiceMock.Object, factoryMock.Object, consumableServiceMock.Object,
               null, tableServiceMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenTableServiceIsNull()
        {
            var bookingsServiceMock = new Mock<IBookingService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var consumableServiceMock = new Mock<IConsumableService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var bookedTableServiceMock = new Mock<IBookedTablesService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.BookingsController(authProviderMock.Object, placeServiceMock.Object,
                bookingsServiceMock.Object, factoryMock.Object, consumableServiceMock.Object,
                bookedTableServiceMock.Object, null));
        }
    }
}
