using System;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Consumables;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.ConsumablesController
{
    [TestFixture]
    public class GetNewFormShould
    {
        [Test]
        public void GetNewFormShould_ReturnErrorView_WhenPassedIdIsNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, factoryMock.Object);

            controller
                .WithCallTo(c => c.GetNewForm(null))
                .ShouldRenderView("Error");
        }

        [Test]
        public void GetNewFormShould_CallFactoryMethodCreateConsumableViewModel_WhenPassedIdIsNotNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();

            controller.GetNewForm(id);

            factoryMock.Verify(f => f.CreateConsumableViewModel(id), Times.Once);
        }

        [Test]
        public void GetNewFormShould_ReturnViewWithCorrectModel_WhenPassedIdIsNotNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var factoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, factoryMock.Object);
            var id = Guid.NewGuid();
            var model = new ConsumableViewModel()
            {
                PlaceId = id
            };

            factoryMock.Setup(f => f.CreateConsumableViewModel(id)).Returns(model);

            controller
                .WithCallTo(c => c.GetNewForm(id))
                .ShouldRenderPartialView("_ConsumableForm")
                .WithModel(model);
        }
    }
}
