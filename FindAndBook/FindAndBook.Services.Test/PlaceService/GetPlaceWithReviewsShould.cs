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

namespace FindAndBook.Services.Test.PlaceService
{
    [TestFixture]
    public class GetPlaceWithReviewsShould
    {
        [Test]
        public void GetPlaceWithReviewsShould_CallRepositoryMethodAll()
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();

            service.GetPlaceWithReviews(id);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void GetPlaceWithReviewsShould_ReturnCorrectResult()
        {
            var repositoryMock = new Mock<IRepository<Place>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IPlaceFactory>();

            var id = Guid.NewGuid();
            var place = new Place() { Id = id, Name = "My place" };
            var list = new List<Place>() { place };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.PlaceService(repositoryMock.Object,
                unitOfWorkMock.Object, factoryMock.Object);

            var result = service.GetPlaceWithReviews(id);

            Assert.AreSame(place, result.ToList().First());
        }
    }
}
