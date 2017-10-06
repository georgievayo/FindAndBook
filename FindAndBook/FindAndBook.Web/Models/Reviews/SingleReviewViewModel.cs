using System;

namespace FindAndBook.Web.Models.Reviews
{
    public class SingleReviewViewModel
    {
        public SingleReviewViewModel()
        {
            
        }

        public SingleReviewViewModel(Guid? placeId, string userId)
        {
            PlaceId = placeId;
            UserId = userId;
        }

        public Guid? PlaceId { get; set; }

        public string UserId { get; set; }

        public string Message { get; set; }

        public DateTime PostedOn { get; set; }

        public int Rating { get; set; }
    }
}