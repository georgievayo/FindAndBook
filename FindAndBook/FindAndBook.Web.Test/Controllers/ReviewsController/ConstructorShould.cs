using System;
using FindAndBook.Providers.Contracts;
using FindAndBook.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.ReviewsController
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedDataCorrectlly_WhenDataIsNotNull()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            var controller = new Web.Controllers.ReviewsController(reviewsServiceMock.Object,
                placeServiceMock.Object, dateTimeProviderMock.Object);

            Assert.IsNotNull(controller);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenReviewsServiceIsNull()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() => 
            new Web.Controllers.ReviewsController(null, placeServiceMock.Object, dateTimeProviderMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPlaceServiceIsNull()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            Assert.Throws<ArgumentNullException>(() =>
                new Web.Controllers.ReviewsController(reviewsServiceMock.Object, null, dateTimeProviderMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenDateTimeProviderIsNull()
        {
            var reviewsServiceMock = new Mock<IReviewsService>();
            var placeServiceMock = new Mock<IPlaceService>();

            Assert.Throws<ArgumentNullException>(() =>
                new Web.Controllers.ReviewsController(reviewsServiceMock.Object, placeServiceMock.Object, null));
        }
    }
}
