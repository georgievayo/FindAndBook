using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class PostCreateShould
    {
        [Test]
        public void PostCreateShould_ReturnSameView_WhenModelIsNotValid()
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
            var model = new CreateViewModel();

            controller
                .WithCallTo(c => c.Create(model))
                .ShouldRenderDefaultView()
                .WithModel(model);
        }

        [Test]
        public void PostCreateShould_CallAddressServiceMethodCreateAddress_WhenModelIsValid()
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

            var model = new CreateViewModel();
            var userId = Guid.NewGuid().ToString();
            var address = new Address() {Id = Guid.NewGuid()};
            var place = new Place();

            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            addressServiceMock.Setup(s => s.CreateAddress(model.Country, model.City, model.Area, model.Street,
                    model.Number))
                .Returns(address);

            placeServiceMock.Setup(s => s.CreatePlace(model.Name, model.Types, model.Contact, model.WeekendHours,
                    model.WeekdayHours, model.Description, model.AverageBill, userId, address.Id))
                .Returns(place);

            controller.Create(model);

            addressServiceMock.Verify(
                s => s.CreateAddress(model.Country, model.City, model.Area, model.Street, model.Number), Times.Once);
        }

        [Test]
        public void PostCreateShould_CallPlaceServiceMethodCreatePlace_WhenModelIsValid()
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

            var model = new CreateViewModel();
            var userId = Guid.NewGuid().ToString();
            var address = new Address() { Id = Guid.NewGuid() };
            var place = new Place();

            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            addressServiceMock.Setup(s => s.CreateAddress(model.Country, model.City, model.Area, model.Street,
                    model.Number))
                .Returns(address);

            placeServiceMock.Setup(s => s.CreatePlace(model.Name, model.Types, model.Contact, model.WeekendHours,
                    model.WeekdayHours, model.Description, model.AverageBill, userId, address.Id))
                .Returns(place);

            controller.Create(model);

            placeServiceMock.Verify(
                s => s.CreatePlace(model.Name, model.Types, model.Contact, model.WeekendHours,
                    model.WeekdayHours, model.Description, model.AverageBill, userId, address.Id), Times.Once);
        }

        [Test]
        public void PostCreateShould_CallTableServiceMethodCreateTableTypeThreeTimes_WhenModelIsValid()
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

            var model = new CreateViewModel();
            var userId = Guid.NewGuid().ToString();
            var address = new Address() { Id = Guid.NewGuid() };
            var place = new Place() {Id = Guid.NewGuid()};

            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            addressServiceMock.Setup(s => s.CreateAddress(model.Country, model.City, model.Area, model.Street,
                    model.Number))
                .Returns(address);

            placeServiceMock.Setup(s => s.CreatePlace(model.Name, model.Types, model.Contact, model.WeekendHours,
                    model.WeekdayHours, model.Description, model.AverageBill, userId, address.Id))
                .Returns(place);

            controller.Create(model);

            tablesServiceMock.Verify(
                s => s.CreateTableType(place.Id, 2, model.TwoPeopleCount), Times.Once);
            tablesServiceMock.Verify(
                s => s.CreateTableType(place.Id, 4, model.FourPeopleCount), Times.Once);
            tablesServiceMock.Verify(
                s => s.CreateTableType(place.Id, 6, model.SixPeopleCount), Times.Once);
        }

        [Test]
        public void PostCreateShould_RedirectToDetails_WhenModelIsValid()
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

            var model = new CreateViewModel();
            var userId = Guid.NewGuid().ToString();
            var address = new Address() { Id = Guid.NewGuid() };
            var place = new Place() { Id = Guid.NewGuid() };

            authProviderMock.Setup(ap => ap.CurrentUserId).Returns(userId);
            addressServiceMock.Setup(s => s.CreateAddress(model.Country, model.City, model.Area, model.Street,
                    model.Number))
                .Returns(address);

            placeServiceMock.Setup(s => s.CreatePlace(model.Name, model.Types, model.Contact, model.WeekendHours,
                    model.WeekdayHours, model.Description, model.AverageBill, userId, address.Id))
                .Returns(place);

            controller
                .WithCallTo(c => c.Create(model))
                .ShouldRedirectTo((Web.Controllers.PlacesController c) => c.Details(place.Id));
        }
    }
}
