using System;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IReviewsFactory
    {
        Review CreateReview(Guid? placeId, string userId, DateTime postedOn, string message, int rating);
    }
}
