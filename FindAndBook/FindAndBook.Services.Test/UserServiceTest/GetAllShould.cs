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
    public class GetAllShould
    {
        [Test]
        public void GetAllShould_CallRepositoryPropertyAll()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();

            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.GetAll();

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetUserWithBookingsShould_ReturnCorrectUserWithBookings()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();
            var user = new User() { UserName="user1" };

            repositoryMock.Setup(r => r.All).Returns(new List<User> { user }.AsQueryable<User>);
            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var result = service.GetAll();

            Assert.Contains(user, result.ToList());
        }
    }
}
