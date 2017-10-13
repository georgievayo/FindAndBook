using System;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.PlacesController
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ContructorShould_SetPassedDataCorrectly_WhenDataIsNotNull()
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

            Assert.IsNotNull(controller);
        }

        [Test]
        public void ContructorShould_ThrowException_WhenAuthProviderIsNull()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.PlacesController(null, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object));
        }

        [Test]
        public void ContructorShould_ThrowException_WhenPlaceServiceIsNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                null, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object));
        }

        [Test]
        public void ContructorShould_ThrowException_WhenFactoryIsNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var addressServiceMock = new Mock<IAddressService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.PlacesController(authProviderMock.Object, null,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object,
                reviewsServiceMock.Object));
        }

        [Test]
        public void ContructorShould_ThrowException_WhenAddressServiceIsNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var placeServiceMock = new Mock<IPlaceService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, null, tablesServiceMock.Object,
                reviewsServiceMock.Object));
        }

        [Test]
        public void ContructorShould_ThrowException_WhenTablesServiceIsNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var addressServiceMock = new Mock<IAddressService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, null, reviewsServiceMock.Object));
        }

        [Test]
        public void ContructorShould_ThrowException_WhenReviewsServiceIsNull()
        {
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var addressServiceMock = new Mock<IAddressService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var tablesServiceMock = new Mock<ITablesService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.PlacesController(authProviderMock.Object, factoryMock.Object,
                placeServiceMock.Object, addressServiceMock.Object, tablesServiceMock.Object, null));
        }
    }
}
