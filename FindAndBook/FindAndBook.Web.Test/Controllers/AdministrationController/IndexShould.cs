using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.AdministrationController
{
    [TestFixture]
    public class IndexShould
    {
        [Test]
        public void IndexShould_CallPlaceServiceMethodGetAll()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var userServiceMock = new Mock<IUserService>();
            var reviewsServiceMock = new Mock<IReviewsService>();
            var authProviderMock = new Mock<IAuthenticationProvider>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Areas.Administration.Controllers.AdministrationController(placeServiceMock.Object,
                reviewsServiceMock.Object, authProviderMock.Object, userServiceMock.Object, factoryMock.Object);
        }
    }
}
