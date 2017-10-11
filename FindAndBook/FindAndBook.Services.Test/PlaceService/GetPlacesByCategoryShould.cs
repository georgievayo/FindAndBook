using System;
using System.Collections.Generic;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Services.Test.PlaceService
{
    [TestFixture]
    public class GetPlacesByCategoryShould
    {
        [TestCase("Restaurant")]
        [TestCase("Club")]
        [TestCase("Cafe")]
        public void GetPlacesByCategoryShould_CallRepositoryMethodAll(string category)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            service.GetPlacesByCategory(category);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("Restaurant")]
        [TestCase("Club")]
        [TestCase("Cafe")]
        public void GetPlacesByCategoryShould_ReturnCorrectResult(string category)
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var managerId = Guid.NewGuid().ToString();
            var place = new Place() { Type = category, Name = "My place" };
            var list = new List<Place>() { place };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.GetPlacesByCategory(category);

            Assert.Contains(place, result.ToList());
        }
    }
}
