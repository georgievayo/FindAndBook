using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Consumables;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.ConsumablesController
{
    [TestFixture]
    public class GetCreateShould
    {
        [Test]
        public void GetCreateShould_ReturnErrorView_WhenPassedIdIsNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, facctoryMock.Object);

            controller
                .WithCallTo(c => c.Create(null))
                .ShouldRenderView("Error");
        }

        [Test]
        public void GetCreateShould_CallConsumableServiceMethodGetAllConsumablesOf_WhenPassedIdIsNotNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, facctoryMock.Object);

            var id = Guid.NewGuid();
            controller.Create(id);

            consumableServiceMock.Verify(s => s.GetAllConsumablesOf(id), Times.Once);
        }

        [Test]
        public void GetCreateShould_CallFactoryMethodCreateMenuViewModel_WhenPassedIdIsNotNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, facctoryMock.Object);

            var id = Guid.NewGuid();
            var emptyList = new List<Consumable>();

            consumableServiceMock.Setup(s => s.GetAllConsumablesOf(id)).Returns(emptyList.AsQueryable());

            controller.Create(id);

            facctoryMock.Verify(s => s.CreateMenuViewModel(id, emptyList), Times.Once);
        }

        [Test]
        public void GetCreateShould_ReturnViewWithCorrectModel_WhenPassedIdIsNotNull()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, facctoryMock.Object);

            var id = Guid.NewGuid();
            var emptyList = new List<Consumable>();
            var model = new CreateMenuViewModel();
            model.Menu = emptyList;
            model.PlaceId = id;

            consumableServiceMock.Setup(s => s.GetAllConsumablesOf(id)).Returns(emptyList.AsQueryable());
            facctoryMock.Setup(s => s.CreateMenuViewModel(id, emptyList)).Returns(model);

            controller
                .WithCallTo(c => c.Create(id))
                .ShouldRenderView("CreateMenu")
                .WithModel(model);
        }
    }
}
