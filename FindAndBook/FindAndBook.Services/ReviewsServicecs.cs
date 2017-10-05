using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Data.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    class ReviewsServicecs : IReviewsService
    {
        private readonly IRepository<Review> reviewsRepository;
        private readonly IUnitOfWork unitOfWork;

        public ReviewsServicecs(IRepository<Review> reviewsRepository, IUnitOfWork unitOfWork)
        {
            if (reviewsRepository == null)
            {
                throw new ArgumentNullException(nameof(reviewsRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            this.reviewsRepository = reviewsRepository;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Review> GetAllByPlace(Guid? placeId)
        {
            throw new NotImplementedException();
        }
    }
}
