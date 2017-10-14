using System;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.AdministrationController
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectly_WhenDataIsNotNull()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, factoryMock.Object);

            Assert.IsNotNull(controller);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPlaceServiceIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Areas.Administration.Controllers.AdministrationController(null,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenReviewsServiceIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                null, authProviderMock.Object, userServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenAuthProviderIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, null, userServiceMock.Object, factoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenFactoryIsNull()
        {
            var userServiceMock = new Mock<IUserService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();

            Assert.Throws<ArgumentNullException>(() => new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object,null));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenUserServiceIsNull()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, null, factoryMock.Object));
        }
    }
}
