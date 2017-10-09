using System;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IRepository<Review> reviewsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IReviewsFactory factory;

        public ReviewsService(IRepository<Review> reviewsRepository, IUnitOfWork unitOfWork, IReviewsFactory factory)
        {
            if (reviewsRepository == null)
            {
                throw new ArgumentNullException(nameof(reviewsRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.reviewsRepository = reviewsRepository;
            this.unitOfWork = unitOfWork;
            this.factory = factory;
        }

        public Review AddReview(Guid? placeId, string userId, DateTime postedOn, string message, int rating)
        {
            var review = this.factory.CreateReview(placeId, userId, postedOn, message, rating);
            this.reviewsRepository.Add(review);
            this.unitOfWork.Commit();

            return review;
        }

        public IQueryable<Review> GetAll()
        {
            return this.reviewsRepository.All;
        }

        public void DeleteAll(Guid? placeId)
        {
            var reviews = this.reviewsRepository.All.Where(x => x.PlaceId == placeId).ToList();

            foreach (var review in reviews)
            {
                this.reviewsRepository.Delete(review);
                this.unitOfWork.Commit();
            }
        }

        public void DeleteReview(Guid? id)
        {
            var review = this.reviewsRepository.GetById(id);
            this.reviewsRepository.Delete(review);
            this.unitOfWork.Commit();
        }

        public IQueryable<Review> GetByUserAndPlace(Guid? placeId, string userId)
        {
            return this.reviewsRepository
                .All
                .Where(x => x.PlaceId == placeId && x.UserId == userId);
        }
    }
}
