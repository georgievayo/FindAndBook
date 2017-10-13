using System;
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
    public class PostEditShould
    {
        [Test]
        public void GetEditShould_ReturnSameView_WhenModelIsNotValid()
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
            controller.ModelState.AddModelError("key", "message");

            var model = new EditViewModel();
            controller
                .WithCallTo(c => c.Edit(model))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void GetEditShould_CallPlaceServiceMethodEditPlace_WhenModelIsValid()
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

            var id = Guid.NewGuid();
            var model = new EditViewModel() {Id = id};
            var place = new Place() {Id = id};
            placeServiceMock.Setup(s => s.EditPlace(model.Id, model.Contact, model.Description, model.PhotoUrl,
                    model.WeekdayHours, model.WeekendHours, model.AverageBill))
                .Returns(place);

            controller.Edit(model);

            placeServiceMock.Verify(s => s.EditPlace(model.Id, model.Contact, model.Description, model.PhotoUrl,
                model.WeekdayHours, model.WeekendHours, model.AverageBill));
        }

        [Test]
        public void GetEditShould_CallAddressServiceMethodEditAddress_WhenModelIsValid()
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

            var id = Guid.NewGuid();
            var model = new EditViewModel() { Id = id };
            var place = new Place() { Id = id, AddressId = Guid.NewGuid() };
            placeServiceMock.Setup(s => s.EditPlace(model.Id, model.Contact, model.Description, model.PhotoUrl,
                    model.WeekdayHours, model.WeekendHours, model.AverageBill))
                .Returns(place);

            controller.Edit(model);

            addressServiceMock.Verify(s => s.EditAddress(place.AddressId, model.Country, model.City, model.Area, model.Street, model.Number));
        }

        [Test]
        public void GetEditShould_RedirectToDetails_WhenModelIsValid()
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

            var id = Guid.NewGuid();
            var model = new EditViewModel() { Id = id };
            var place = new Place() { Id = id, AddressId = Guid.NewGuid() };
            placeServiceMock.Setup(s => s.EditPlace(model.Id, model.Contact, model.Description, model.PhotoUrl,
                    model.WeekdayHours, model.WeekendHours, model.AverageBill))
                .Returns(place);

            controller
                .WithCallTo(c => c.Edit(model))
                .ShouldRedirectTo((Web.Controllers.PlacesController pc) => pc.Details(place.Id));
        }
    }
}
