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
        private readonly IBookedTablesService bookedTablesService;
        private readonly ITablesService tablesService;

        public BookingsController(IAuthenticationProvider authProvider,
            IPlaceService placeService, IBookingService bookingService,
            IViewModelFactory factory, IConsumableService consumableService,
            IBookedTablesService bookedTablesService, ITablesService tablesService)
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

            if (bookedTablesService == null)
            {
                throw new ArgumentNullException(nameof(bookedTablesService));
            }

            if (tablesService == null)
            {
                throw new ArgumentNullException(nameof(tablesService));
            }

            this.authProvider = authProvider;
            this.placeService = placeService;
            this.bookingService = bookingService;
            this.factory = factory;
            this.consumableService = consumableService;
            this.bookedTablesService = bookedTablesService;
            this.tablesService = tablesService;
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


        //[HttpPost]
        //public ActionResult GetAvailableTables(BookingViewModel model)
        //{
        //    var bookings = this.bookingService
        //        .FindAllOn(model.DateTime, model.PlaceId).ToList();

        //    var reservedTwoPeopleTables = 0;
        //    var reservedFourPeopleTables = 0;
        //    var reservedSixPeopleTables = 0;

        //    foreach (var booking in bookings)
        //    {
        //        this.bookedTablesService.GetBookedTablesCount()
        //        foreach (var table in booking.Tables)
        //        {
        //            if (table. == 2)
        //            {
        //                reservedTwoPeopleTables += table.NumberOfTables;
        //            }
        //            else if (table.NumberOfPeople == 4)
        //            {
        //                reservedFourPeopleTables += table.NumberOfTables;
        //            }
        //            else
        //            {
        //                reservedSixPeopleTables += table.NumberOfTables;
        //            }
        //        }
        //    }

        //    var allTables = this.tablesService.GetTablesCount(model.PlaceId, 2);
        //    var availableTwoPeople = allTables - reservedTwoPeopleTables;

        //    allTables = this.tablesService.GetTablesCount(model.PlaceId, 4);
        //    var availableFourPeople = allTables - reservedFourPeopleTables;

        //    allTables = this.tablesService.GetTablesCount(model.PlaceId, 6);
        //    var availableSixPeople = allTables - reservedSixPeopleTables;

        //    var availableTablesModel =
        //        this.factory.CreateBookingFormViewModel(availableTwoPeople, availableFourPeople,
        //            availableSixPeople, model.PlaceId, model.DateTime);

        //    return PartialView("_AvailableTables", availableTablesModel);
        //}

        [HttpPost]
        public ActionResult BookTables(BookingFormViewModel model)
        {
            var currentUserId = this.authProvider.CurrentUserId;
            var booking = this.bookingService.CreateBooking(model.PlaceId, currentUserId, model.DateTime);
            model.BookingId = booking.Id;
            if (model.TwoPeopleInput > 0)
            {
                var table = this.tablesService.GetByPlaceAndNumberOfPeople(model.PlaceId, 2);
                this.bookedTablesService.AddBookedTables(booking.Id, table.Id, model.TwoPeopleInput);
            }
            if (model.FourPeopleInput > 0)
            {
                var bookedTable = this.tablesService.GetByPlaceAndNumberOfPeople(model.PlaceId, 4);
                this.bookedTablesService.AddBookedTables(booking.Id, bookedTable.Id, model.FourPeopleInput);
            }
            if (model.SixPeopleInput > 0)
            {
                var bookedTable = this.tablesService.GetByPlaceAndNumberOfPeople(model.PlaceId, 6);
                this.bookedTablesService.AddBookedTables(booking.Id, bookedTable.Id, model.SixPeopleInput);
            }

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

        public ActionResult GetBookings(string id)
        {
            var idGuid = new Guid(id);
            var bookings = this.bookingService.GetBookingsOfPlace(idGuid).ToList();

            return PartialView("_PlaceBookings", bookings);
        }
    }
}