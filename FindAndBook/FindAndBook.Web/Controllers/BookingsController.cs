﻿using System;
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

        [HttpGet]
        public ActionResult GetBookingForm(Guid? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var model = this.factory.CreateBookingFormViewModel(id);
            if (authProvider.IsAuthenticated)
            {
                return PartialView("_BookingDateTimeForm", model);
            }
            else
            {
                return PartialView("_LoginMessage");
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult GetAvailableTables(BookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_BookingDateTimeForm", model);
            }

            var bookings = this.bookingService
                .FindAllOn(model.DateTime, model.PlaceId)
                .ToList();

            var reservedTwoPeopleTables = 0;
            var reservedFourPeopleTables = 0;
            var reservedSixPeopleTables = 0;

            foreach (var booking in bookings)
            {
                var bookedTables = this.bookedTablesService.GetBookedTable(booking.Id);

                    if (bookedTables.Table.NumberOfPeople == 2)
                    {
                        reservedTwoPeopleTables += bookedTables.TablesCount;
                    }
                    else if (bookedTables.Table.NumberOfPeople == 4)
                    {
                        reservedFourPeopleTables += bookedTables.TablesCount;
                    }
                    else if(bookedTables.Table.NumberOfPeople == 6)
                    {
                        reservedSixPeopleTables += bookedTables.TablesCount;
                    }
            }

            var allTables = this.tablesService.GetTablesCount(model.PlaceId, 2);
            var availableTwoPeople = allTables - reservedTwoPeopleTables;

            allTables = this.tablesService.GetTablesCount(model.PlaceId, 4);
            var availableFourPeople = allTables - reservedFourPeopleTables;

            allTables = this.tablesService.GetTablesCount(model.PlaceId, 6);
            var availableSixPeople = allTables - reservedSixPeopleTables;

            var availableTablesModel =
                this.factory.CreateBookingFormViewModel(availableTwoPeople, availableFourPeople,
                    availableSixPeople, model.PlaceId, model.DateTime);

            return PartialView("_AvailableTables", availableTablesModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult BookTables(BookingFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_AvailableTables", model);
            }

            var currentUserId = this.authProvider.CurrentUserId;
            var peopleCount = 2 * model.TwoPeopleInput + 4 * model.FourPeopleInput + 6 * model.SixPeopleInput;

            var booking = this.bookingService.CreateBooking(model.PlaceId, currentUserId, model.DateTime, peopleCount);
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
        [Authorize]
        public ActionResult Order(Guid? id, Guid? bookingId)
        {
            if (id == null || bookingId == null)
            {
                return View("Error");
            }

            var menu = this.consumableService.GetAllConsumablesOf(id).ToList();

            var model = this.factory.CreateOrderFormViewModel(bookingId, menu);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Order(OrderFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string selected = Request.Form["consumable"];
            string[] selectedList = selected.Split(',');
            decimal currentBill = 0;
            foreach (var select in selectedList)
            {
                var consumable = this.consumableService.GetByName(select);
                currentBill += (decimal)consumable.Price;
                var booking = this.bookingService.GetById(model.BookingId);
                this.consumableService.AddBooking(consumable, booking);
            }
            ViewBag.CurrentBill = currentBill;
            return PartialView("_CurrentBill");
        }

        [Authorize]
        public ActionResult GetBookings(string id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var idGuid = new Guid(id);
            var bookings = this.bookingService
                .GetBookingsOfPlace(idGuid)
                .ToList();

            return PartialView("_PlaceBookings", bookings);
        }

        [Authorize]
        public ActionResult CancelBooking(Guid? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            this.bookedTablesService.RemoveBookedTables(id);
            this.bookingService.RemoveBooking(id);

            return new EmptyResult();
        }
    }
}