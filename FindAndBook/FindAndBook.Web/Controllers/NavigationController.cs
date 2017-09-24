using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Web.Factories;
using Microsoft.AspNet.Identity;

namespace FindAndBook.Web.Controllers
{
    public class NavigationController : Controller
    {
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IViewModelFactory viewModelFactory;

        public NavigationController(IAuthenticationProvider authenticationProvider, IViewModelFactory viewModelFactory)
        {
            if (authenticationProvider == null)
            {
                throw new ArgumentNullException(nameof(authenticationProvider));
            }

            if (viewModelFactory == null)
            {
                throw new ArgumentNullException(nameof(viewModelFactory));
            }

            this.viewModelFactory = viewModelFactory;
            this.authenticationProvider = authenticationProvider;
        }

        public ActionResult Index()
        {
            var isAuthenticated = this.authenticationProvider.IsAuthenticated;
            var username = string.Empty;
            var isAdmin = false;
            var isManager = false;

            if (isAuthenticated)
            {
                username = this.authenticationProvider.CurrentUserUsername;
                var userId = this.authenticationProvider.CurrentUserId;

                isAdmin = this.authenticationProvider.IsInRole(userId, "admin");
            }

            var model = this.viewModelFactory.CreateNavigationViewModel(isAuthenticated, isManager, isAdmin);
            return this.PartialView("Navigation", model);
        }
    }
}