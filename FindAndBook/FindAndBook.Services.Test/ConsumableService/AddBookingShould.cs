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

namespace FindAndBook.Services.Test.ConsumableService
{
    [TestFixture]
    public class AddBookingShould
    {
        [Test]
        public void AddBookingShould_CallUnitOfWorkMethodCommit()
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IConsumableFactory>();

            var service = new Services.ConsumableService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var booking = new Booking();
            var consumable = new Consumable() {Bookings = new List<Booking>()};
            service.AddBooking(consumable, booking);

            unitOfWorkMock.Verify(u => u.Commit(), Times.Once);
        }
    }
}
