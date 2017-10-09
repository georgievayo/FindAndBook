using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;

namespace FindAndBook.Web.Areas.Administration.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IPlaceService placeService;
        private readonly IReviewsService reviewService;
        private readonly IBookingService bookingService;
        private readonly IUserService userService;
        private readonly IViewModelFactory factory;

        public AdministrationController(IPlaceService placeService, IReviewsService reviewService, 
            IUserService userService, IBookingService bookingService, IViewModelFactory factory)
        {
            if (placeService == null)
            {
                throw new ArgumentNullException(nameof(placeService));
            }

            if (reviewService == null)
            {
                throw new ArgumentNullException(nameof(reviewService));
            }

            if (userService == null)
            {
                throw new ArgumentNullException(nameof(userService));
            }

            if (bookingService == null)
            {
                throw new ArgumentNullException(nameof(bookingService));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.placeService = placeService;
            this.reviewService = reviewService;
            this.userService = userService;
            this.bookingService = bookingService;
            this.factory = factory;
        }

        public ActionResult Index()
        {
            var places = this.placeService
                .GetAll()
                .ToList();

            var reviews = this.reviewService
                .GetAll()
                .ToList();

            var users = this.userService
                .GetAll()
                .ToList();

            var model = this.factory.CreateAllInformationViewModel(users, reviews, places);

            return View(model);
        }

        public ActionResult DeletePlace(Guid? id)
        {
            this.placeService.DeletePlace(id);
            return Json("Success");
        }

        public ActionResult DeleteUser(string id)
        {
            this.userService.DeleteUser(id);
            return Json("Success");
        }

        public ActionResult DeleteReview(Guid? id)
        {
            this.reviewService.DeleteReview(id);
            return Json("Success");
        }
    }
}