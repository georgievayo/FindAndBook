using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.UserServiceTest
{
    [TestFixture]
    public class AddUserShould
    {
        [TestCase("pesho", "pesho@gmail.com", "Pesho", "Peshov", "0863566565")]
        public void AddUserShould_CallFactoryMethodCreateUser(string username, string email, string firstName, string lastName, string phoneNumber)
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();

            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.CreateUser(username, email, firstName, lastName, phoneNumber);

            factoryMock.Verify(f => f.CreateUser(username, email, firstName, lastName, phoneNumber), Times.Once);
        }

        [TestCase("pesho", "pesho@gmail.com", "Pesho", "Peshov", "0863566565")]
        public void AddUserShould_ReturnCorretUser(string username, string email, string firstName, string lastName, string phoneNumber)
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();
            var user = new User(username, email, firstName, lastName, phoneNumber);

            factoryMock.Setup(f => f.CreateUser(username, email, firstName, lastName, phoneNumber)).Returns(user);

            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var result = service.CreateUser(username, email, firstName, lastName, phoneNumber);

            Assert.AreSame(user, result);
        }
    }
}
