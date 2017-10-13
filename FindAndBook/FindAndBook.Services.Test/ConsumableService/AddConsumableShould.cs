using System;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.ConsumableService
{
    [TestFixture]
    public class AddConsumableShould
    {
        [TestCase("Pizza", 500, 10.25, "Meal", "tomatoes, meat, cheese")]
        public void AddConsumableShould_CallFactoryMethodCreateConsumable(string name, int quantity, 
            decimal? price, string type, string ingredients)
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IConsumableFactory>();

            var service = new Services.ConsumableService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var placeId = Guid.NewGuid();
            service.AddConsumable(placeId, name, quantity, price, type, ingredients);

            factoryMock.Verify(f => f.CreateConsumable(placeId, name, quantity, price, type, ingredients));
        }

        [TestCase("Pizza", 500, 10.25, "Meal", "tomatoes, meat, cheese")]
        public void AddConsumableShould_CallRepositoryMethodAdd(string name, int quantity,
            decimal? price, string type, string ingredients)
        {
            var repositoryMock = new Mock<IRepository<Consumable>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IConsumableFactory>();

            var service = new Services.ConsumableService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var placeId = Guid.NewGuid();
            var consumable = new Consumable()
            {
                PlaceId = placeId,
                Name = name,
                Quantity = quantity,
                Price = price,
                Type = type,
                Ingredients = ingredients
            };
            factoryMock.Setup(f => f.CreateConsumable(placeId, name, quantity, price, type, ingredients))
                .Returns(consumable);

            service.AddConsumable(placeId, name, quantity, price, type, ingredients);

            repositoryMock.Verify(r => r.Add(consumable), Times.Once);
        }
    }
}
