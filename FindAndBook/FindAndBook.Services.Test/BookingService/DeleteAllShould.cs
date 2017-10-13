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
    public class DeleteAllShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void DeleteAllShould_CallRepositoryPropertyAll(string placeId)
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var placeIdGuid = new Guid(placeId);
            var service = new Services.BookingService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.DeleteAll(placeIdGuid);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void DeleteAllShould_CallRepositoryMethodDeleteForEachBooking(string placeId)
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();
            var placeIdGuid = new Guid(placeId);
            var booking = new Booking() { PlaceId = placeIdGuid };
            var list = new List<Booking>() { booking };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.BookingService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.DeleteAll(placeIdGuid);

            repositoryMock.Verify(r => r.Delete(booking), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void DeleteAllShould_CallUnitOfWorkMethodCommitForEachBooking(string placeId)
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();
            var placeIdGuid = new Guid(placeId);
            var booking = new Booking() { PlaceId = placeIdGuid };
            var list = new List<Booking>() { booking };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.BookingService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.DeleteAll(placeIdGuid);

            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }

    }
}
