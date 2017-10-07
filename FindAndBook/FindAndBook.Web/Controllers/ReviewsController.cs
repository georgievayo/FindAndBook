using System;
using System.Web.Mvc;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Models.Reviews;

namespace FindAndBook.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewsService;
        private readonly IPlaceService placeService;

        public ReviewsController(IReviewsService reviewsService, IPlaceService placeService)
        {
            if (reviewsService == null)
            {
                throw new ArgumentNullException(nameof(reviewsService));
            }

            if (placeService == null)
            {
                throw new ArgumentNullException(nameof(placeService));
            }

            this.reviewsService = reviewsService;
            this.placeService = placeService;
        }

        [Authorize]
        [HttpPost]
        public ActionResult LeaveReview(SingleReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ReviewForm", model);
            }

            var addedReview = this.reviewsService
                .AddReview(model.PlaceId, model.UserId, DateTime.Now, model.Message, model.Rating);

            return RedirectToAction("Details", new {controller = "Places", id = model.PlaceId});
        }
    }
}