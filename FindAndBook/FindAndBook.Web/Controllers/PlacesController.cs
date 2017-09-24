using System;
using System.Web.Mvc;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Factories;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;

namespace FindAndBook.Web.Controllers
{
    public class PlacesController : Controller
    {
        private IViewModelFactory viewModelFactory;
        private IAuthenticationProvider authProvider;

        public PlacesController(IAuthenticationProvider authProvider, IViewModelFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (authProvider == null)
            {
                throw new ArgumentNullException(nameof(authProvider));
            }

            this.viewModelFactory = factory;
            this.authProvider = authProvider;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var userId = this.authProvider.CurrentUserId;
            var isManager = this.authProvider.IsInRole(userId, "Manager");
            var model = this.viewModelFactory.CreateCreateViewModel(isManager);

            return View(model);
        }

        //[Authorize]
        //[HttpPost]
        //public ActionResult Create(CreateViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.View(model);
        //    }

        //}
    }
}