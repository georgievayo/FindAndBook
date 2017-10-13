using System.Collections.Generic;
using System.Linq;
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
    public class GetPlacesByCategoryShould
    {
        private void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
                    cfg.CreateMap<Place, PlaceShortViewModel>()
                        .ForMember(viewModel => viewModel.Name,
                            opt => opt.MapFrom(place => place.Name))
            );
        }
        public void GetPlacesByCategoryShould_ReturnErrorView_WhenPassedCategoryIsNull()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            InitializeMapper();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);

            controller
                .WithCallTo(c => c.GetPlacesByCategory(null, 10, 1))
                .ShouldRenderView("Error");
        }

        [TestCase("Restaurant")]
        [TestCase("Club")]
        [TestCase("Cafe")]
        public void GetPlacesByCategoryShould_CallPlaceServiceMethodGetPlacesByCategory_WhenPassedCategoryIsNotNull(string category)
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            InitializeMapper();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);

            controller.GetPlacesByCategory(category);

            placeServiceMock.Verify(s => s.GetPlacesByCategory(category), Times.Once);
        }

        [TestCase("Restaurant")]
        [TestCase("Club")]
        [TestCase("Cafe")]
        public void GetPlacesByCategoryShould_ReturnViewWithCorrectModel_WhenPassedCategoryIsNotNull(string category)
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            InitializeMapper();

            var controller = new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object);
            var place = new Place()
            {
                Type = category
            };
            var list = new List<Place>() {place};
            placeServiceMock.Setup(s => s.GetPlacesByCategory(category)).Returns(list.AsQueryable());
            var model =
            controller
                .WithCallTo(c => c.GetPlacesByCategory(category, 10, 1))
                .ShouldRenderPartialView("_PartialList");
        }
    }
}
