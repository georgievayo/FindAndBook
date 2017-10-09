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
        Review AddReview(Guid? placeId, string userId, DateTime postedOn, string message, int rating);

        IQueryable<Review> GetAll();

        void DeleteAll(Guid? placeId);

        void DeleteReview(Guid? id);

        IQueryable<Review> GetByUserAndPlace(Guid? placeId, string userId);
    }
}
