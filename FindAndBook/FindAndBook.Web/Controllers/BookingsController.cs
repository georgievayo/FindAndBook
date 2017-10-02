using System;
using System.Web.Mvc;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IAuthenticationProvider authProvider;
        private readonly IPlaceService placeService;
        private readonly IBookingService bookingService;

        public BookingsController(IAuthenticationProvider authProvider, 
            IPlaceService placeService, IBookingService bookingService)
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

            this.authProvider = authProvider;
            this.placeService = placeService;
            this.bookingService = bookingService;
        }

        public ActionResult GetBookingForm()
        {
            if (authProvider.IsAuthenticated)
            {
                return PartialView("_BookingDateTimeForm");
            }
            else
            {
                return View("Error");
            }
        }

        //public ActionResult PostBookingForm()
        //{
            
        //}
    }
}