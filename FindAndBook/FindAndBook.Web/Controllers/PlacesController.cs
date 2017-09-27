using System;
using System.Linq;
using System.Web.Mvc;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;

namespace FindAndBook.Web.Controllers
{
    public class PlacesController : Controller
    {
        private IViewModelFactory viewModelFactory;
        private IAuthenticationProvider authProvider;
        private IPlaceService placeService;
        private IAddressService addressService;

        public PlacesController(IAuthenticationProvider authProvider, IViewModelFactory factory,
            IPlaceService placeService, IAddressService addressService)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (authProvider == null)
            {
                throw new ArgumentNullException(nameof(authProvider));
            }

            if (placeService == null)
            {
                throw new ArgumentNullException(nameof(placeService));
            }

            if (addressService == null)
            {
                throw new ArgumentNullException(nameof(addressService));
            }

            this.viewModelFactory = factory;
            this.authProvider = authProvider;
            this.placeService = placeService;
            this.addressService = addressService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            if (!this.authProvider.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var userId = this.authProvider.CurrentUserId;
            var isManager = this.authProvider.IsInRole(userId, "Manager");
            var model = this.viewModelFactory.CreateCreateViewModel();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.authProvider.CurrentUserId;
            var place = this.placeService.CreatePlace(model.Name, model.Contact, model.WeekendHours, model.WeekdayHours,
                model.Description, model.AverageBill, userId);
            var address = this.addressService.CreateAddress(place.Id, model.Country, model.City, model.Area, model.Street,
                model.Number);

            return this.RedirectToAction("Details", new { id = place.Id });
        }

        [HttpGet]
        public ActionResult List()
        {
            var places = this.placeService.GetAll()
                .Select(p =>
                {
                    var address = this.addressService.GetAddressByPlaceId(p.Id);
                    return this.viewModelFactory.CreatePlaceShort(p, address);
                })
                .ToList();

            return View(places);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var place = this.placeService.GetPlaceById(id);
            var address = this.addressService.GetAddressByPlaceId(place.Id);
            var model = this.viewModelFactory.CreatePlaceDetailsViewModel(place, address);

            return this.View(model);
        }
    }
}