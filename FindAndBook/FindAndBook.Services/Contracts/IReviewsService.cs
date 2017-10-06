using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IReviewsService
    {
        IQueryable<Review> GetAllByPlace(Guid? placeId);

        Review AddReview(Guid? placeId, string userId, DateTime postedOn, string message, int rating);
    }
}
