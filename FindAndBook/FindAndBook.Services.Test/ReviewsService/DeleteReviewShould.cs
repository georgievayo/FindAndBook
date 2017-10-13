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
    public class DeleteReviewShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void DeleteReviewShould_CallRepositoryMethodGetById(string id)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            var idGuid = new Guid(id);
            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.DeleteReview(idGuid);

            repositoryMock.Verify(r => r.GetById(idGuid), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void DeleteReviewShould_CallRepositoryMethodDelete(string id)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();
            var idGuid = new Guid(id);
            var review = new Review() { Id = idGuid, Message = "cool" };
            repositoryMock.Setup(r => r.GetById(idGuid)).Returns(review);

            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.DeleteReview(idGuid);

            repositoryMock.Verify(r => r.Delete(review), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95")]
        public void DeleteAllShould_CallUnitOfWorkMethodCommitForEachReview(string id)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();
            var idGuid = new Guid(id);
            var review = new Review() { PlaceId = idGuid };
            repositoryMock.Setup(r => r.GetById(id)).Returns(review);

            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);
            service.DeleteReview(idGuid);

            unitOfWorkMock.Verify(r => r.Commit(), Times.Once);
        }
    }
}
