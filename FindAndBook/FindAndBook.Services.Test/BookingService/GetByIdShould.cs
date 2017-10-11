using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.BookingService
{
    [TestFixture]
    public class GetByIdShould
    {
        [Test]
        public void GetByIdShould_CallRepositoryMethodGetById()
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var id = Guid.NewGuid();
            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.GetById(id);

            repositoryMock.Verify(r => r.GetById(id));
        }

        [Test]
        public void GetByIdShould_ReturnCorrectValue()
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var id = Guid.NewGuid();
            var booking = new Booking() {Id = id};
            repositoryMock.Setup(r => r.GetById(id)).Returns(booking);

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.GetById(id);

            Assert.AreSame(booking, result);
        }
    }
}
