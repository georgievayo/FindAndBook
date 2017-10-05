using System;
using System.Collections.Generic;
using FindAndBook.Models;
using FindAndBook.Web.Models.Bookings;
using FindAndBook.Web.Models.Consumables;
using FindAndBook.Web.Models.Home;
using FindAndBook.Web.Models.Navigation;
using FindAndBook.Web.Models.Places;

namespace FindAndBook.Web.Factories
{
    public interface IViewModelFactory
    {
        HomeViewModel CreateHomeViewModel(bool isAuthenticated);

        CreateViewModel CreateCreateViewModel();

        NavigationViewModel CreateNavigationViewModel(bool isAuthenticated, bool isManager,
            bool isAdmin, string username, string userId);

        BookingFormViewModel CreateBookingFormViewModel(int two, int four, int six,
            Guid? placeId, DateTime dateTime);

        BookingViewModel CreateBookingFormViewModel(Guid? id);

        OrderFormViewModel CreateOrderFormViewModel(Guid? bookingId, ICollection<Consumable> placeMenu);

        CreateMenuViewModel CreateCreateMenuViewModel(Guid? id);
    }
}