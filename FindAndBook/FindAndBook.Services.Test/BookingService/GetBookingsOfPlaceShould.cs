using System;
using System.Collections.Generic;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.BookingService
{
    [TestFixture]
    public class GetBookingsOfPlaceShould
    {
        [Test]
        public void GetBookingsOfPlaceShould_CallRepositoryMethodAll()
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();

            service.GetBookingsOfPlace(id);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetBookingsOfPlaceShould_ReturnCorrectResult()
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var id = Guid.NewGuid();
            var place = new Booking() { PlaceId = id };
            var list = new List<Booking>() { place };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.GetBookingsOfPlace(id);

            Assert.AreSame(place, result.ToList().First());
        }
    }
}
