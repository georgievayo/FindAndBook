using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.UserServiceTest
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectlly_WhenDataIsNotNull()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();

            var service = new UserService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            Assert.IsNotNull(service);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPassedRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IUserFactory>();

            Assert.Throws<ArgumentNullException>(
                () => new UserService(null, unitOfWorkMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPassedUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var factoryMock = new Mock<IUserFactory>();

            Assert.Throws<ArgumentNullException>(
                () => new UserService(repositoryMock.Object, null, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPassedFactoryIsNull()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(
                () => new UserService(repositoryMock.Object, unitOfWorkMock.Object, null));
        }
    }
}
