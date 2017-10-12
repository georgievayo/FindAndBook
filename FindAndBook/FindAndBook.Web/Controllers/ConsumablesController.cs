using System;
using System.Linq;
using System.Web.Mvc;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Consumables;

namespace FindAndBook.Web.Controllers
{
    public class ConsumablesController : Controller
    {
        private readonly IConsumableService consumableService;
        private readonly IPlaceService placeService;
        private readonly IViewModelFactory factory;

        public ConsumablesController(IConsumableService consumableService, 
            IPlaceService placeService, IViewModelFactory factory)
        {
            if (consumableService == null)
            {
                throw new ArgumentNullException(nameof(consumableService));
            }

            if (placeService == null)
            {
                throw new ArgumentNullException(nameof(placeService));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.consumableService = consumableService;
            this.placeService = placeService;
            this.factory = factory;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Create(Guid? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var menu = this.consumableService.GetAllConsumablesOf(id).ToList();
            var model = this.factory.CreateMenuViewModel(id, menu);
            return View("CreateMenu", model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult AddConsumable(ConsumableViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ConsumableForm", model);
            }

            var consumable = this.consumableService.AddConsumable(model.PlaceId, model.Name, 
                model.Quantity, model.Price, model.Type, model.Ingredients);

            return PartialView("_Consumable", model);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult GetNewForm(Guid? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var model = this.factory.CreateConsumableViewModel(id);
            return PartialView("_ConsumableForm", model);
        }
    }
}