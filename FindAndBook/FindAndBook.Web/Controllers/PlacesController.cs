using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;

namespace FindAndBook.Web.Controllers
{
    public class PlacesController : Controller
    {
        private readonly IViewModelFactory viewModelFactory;
        private readonly IAuthenticationProvider authProvider;
        private readonly IPlaceService placeService;
        private readonly IAddressService addressService;
        private readonly ITablesService tablesService;

        public PlacesController(IAuthenticationProvider authProvider, IViewModelFactory factory,
            IPlaceService placeService, IAddressService addressService, ITablesService tablesService)
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

            if (tablesService == null)
            {
                throw new ArgumentNullException(nameof(tablesService));
            }

            this.viewModelFactory = factory;
            this.authProvider = authProvider;
            this.placeService = placeService;
            this.addressService = addressService;
            this.tablesService = tablesService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPlacesByCategory([Bind(Prefix = "category")] string category)
        {
            var places = this.placeService
                .GetPlacesByCategory(category)
                .ProjectTo<PlaceShortViewModel>()
                .ToList();

            return this.PartialView("_PartialList", places);
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
            var address = this.addressService.CreateAddress(model.Country, model.City, model.Area, model.Street,
                model.Number);
            var place = this.placeService.CreatePlace(model.Name, model.Types, model.Contact, model.WeekendHours, model.WeekdayHours,
                model.Description, model.AverageBill, userId, address);
            var tablesWithTwoPeople = this.tablesService.CreateTableType(place.Id, 2, model.TwoPeopleCount);
            var tablesWithFourPeople = this.tablesService.CreateTableType(place.Id, 4, model.FourPeopleCount);
            var tablesWithSixPeople = this.tablesService.CreateTableType(place.Id, 6, model.SixPeopleCount);

            return this.RedirectToAction("Details", new { id = place.Id });
        }

        [HttpGet]
        public ActionResult List()
        {
            var places = this.placeService
                .GetAll()
                .Include(p => p.Reviews)
                .Include(p => p.Address)
                .ProjectTo<PlaceShortViewModel>()
                .ToList();
           
            return View(places);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var model = this.placeService
                .GetPlaceWithReviews(id)
                .ProjectTo<DetailsViewModel>()
                .FirstOrDefault();

            return this.View(model);
        }

        [HttpGet]
        public ActionResult MyPlaces(string id)
        {
            var currentUser = this.authProvider.CurrentUserId;
            var model = this.placeService.GetUserPlaces(currentUser).ToList();
            return View(model);
        }


    }
}