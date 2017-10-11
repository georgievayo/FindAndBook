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
    public class AddReviewShould
    {
        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", 
            "d547a474-c45f-4c43-99de-0bfe9199ff95", "5/1/2008 8:30:52 AM", "cool", 5)]
        public void AddReviewShould_CallFactoryMethodCreateReview(string placeId, string userId, 
            string postedOn, string message, int rating)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            var placeIdGuid = new Guid(placeId);
            var postedOnDateTime = Convert.ToDateTime(postedOn);
            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.AddReview(placeIdGuid, userId, postedOnDateTime, message, rating);

            factoryMock.Verify(f => f.CreateReview(placeIdGuid, userId, postedOnDateTime, message, rating), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", "d547a474-c45f-4c43-99de-0bfe9199ff95",
            "5/1/2008 8:30:52 AM", "cool", 5)]
        public void AddReviewShould_CallRepositoryMethodAdd(string placeId, string userId,
            string postedOn, string message, int rating)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            var placeIdGuid = new Guid(placeId);
            var postedOnDateTime = Convert.ToDateTime(postedOn);
            var review = new Review()
            {
                PlaceId = placeIdGuid,
                UserId = userId,
                PostedOn = postedOnDateTime,
                Message = message,
                Rating = rating
            };
            factoryMock.Setup(f => f.CreateReview(placeIdGuid, userId, postedOnDateTime, message, rating))
                .Returns(review);
            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.AddReview(placeIdGuid, userId, postedOnDateTime, message, rating);

            repositoryMock.Verify(f => f.Add(review), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", "d547a474-c45f-4c43-99de-0bfe9199ff95",
            "5/1/2008 8:30:52 AM", "cool", 5)]
        public void AddReviewShould_CallUnitOfWorkMethodCommit(string placeId, string userId,
            string postedOn, string message, int rating)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            var placeIdGuid = new Guid(placeId);
            var postedOnDateTime = Convert.ToDateTime(postedOn);
            var review = new Review()
            {
                PlaceId = placeIdGuid,
                UserId = userId,
                PostedOn = postedOnDateTime,
                Message = message,
                Rating = rating
            };
            factoryMock.Setup(f => f.CreateReview(placeIdGuid, userId, postedOnDateTime, message, rating))
                .Returns(review);
            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            service.AddReview(placeIdGuid, userId, postedOnDateTime, message, rating);

            unitOfWorkMock.Verify(f => f.Commit(), Times.Once);
        }

        [TestCase("d547a40d-c45f-4c43-99de-0bfe9199ff95", "d547a474-c45f-4c43-99de-0bfe9199ff95",
            "5/1/2008 8:30:52 AM", "cool", 5)]
        public void AddReviewShould_ReturnCorrectResult(string placeId, string userId,
            string postedOn, string message, int rating)
        {
            var repositoryMock = new Mock<IRepository<Review>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var factoryMock = new Mock<IReviewsFactory>();

            var placeIdGuid = new Guid(placeId);
            var postedOnDateTime = Convert.ToDateTime(postedOn);
            var review = new Review()
            {
                PlaceId = placeIdGuid,
                UserId = userId,
                PostedOn = postedOnDateTime,
                Message = message,
                Rating = rating
            };
            factoryMock.Setup(f => f.CreateReview(placeIdGuid, userId, postedOnDateTime, message, rating))
                .Returns(review);
            var service = new Services.ReviewsService(repositoryMock.Object, unitOfWorkMock.Object, factoryMock.Object);

            var result = service.AddReview(placeIdGuid, userId, postedOnDateTime, message, rating);

            Assert.AreSame(review, result);
        }
    }
}
