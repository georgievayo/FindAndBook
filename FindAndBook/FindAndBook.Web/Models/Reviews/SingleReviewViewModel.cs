using System;

namespace FindAndBook.Web.Models.Reviews
{
    public class SingleReviewViewModel
    {
        public SingleReviewViewModel()
        {
            
        }

        public SingleReviewViewModel(Guid? placeId, string userId, string username)
        {
            PlaceId = placeId;
            UserId = userId;
            Username = username;
        }

        public Guid? PlaceId { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public string Message { get; set; }

        public DateTime PostedOn { get; set; }

        public int Rating { get; set; }
    }
}