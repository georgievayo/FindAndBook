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
    public class DeleteAllShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void DeleteAllShould_CallRepositoryPropertyAll(string placeId)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            var placeIdGuid = new Guid(placeId);
            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.DeleteAll(placeIdGuid);

            repositoryMock.Verify(r => r.All, Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void DeleteAllShould_CallRepositoryMethodDeleteForEachReview(string placeId)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();
            var placeIdGuid = new Guid(placeId);
            var review = new Review() { PlaceId = placeIdGuid };
            var list = new List<Review>() {review};
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.DeleteAll(placeIdGuid);

            repositoryMock.Verify(r => r.Delete(review), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void DeleteAllShould_CallUnitOfWorkMethodCommitForEachReview(string placeId)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();
            var placeIdGuid = new Guid(placeId);
            var review = new Review() { PlaceId = placeIdGuid };
            var list = new List<Review>() { review };
            repositoryMock.Setup(r => r.All).Returns(list.AsQueryable());

            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.DeleteAll(placeIdGuid);

            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }
    }
}
