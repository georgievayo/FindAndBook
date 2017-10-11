using System.Collections.Generic;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.ConsumableService
{
    [TestFixture]
    public class GetByNameShould
    {
        [TestCase("Pizza")]
        [TestCase("Ice cream")]
        public void GetAllConsumablesOfShould_CallRepositoryMethodAll(string name)
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IConsumableFactory>();

            var service = new Services.ConsumableService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            
            service.GetByName(name);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("Pizza")]
        public void GetAllConsumablesOfShould_ReturnCorrectResult(string name)
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IConsumableFactory>();

            var consumable = new Consumable() { Name = "Pizza" };

            var list = new List<Consumable>() { consumable };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.ConsumableService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.GetByName(name);

            Assert.AreSame(consumable, result);
        }
    }
}
