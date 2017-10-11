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
    public class GetAllConsumablesOfShould
    {
        [Test]
        public void GetAllConsumablesOfShould_CallRepositoryMethodAll()
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IConsumableFactory>();

            var service = new Services.ConsumableService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();

            service.GetAllConsumablesOf(id);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetAllConsumablesOfShould_ReturnCorrectResult()
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IConsumableFactory>();

            var id = Guid.NewGuid();
            var consumable = new Consumable() { PlaceId = id, Name = "Pizza" };
            var list = new List<Consumable>() { consumable };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.ConsumableService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.GetAllConsumablesOf(id);

            Assert.AreSame(consumable, result.ToList().First());
        }
    }
}
