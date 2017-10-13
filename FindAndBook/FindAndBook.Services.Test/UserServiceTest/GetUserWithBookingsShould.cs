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

namespace FindAndBook.Services.Test.UserServiceTest
{
    [TestFixture]
    public class GetUserWithBookingsShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        [TestCase("99ae8dd3-1067-4141-9675-62e94bb6caaa")]
        public void GetUserWithBookingsShould_CallRepositoryPropertyAll(string id)
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();

            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.GetUserWithBookings(id);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        [TestCase("99ae8dd3-1067-4141-9675-62e94bb6caaa")]
        public void GetUserWithBookingsShould_ReturnCorrectUserWithBookings(string id)
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();
            var booking = new Booking() {Id = new Guid("d547a40d-c45f-4c43-99de-0bfe9199ff95")};
            var user = new User() {Id = id, Bookings = new List<Booking>() {booking}};

            repositoryMock.Setup(r => r.All).Returns(new List<User> {user}.AsQueryable<User>);
            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var result = service.GetUserWithBookings(id);

            Assert.AreSame(user, result);
        }
    }
}
