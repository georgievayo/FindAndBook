using System;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.AdministrationController
{
    [TestFixture]
    public class DeleteReviewShould
    {
        [Test]
        public void DeleteReviewShould_CallReviewsServiceMethodDeleteReview()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, factoryMock.Object);

            var id = Guid.NewGuid();
            controller.DeleteReview(id);

            reviewsServiceMock.Verify(s => s.DeleteReview(id), Times.Once);
        }

        [Test]
        public void DeleteReviewShould_ReturnEmptyResult()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, factoryMock.Object);

            var id = Guid.NewGuid();
            controller
                .WithCallTo(c => c.DeleteReview(id))
                .ShouldReturnEmptyResult();
        }
    }
}
