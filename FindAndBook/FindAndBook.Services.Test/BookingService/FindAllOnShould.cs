using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.BookingService
{
    [TestFixture]
    public class FindAllOnShould
    {
        [Test]
        public void FindAllOnShould_CallRepositoryMethodAll()
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var placeId = Guid.NewGuid();
            var dateTime = DateTime.Now;

            service.FindAllOn(dateTime, placeId);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void FindAllOnShould_ReturnCorrectResult()
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var placeId = Guid.NewGuid();
            var dateTime = DateTime.Now;
            var booking = new Booking() { PlaceId = placeId, DateTime = dateTime};
            var list = new List<Booking>() { booking };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.FindAllOn(dateTime, placeId);

            Assert.AreSame(booking, result.ToList().First());
        }
    }
}
