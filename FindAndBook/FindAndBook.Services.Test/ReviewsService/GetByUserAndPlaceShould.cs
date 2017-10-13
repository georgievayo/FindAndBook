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

namespace FindAndBook.Services.Test.ReviewsService
{
    [TestFixture]
    public class GetByUserAndPlaceShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", "c147a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void GetAllShould_CallRepositoryPropertyAll(string placeId, string userId)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();
            var placeIdGuid = new Guid(placeId);
            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.GetByUserAndPlace(placeIdGuid, userId);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", "c147a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void GetAllShould_ReturnCorrectResult(string placeId, string userId)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();
            var placeIdGuid = new Guid(placeId);
            var review = new Review() { PlaceId = placeIdGuid, UserId = userId };
            var all = new List<Review>() { review };
            repositoryMock.Setup(r => r.All).Returns(all.AsQueryable());

            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            var result = service.GetByUserAndPlace(placeIdGuid, userId);

            Assert.Contains(review, result.ToList());
        }
    }
}
