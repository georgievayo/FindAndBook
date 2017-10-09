﻿using System;
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

        public ActionResult GetPlacesByCategory([Bind(Prefix = "category")] string category, int count = 10, int page = 1)
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

            return this.PartialView("_PartialList", model);
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
            if (!isManager)
            {
                return View("Error");
            }

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

            // Create address
            var address = this.addressService.CreateAddress(model.Country, model.City, model.Area, model.Street,
                model.Number);

            //Create place
            var place = this.placeService.CreatePlace(model.Name, model.Types, model.Contact, model.WeekendHours, model.WeekdayHours,
                model.Description, model.AverageBill, userId, address);

            // Create all types of tables
            var tablesWithTwoPeople = this.tablesService.CreateTableType(place.Id, 2, model.TwoPeopleCount);
            var tablesWithFourPeople = this.tablesService.CreateTableType(place.Id, 4, model.FourPeopleCount);
            var tablesWithSixPeople = this.tablesService.CreateTableType(place.Id, 6, model.SixPeopleCount);

            return this.RedirectToAction("Details", new { id = place.Id });
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            if (!this.authProvider.IsAuthenticated)
            {
                return this.RedirectToAction("Login", "Account");
            }

            var currentUserId = this.authProvider.CurrentUserId;
            var place = this.placeService.GetPlaceById(id).FirstOrDefault();
            if (place == null || (place != null && currentUserId != place.ManagerId))
            {
                return View("Error");
            }

            var model = this.placeService.GetPlaceById(id)
                .ProjectTo<EditViewModel>()
                .FirstOrDefault();

            return View("Edit", model);

        }

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

            return this.RedirectToAction("Details", "Places", new {id = updatedPlace.Id});
        }

        [HttpGet]
        public ActionResult List(int count = 10, int page = 1)
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
        public ActionResult MyPlaces()
        {
            var currentUser = this.authProvider.CurrentUserId;
            if (this.authProvider.IsInRole(currentUser, "Manager"))
            {
                var model = this.placeService.GetUserPlaces(currentUser).ToList();
                return View(model);
            }

            return View("Error");
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