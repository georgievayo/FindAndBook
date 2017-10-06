using System;
using FindAndBook.Models;
using System.Linq;

namespace FindAndBook.Services.Contracts
{
    public interface IPlaceService
    {
        Place CreatePlace(string name, string type, string contact,
            string weekendHours, string weekdaayHours, string details, int? averageBill, string userId, Address address);

        IQueryable<Place> GetAll();

        IQueryable<Place> GetPlaceById(Guid id);

        IQueryable<Place> GetPlaceWithReviews(Guid id);

        IQueryable<Place> GetUserPlaces(string userId);

        IQueryable<Place> GetPlacesByCategory(string category);

        Place EditPlace(Guid? id, string contact, string description, string photoUrl, 
            string weekdayHours, string weekendHours, int? averageBill);

        IQueryable<Place> FindInName(string category, string pattern);

        IQueryable<Place> FindInDescription(string category, string pattern);

        IQueryable<Place> FindInAddress(string category, string pattern);

        IQueryable<Place> FindInBill(string category, string pattern);
    }
}
