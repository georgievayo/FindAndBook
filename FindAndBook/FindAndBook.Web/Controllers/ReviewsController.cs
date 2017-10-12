using System;
using System.Linq;
using System.Web.Mvc;
using FindAndBook.Providers.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Models.Reviews;

namespace FindAndBook.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewsService;
        private readonly IPlaceService placeService;
        private readonly IDateTimeProvider dateTimeProvider;

        public ReviewsController(IReviewsService reviewsService, IPlaceService placeService,
            IDateTimeProvider dateTimeProvider)
        {
            if (reviewsService == null)
            {
                throw new ArgumentNullException(nameof(reviewsService));
            }

            if (placeService == null)
            {
                throw new ArgumentNullException(nameof(placeService));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(nameof(dateTimeProvider));
            }

            this.reviewsService = reviewsService;
            this.placeService = placeService;
            this.dateTimeProvider = dateTimeProvider;
        }

        [Authorize]
        [HttpPost]
        public ActionResult LeaveReview(SingleReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ReviewForm", model);
            }

            var reviewsOfUser = this.reviewsService.GetByUserAndPlace(model.PlaceId, model.UserId).ToList();
            if (reviewsOfUser.Count > 0)
            {
                return Json("You have already left a review for this place!");
            }

            var currentTime = this.dateTimeProvider.GetCurrentTime();
            var addedReview = this.reviewsService
                .AddReview(model.PlaceId, model.UserId, currentTime, model.Message, model.Rating);
            model.PostedOn = addedReview.PostedOn;

            return PartialView("_Review", model);
        }
    }
}