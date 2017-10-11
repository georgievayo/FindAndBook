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
    public class GetUserByIdShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        [TestCase("99ae8dd3-1067-4141-9675-62e94bb6caaa")]
        public void GetUserByIdShould_CallRepositoryMethodGetById(string id)
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();

            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.GetUserById(id);

            repositoryMock.Verify(r => r.GetById(id), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        [TestCase("99ae8dd3-1067-4141-9675-62e94bb6caaa")]
        public void GetUserByIdShould_ReturnCorrectUser(string id)
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();
            var foundUserMock = new Mock<User>();

            repositoryMock.Setup(r => r.GetById(id)).Returns(foundUserMock.Object);

            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var result = service.GetUserById(id);

            Assert.AreSame(foundUserMock.Object, result);
        }
    }
}
