using System;
using System.Linq;
using System.Web.Mvc;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Bookings;
using FindAndBook.Web.Models.Places;

namespace FindAndBook.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IAuthenticationProvider authProvider;
        private readonly IPlaceService placeService;
        private readonly IBookingService bookingService;
        private readonly IViewModelFactory factory;
        private readonly IConsumableService consumableService;

        public BookingsController(IAuthenticationProvider authProvider,
            IPlaceService placeService, IBookingService bookingService, 
            IViewModelFactory factory, IConsumableService consumableService)
        {
            if (authProvider == null)
            {
                throw new ArgumentNullException(nameof(authProvider));
            }

            if (placeService == null)
            {
                throw new ArgumentNullException(nameof(placeService));
            }

            if (bookingService == null)
            {
                throw new ArgumentNullException(nameof(bookingService));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (consumableService == null)
            {
                throw new ArgumentNullException(nameof(consumableService));
            }

            this.authProvider = authProvider;
            this.placeService = placeService;
            this.bookingService = bookingService;
            this.factory = factory;
            this.consumableService = consumableService;
        }

        public ActionResult GetBookingForm(Guid? id)
        {
            var model = this.factory.CreateBookingFormViewModel(id);
            if (authProvider.IsAuthenticated)
            {
                return PartialView("_BookingDateTimeForm", model);
            }
            else
            {
                return PartialView("Error");
            }
        }


        [HttpPost]
        public ActionResult GetAvailableTables(BookingViewModel model)
        {
            var bookings = this.bookingService
                .FindAllOn(model.DateTime, model.PlaceId).ToList();

            var reservedTwoPeopleTables = 0;
            var reservedFourPeopleTables = 0;
            var reservedSixPeopleTables = 0;

            foreach (var booking in bookings)
            {
                foreach (var table in booking.Tables)
                {
                    if (table.NumberOfPeople == 2)
                    {
                        reservedTwoPeopleTables += table.NumberOfTables;
                    }
                    else if (table.NumberOfPeople == 4)
                    {
                        reservedFourPeopleTables += table.NumberOfTables;
                    }
                    else
                    {
                        reservedSixPeopleTables += table.NumberOfTables;
                    }
                }
            }

            var allTablesTwoPeople = this.placeService.GetTwoPeopleTablesCount(model.PlaceId);
            var availableTwoPeople = allTablesTwoPeople - reservedTwoPeopleTables;

            var allTablesFourPeople = this.placeService.GetFourPeopleTablesCount(model.PlaceId);
            var availableFourPeople = allTablesFourPeople - reservedFourPeopleTables;

            var allTablesSixPeople = this.placeService.GetSixPeopleTablesCount(model.PlaceId);
            var availableSixPeople = allTablesSixPeople - reservedSixPeopleTables;

            var availableTablesModel =
                this.factory.CreateBookingFormViewModel(availableTwoPeople, availableFourPeople,
                    availableSixPeople, model.PlaceId, model.DateTime);

            return PartialView("_AvailableTables", availableTablesModel);
        }

        [HttpPost]
        public ActionResult BookTables(BookingFormViewModel model)
        {
            var currentUserId = this.authProvider.CurrentUserId;
            var booking = this.bookingService.CreateBooking(model.PlaceId, currentUserId, model.DateTime);
            model.BookingId = booking.Id;
            return PartialView("_SuccessfullBooking", model);
        }

        [HttpGet]
        public ActionResult Order(Guid? id, Guid? bookingId)
        {
            var menu = this.consumableService.GetAllConsumablesOf(id).ToList();

            var model = this.factory.CreateOrderFormViewModel(bookingId, menu);

            return View(model);
        }

        [HttpPost]
        public ActionResult Order(OrderFormViewModel model)
        {
            string selected = Request.Form["consumable"];
            string[] selectedList = selected.Split(',');

            foreach (var select in selectedList)
            {
                var consumable = this.consumableService.GetByName(select);
                var booking = this.bookingService.GetById(model.BookingId);
                this.consumableService.AddBooking(consumable, booking);
            }

            return View(model);
        }
    }
}