using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
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
        private readonly IMapper mapper;

        public PlacesController(IAuthenticationProvider authProvider, IViewModelFactory factory,
            IPlaceService placeService, IAddressService addressService, IMapper mapper)
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

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            this.viewModelFactory = factory;
            this.authProvider = authProvider;
            this.placeService = placeService;
            this.addressService = addressService;
            this.mapper = mapper;
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
            var address = this.addressService.CreateAddress(model.Country, model.City, model.Area, model.Street,
                model.Number);
            var place = this.placeService.CreatePlace(model.Name, model.Contact, model.WeekendHours, model.WeekdayHours,
                model.Description, model.AverageBill, userId, address);

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
                .GetPlaceById(id)
                .ProjectTo<DetailsViewModel>()
                .FirstOrDefault();

            return this.View(model);
        }
    }
}