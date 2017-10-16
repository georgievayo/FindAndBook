using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;
using Microsoft.AspNet.Identity;
using PagedList;

namespace FindAndBook.Web.Controllers
{
    public class PlacesController : Controller
    {
        private readonly IViewModelFactory viewModelFactory;
        private readonly IAuthenticationProvider authProvider;
        private readonly IPlaceService placeService;
        private readonly IAddressService addressService;
        private readonly ITablesService tablesService;
        private readonly IReviewsService reviewsService;


        public PlacesController(IAuthenticationProvider authProvider, IViewModelFactory factory,
            IPlaceService placeService, IAddressService addressService, ITablesService tablesService,
            IReviewsService reviewsService)
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

            if (reviewsService == null)
            {
                throw new ArgumentNullException(nameof(reviewsService));
            }

            this.viewModelFactory = factory;
            this.authProvider = authProvider;
            this.placeService = placeService;
            this.addressService = addressService;
            this.tablesService = tablesService;
            this.reviewsService = reviewsService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 60*5, VaryByParam = "category")]
        public ActionResult GetPlacesByCategory([Bind(Prefix = "category")] string category, int count = 6, int page = 1)
        {
            if (category == null)
            {
                return View("Error");
            }

            var places = this.placeService
                .GetPlacesByCategory(category)
                .ProjectTo<PlaceShortViewModel>()
                .ToList();

            var model = places.ToPagedList(page, count);
            ViewBag.Category = category;

            return this.PartialView("_PartialList", model);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            var model = this.viewModelFactory.CreateCreateViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var userId = this.authProvider.CurrentUserId;

            // Create address
            var address = this.addressService.CreateAddress(model.Country, model.City, model.Area, model.Street,
                model.Number);

            //Create place
            var place = this.placeService.CreatePlace(model.Name, model.Types, model.Contact, model.WeekendHours, model.WeekdayHours,
                model.Description, model.AverageBill, userId, address.Id);

            // Create all types of tables
            var tablesWithTwoPeople = this.tablesService.CreateTableType(place.Id, 2, model.TwoPeopleCount);
            var tablesWithFourPeople = this.tablesService.CreateTableType(place.Id, 4, model.FourPeopleCount);
            var tablesWithSixPeople = this.tablesService.CreateTableType(place.Id, 6, model.SixPeopleCount);

            return this.RedirectToAction("Details", new { id = place.Id });
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var currentUserId = this.authProvider.CurrentUserId;
            var found = this.placeService.GetPlaceById(id);
            var place = found.FirstOrDefault();
            if (place == null || currentUserId != place.ManagerId)
            {
                return View("Error");
            }

            var model = found.ProjectTo<EditViewModel>()
                            .FirstOrDefault();

            return View("Edit", model);
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updatedPlace = this.placeService.EditPlace(model.Id, model.Contact, model.Description, model.PhotoUrl, model.WeekdayHours,
                model.WeekendHours, model.AverageBill);

            var updatedAddress = this.addressService.EditAddress(updatedPlace.AddressId, model.Country, model.City, model.Area,
                model.Street, model.Number);

            return this.RedirectToAction("Details", "Places", new { id = updatedPlace.Id });
        }

        [HttpGet]
        public ActionResult List(int count = 6, int page = 1)
        {
            var places = this.placeService
                .GetAll()
                .Include(p => p.Reviews)
                .Include(p => p.Address)
                .ProjectTo<PlaceShortViewModel>()
                .ToList();

            var model = places.ToPagedList(page, count);

            return View(model);
        }

        [HttpGet]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var model = this.placeService
                .GetPlaceWithReviews(id)
                .ProjectTo<DetailsViewModel>()
                .FirstOrDefault();

            if (model == null)
            {
                return View("Error");
            }

            var currentUserId = this.authProvider.CurrentUserId;
            var currentUserUsername = this.authProvider.CurrentUserUsername;
            var canVote = this.reviewsService
                .GetByUserAndPlace(id, currentUserId)
                .ToList()
                .Count <= 0;

            var reviewForm = this.viewModelFactory.CreateReviewViewModel(model.Id, currentUserId, currentUserUsername);
            model.ReviewForm = reviewForm;
            model.CanLeaveReview = canVote;

            return this.View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public ActionResult MyPlaces()
        {
            var currentUser = this.authProvider.CurrentUserId;
            var model = this.placeService.GetUserPlaces(currentUser).ToList();

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}