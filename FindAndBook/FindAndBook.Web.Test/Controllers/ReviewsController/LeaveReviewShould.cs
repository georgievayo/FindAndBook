using System;
using System.Collections.Generic;
using System.Linq;
using FindAndBook.Models;
using FindAndBook.Providers.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Models.Reviews;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.ReviewsController
{
    [TestFixture]
    public class LeaveReviewShould
    {
        [Test]
        public void LeaveReviewShould_ReturnPartialViewOfModel_WhenModelIsNotValid()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var controller = new Web.Controllers.ReviewsController(reviewsServiceMock.Object, 
                placeServiceMock.Object, dateTimeProviderMock.Object);
            controller.ModelState.AddModelError("key", "message");
            var model = new SingleReviewViewModel();

            controller
                .WithCallTo(c => c.LeaveReview(model))
                .ShouldRenderPartialView("_ReviewForm")
                .WithModel(model);
        }

        [Test]
        public void LeaveReviewShould_CallReviewsServiceMethodGetByUserAndPlace_WhenModelIsValid()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var controller = new Web.Controllers.ReviewsController(reviewsServiceMock.Object,
                placeServiceMock.Object, dateTimeProviderMock.Object);

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var model = new SingleReviewViewModel()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var review = new Review()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var emptyList = new List<Review>();

            var currentTime = DateTime.Now;
            dateTimeProviderMock.Setup(dt => dt.GetCurrentTime()).Returns(currentTime);

            reviewsServiceMock.Setup(s => s.GetByUserAndPlace(model.PlaceId, model.UserId))
                .Returns(emptyList.AsQueryable());

            reviewsServiceMock.Setup(s => s.AddReview(model.PlaceId, model.UserId, currentTime, model.Message, model.Rating))
                .Returns(review);
            controller.LeaveReview(model);

            reviewsServiceMock.Verify(s => s.GetByUserAndPlace(model.PlaceId, model.UserId), Times.Once);
        }

        [Test]
        public void LeaveReviewShould_ReturnJson_WhenUserHasAlreadyLeftReview()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var controller = new Web.Controllers.ReviewsController(reviewsServiceMock.Object,
                placeServiceMock.Object, dateTimeProviderMock.Object);

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var model = new SingleReviewViewModel()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var review = new Review()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var list = new List<Review>()
            {
                review
            };

            var currentTime = DateTime.Now;
            dateTimeProviderMock.Setup(dt => dt.GetCurrentTime()).Returns(currentTime);

            reviewsServiceMock.Setup(s => s.GetByUserAndPlace(model.PlaceId, model.UserId))
                .Returns(list.AsQueryable());

            reviewsServiceMock.Setup(s => s.AddReview(model.PlaceId, model.UserId, currentTime, model.Message, model.Rating))
                .Returns(review);

            controller
                .WithCallTo(c => c.LeaveReview(model))
                .ShouldReturnJson();
        }

        [Test]
        public void LeaveReviewShould_CallDateTimeProviderMethodGetCurrentTime_WhenUserHasnotAlreadyLeftReview()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var controller = new Web.Controllers.ReviewsController(reviewsServiceMock.Object,
                placeServiceMock.Object, dateTimeProviderMock.Object);

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var model = new SingleReviewViewModel()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var review = new Review()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var emptyList = new List<Review>();

            var currentTime = DateTime.Now;
            dateTimeProviderMock.Setup(dt => dt.GetCurrentTime()).Returns(currentTime);

            reviewsServiceMock.Setup(s => s.GetByUserAndPlace(model.PlaceId, model.UserId))
                .Returns(emptyList.AsQueryable());

            reviewsServiceMock.Setup(s => s.AddReview(model.PlaceId, model.UserId, currentTime, model.Message, model.Rating))
                .Returns(review);
            controller.LeaveReview(model);

            dateTimeProviderMock.Verify(s => s.GetCurrentTime(), Times.Once);
        }

        [Test]
        public void LeaveReviewShould_CallReviewsServiceMethodAddReview_WhenUserHasnotAlreadyLeftReview()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var controller = new Web.Controllers.ReviewsController(reviewsServiceMock.Object,
                placeServiceMock.Object, dateTimeProviderMock.Object);

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var model = new SingleReviewViewModel()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var review = new Review()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var emptyList = new List<Review>();

            var currentTime = DateTime.Now;
            dateTimeProviderMock.Setup(dt => dt.GetCurrentTime()).Returns(currentTime);

            reviewsServiceMock.Setup(s => s.GetByUserAndPlace(model.PlaceId, model.UserId))
                .Returns(emptyList.AsQueryable());

            reviewsServiceMock.Setup(s => s.AddReview(model.PlaceId, model.UserId, currentTime, model.Message, model.Rating))
                .Returns(review);
            controller.LeaveReview(model);

            reviewsServiceMock.Verify(s => s.AddReview(model.PlaceId, model.UserId, currentTime, model.Message, model.Rating), Times.Once);
        }

        [Test]
        public void LeaveReviewShould_ReturnCorrectPartialView_WhenUserHasnotAlreadyLeftReview()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var controller = new Web.Controllers.ReviewsController(reviewsServiceMock.Object,
                placeServiceMock.Object, dateTimeProviderMock.Object);

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var model = new SingleReviewViewModel()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var review = new Review()
            {
                PlaceId = placeId,
                UserId = userId
            };
            var emptyList = new List<Review>();

            var currentTime = DateTime.Now;
            dateTimeProviderMock.Setup(dt => dt.GetCurrentTime()).Returns(currentTime);

            reviewsServiceMock.Setup(s => s.GetByUserAndPlace(model.PlaceId, model.UserId))
                .Returns(emptyList.AsQueryable());

            reviewsServiceMock.Setup(s => s.AddReview(model.PlaceId, model.UserId, currentTime, model.Message, model.Rating))
                .Returns(review);
            model.PostedOn = currentTime;
            controller.WithCallTo(c => c.LeaveReview(model))
                .ShouldRenderPartialView("_Review")
                .WithModel(model);
        }
    }
}
