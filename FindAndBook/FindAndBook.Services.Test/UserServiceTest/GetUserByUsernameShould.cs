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
    public class GetUserByUsernameShould
    {
        [TestCase("user1")]
        [TestCase("user2")]

        public void GetUserByUsernameShould_CallRepositoryPropertyAll(string username)
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();

            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.GetUserByUsername(username);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("user1")]
        [TestCase("user2")]

        public void GetUserByUsernameShould_ReturnCorrectUser(string username)
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();
            var foundUser = new User() {UserName = username};

            repositoryMock.Setup(r => r.All).Returns(new List<User> { foundUser}.AsQueryable());

            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var result = service.GetUserByUsername(username);

            Assert.Contains(foundUser, result.ToList());
        }
    }
}
