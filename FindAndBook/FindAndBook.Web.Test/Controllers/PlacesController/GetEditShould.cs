using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.PlacesController
{
    [TestFixture]
    public class GetEditShould
    {
        [Test]
        public void GetEditShould_ReturnErrorView_WhenPassedIdIsNull()
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
                .WithCallTo(c => c.Edit((Guid?) null))
                .ShouldRenderView("Error");
        }

        [Test]
        public void GetEditShould_CallAuthProviderPropertyCurrentUserId_WhenPassedIdIsNotNull()
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

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);

            controller.Edit(placeId);

            authProviderMock.Verify(ap => ap.CurrentUserId, Times.Once);
        }

        [Test]
        public void GetEditShould_CallAuthProviderPlaceServiceMethodGetPlaceById_WhenPassedIdIsNotNull()
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

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);

            controller.Edit(placeId);

            placeServiceMock.Verify(s => s.GetPlaceById(placeId), Times.Once);
        }

        [Test]
        public void GetEditShould_ReturnErrorView_WhenPlaceWasNotFound()
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

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            var emptyList = new List<Place>();
            placeServiceMock.Setup(s => s.GetPlaceById(placeId)).Returns(emptyList.AsQueryable());

            controller
                .WithCallTo(c => c.Edit(placeId))
                .ShouldRenderView("Error");
        }

        [Test]
        public void GetEditShould_ReturnErrorView_WhenCurrentUserIsNotManagerOfFoundPlace()
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

            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            var place = new Place()
            {
                Id = placeId,
                ManagerId = Guid.NewGuid().ToString()
            };
            var list = new List<Place>() {place};
            placeServiceMock.Setup(s => s.GetPlaceById(placeId)).Returns(list.AsQueryable());

            controller
                .WithCallTo(c => c.Edit(placeId))
                .ShouldRenderView("Error");
        }

        [Test]
        public void GetEditShould_ReturnDefaultView_WhenCurrentUserIsManagerOfFoundPlace()
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
            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            var place = new Place()
            {
                Id = placeId,
                ManagerId = userId
            };
            var list = new List<Place>() { place };
            placeServiceMock.Setup(s => s.GetPlaceById(placeId)).Returns(list.AsQueryable());

            controller
                .WithCallTo(c => c.Edit(placeId))
                .ShouldRenderDefaultView();
        }

        private void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
                cfg.CreateMap<Place, EditViewModel>()
                    .ForMember(viewModel => viewModel.Id,
                        opt => opt.MapFrom(place => place.Id))
            );
        }
    }
}
