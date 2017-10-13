using System;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using Moq;
using NUnit.Framework;

namespace FindAndBook.Web.Test.Controllers.ConsumablesController
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ConstructorShould_SetPassedData_WhenDataIsNotNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, facctoryMock.Object);

            Assert.IsNotNull(controller);
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenPlaceServiceIsNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                null, facctoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenConsumableServiceIsNull()
        {
            var placeServiceMock = new Mock<IPlaceService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.ConsumablesController(null,
                placeServiceMock.Object, facctoryMock.Object));
        }

        [Test]
        public void ConstructorShould_ThrowException_WhenFactoryIsNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();

            Assert.Throws<ArgumentNullException>(() => new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
               placeServiceMock.Object, null));
        }
    }
}
