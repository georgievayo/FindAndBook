using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.BookingService
{
    [TestFixture]
    public class RemoveBookingShould
    {
        [Test]
        public void RemoveBookingShould_CallRepositoryMethodGetById()
        {
            var id = Guid.NewGuid();
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.RemoveBooking(id);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void RemoveBookingShould_CallRepositoryMethodDelete()
        {
            var id = Guid.NewGuid();
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var booking = new Booking() { Id = id };
            repositoryMock.Setup(r => r.GetById(id)).Returns(booking);

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.RemoveBooking(id);

            repositoryMock.Verify(r => r.Delete(booking), Times.Once);
        }

        [Test]
        public void RemoveBookingShould_CallUnitOfWorkMethodCommit()
        {
            var id = Guid.NewGuid();
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var booking = new Booking() { Id = id };
            repositoryMock.Setup(r => r.GetById(id)).Returns(booking);

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.RemoveBooking(id);

            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }
    }
}
