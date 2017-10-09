using System;
using System.Linq;
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

            var reviewsOfUser = this.reviewsService.GetByUserAndPlace(model.PlaceId, model.UserId).ToList();
            if (reviewsOfUser.Count > 0)
            {
                return Json("You have already left a review for this place!");
            }

            var addedReview = this.reviewsService
                .AddReview(model.PlaceId, model.UserId, DateTime.Now, model.Message, model.Rating);
            

            //return RedirectToAction("Details", new {controller = "Places", id = model.PlaceId});
            return PartialView("_Review", model);
        }
    }
}