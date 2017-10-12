using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Consumables;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace FindAndBook.Web.Test.Controllers.ConsumablesController
{
    [TestFixture]
    public class AddConsumableShould
    {
        [Test]
        public void AddConsumableShould_ReturnSameView_WhenModelIsNotValid()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, facctoryMock.Object);
            var model = new ConsumableViewModel();
            controller.ModelState.AddModelError("key", "message");

            controller
                .WithCallTo(c => c.AddConsumable(model))
                .ShouldRenderPartialView("_ConsumableForm")
                .WithModel(model);
        }

        [Test]
        public void AddConsumableShould_CallConsumableServiceMethodAddConsumable_WhenModelIsValid()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, facctoryMock.Object);
            var model = new ConsumableViewModel();

            controller.AddConsumable(model);
            consumableServiceMock.Verify(s => s.AddConsumable(model.PlaceId, model.Name, model.Quantity, model.Price,
                model.Type, model.Ingredients));
        }

        [Test]
        public void AddConsumableShould_ReturnViewWithCorrectModel_WhenModelIssValid()
        {
            var consumableServiceMock = new Mock<IConsumableService>();
            var placeServiceMock = new Mock<IPlaceService>();
            var facctoryMock = new Mock<IViewModelFactory>();

            var controller = new Web.Controllers.ConsumablesController(consumableServiceMock.Object,
                placeServiceMock.Object, facctoryMock.Object);
            var model = new ConsumableViewModel();

            controller
                .WithCallTo(c => c.AddConsumable(model))
                .ShouldRenderPartialView("_Consumable")
                .WithModel(model);
        }
    }
}
