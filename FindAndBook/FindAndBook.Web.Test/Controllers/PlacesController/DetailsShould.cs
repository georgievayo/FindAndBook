using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;
using FindAndBook.Web.Models.Reviews;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.PlacesController
{
    [TestFixture]
    public class DetailsShould
    {
        [Test]
        public void DetailsShould_ReturnErrorView_WhenIdIsNull()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);

            controller
                .WithCallTo(c => c.Details(null))
                .ShouldRenderView("Error");
        }

        [Test]
        public void DetailsShould_CallPlaceServiceMethodGetPlaceWithReviews_WhenIdIsNotNull()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);
            InitializeMapper();
            var placeId = Guid.NewGuid();
            controller.Details(placeId);

            placeServiceMock.Verify(s => s.GetPlaceWithReviews(placeId), Times.Once);
        }

        [Test]
        public void DetailsShould_ReturnErrorView_WhenPlaceWasNotFound()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);
            InitializeMapper();

            var placeId = Guid.NewGuid();
            var emptyList = new List<Place>();
            placeServiceMock.Setup(s => s.GetPlaceWithReviews(placeId)).Returns(emptyList.AsQueryable());

            controller
                .WithCallTo(c => c.Details(placeId))
                .ShouldRenderView("Error");
        }

        [Test]
        public void DetailsShould_CallAuthProviderPropertyCurrentUserId_WhenPlaceWasFound()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);
            InitializeMapper();

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var username = "user";
            var place = new Place() {Id = placeId};
            var list = new List<Place>() {place};
            placeServiceMock.Setup(s => s.GetPlaceWithReviews(placeId)).Returns(list.AsQueryable());
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            authProviderMock.Setup(ap => ap.CurrentUserUsername).Returns(username);
            var emptyListReviews = new List<Review>();
            reviewsServiceMock.Setup(s => s.GetByUserAndPlace(placeId, userId)).Returns(emptyListReviews.AsQueryable());

            controller.Details(placeId);

            authProviderMock.Verify(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void DetailsShould_CallAuthProviderPropertyCurrentUserUsername_WhenPlaceWasFound()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);
            InitializeMapper();

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var username = "user";
            var place = new Place() { Id = placeId };
            var list = new List<Place>() { place };
            placeServiceMock.Setup(s => s.GetPlaceWithReviews(placeId)).Returns(list.AsQueryable());
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            authProviderMock.Setup(ap => ap.CurrentUserUsername).Returns(username);
            var emptyListReviews = new List<Review>();
            reviewsServiceMock.Setup(s => s.GetByUserAndPlace(placeId, userId)).Returns(emptyListReviews.AsQueryable());

            controller.Details(placeId);

            authProviderMock.Verify(ap => ap.CurrentUserUsername, Times.Once);
        }

        [Test]
        public void DetailsShould_CallFactoryMethodCreateReviewViewModel_WhenPlaceWasFound()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);
            InitializeMapper();

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var username = "user";
            var place = new Place() { Id = placeId };
            var list = new List<Place>() { place };
            placeServiceMock.Setup(s => s.GetPlaceWithReviews(placeId)).Returns(list.AsQueryable());
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            authProviderMock.Setup(ap => ap.CurrentUserUsername).Returns(username);
            var emptyListReviews = new List<Review>();
            reviewsServiceMock.Setup(s => s.GetByUserAndPlace(placeId, userId)).Returns(emptyListReviews.AsQueryable());
            var reviewForm = new SingleReviewViewModel();
            factoryMock.Setup(f => f.CreateReviewViewModel(placeId, userId, username)).Returns(reviewForm);
            controller.Details(placeId);

            factoryMock.Verify(f => f.CreateReviewViewModel(placeId, userId, username), Times.Once);
        }

        [Test]
        public void DetailsShould_ReturnView_WhenPlaceWasFound()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);
            InitializeMapper();

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            var username = "user";
            var place = new Place() { Id = placeId };
            var list = new List<Place>() { place };
            placeServiceMock.Setup(s => s.GetPlaceWithReviews(placeId)).Returns(list.AsQueryable());
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            authProviderMock.Setup(ap => ap.CurrentUserUsername).Returns(username);
            var emptyListReviews = new List<Review>();
            reviewsServiceMock.Setup(s => s.GetByUserAndPlace(placeId, userId)).Returns(emptyListReviews.AsQueryable());
            var reviewForm = new SingleReviewViewModel();
            factoryMock.Setup(f => f.CreateReviewViewModel(placeId, userId, username)).Returns(reviewForm);

            controller
                .WithCallTo(c => c.Details(placeId))
                .ShouldRenderDefaultView();
        }

        private void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
                cfg.CreateMap<Place, DetailsViewModel>()
                    .ForMember(viewModel => viewModel.Id,
                        opt => opt.MapFrom(place => place.Id))
            );
        }
    }
}
