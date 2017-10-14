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
    public class DeletePlaceShould
    {
        [Test]
        public void DeletePlaceShould_CallPlaceServiceMethodDeletePlace()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var questionServiceMock = new Mock<IQuestionService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, 
                questionServiceMock.Object, factoryMock.Object);

            var id = Guid.NewGuid();
            controller.DeletePlace(id);

            placeServiceMock.Verify(s => s.DeletePlace(id), Times.Once);
        }

        [Test]
        public void DeletePlaceShould_ReturnEmptyResult()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var questionServiceMock = new Mock<IQuestionService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object,
                questionServiceMock.Object, factoryMock.Object);

            var id = Guid.NewGuid();
            controller
                .WithCallTo(c => c.DeletePlace(id))
                .ShouldReturnEmptyResult();
        }
    }
}
