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
    public class GetUserPlacesShould
    {
        [Test]
        public void GetUserPlacesShould_CallRepositoryMethodAll()
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();

            service.GetUserPlaces(id.ToString());

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetPlaceWithReviewsShould_ReturnCorrectResult()
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var managerId = Guid.NewGuid().ToString();
            var place = new Place() { ManagerId = managerId, Name = "My place" };
            var list = new List<Place>() { place };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.GetUserPlaces(managerId);

            Assert.Contains(place, result.ToList());
        }
    }
}
