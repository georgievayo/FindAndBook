using System;
using System.Web.Mvc;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;

namespace FindAndBook.Web.Controllers
{
    public class ConsumablesController : Controller
    {
        private readonly IConsumableService consumableService;
        private readonly IPlaceService placeService;
        private readonly IViewModelFactory factory;

        public ConsumablesController(IConsumableService consumableService, IPlaceService placeService, IViewModelFactory factory)
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
        public ActionResult Create(Guid? id)
        {
            var model = this.factory.CreateCreateMenuViewModel(id);
            return View("CreateMenu", model);
        }
    }
}