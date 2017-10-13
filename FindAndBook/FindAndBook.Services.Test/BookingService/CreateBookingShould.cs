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
    public class CreateBookingShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 10)]
        public void CreateBookingShould_CallFactoryMethodCreateBooking(string userId, int people)
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var placeId = Guid.NewGuid();
            var dateTime = DateTime.Now;

            service.CreateBooking(placeId, userId, dateTime, people);

            factoryMock.Verify(f => f.CreateBooking(placeId, userId, dateTime, people));
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 5)]
        public void CreateBookingShould_CallRepositoryMethodAdd(string userId, int people)
        {
            var repositoryMock = new Mock<IRepository<Booking>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IBookingFactory>();

            var service = new Services.BookingService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var placeId = Guid.NewGuid();
            var dateTime = DateTime.Now;
            var booking = new Booking()
            {
                PlaceId = placeId,
                UserId = userId,
                DateTime = dateTime,
                NumberOfPeople = people
            };
            factoryMock.Setup(f => f.CreateBooking(placeId,userId, dateTime, people))
                .Returns(booking);

            service.CreateBooking(placeId, userId, dateTime, people);

            repositoryMock.Verify(r => r.Add(booking), Times.Once);
        }

    }
}
