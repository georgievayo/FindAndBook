using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.ConsumableService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectlly_WhenDataIsNotNull()
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IConsumableFactory>();

            var service = new Services.ConsumableService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            Assert.IsNotNull(service);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenRepositoryIsNull()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IConsumableFactory>();

            Assert.Throws<ArgumentNullException>(() => new Services.ConsumableService(null,
                unitOfWorkMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenUnitOfWorkIsNull()
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var factoryMock = new Mock<IConsumableFactory>();

            Assert.Throws<ArgumentNullException>(() => new Services.ConsumableService(repositoryMock.Object,
                null, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenFactoryIsNull()
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => new Services.ConsumableService(repositoryMock.Object,
                unitOfWorkMock.Object, null));
        }
    }
}
