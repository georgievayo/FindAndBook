using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FindAndBook.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private readonly IAuthenticationProvider provider;
        private readonly IUserService userService;
        private readonly IViewModelFactory viewModelFactory;

        public AccountController(IAuthenticationProvider provider,
            IUserService userService, IViewModelFactory viewModelFactory)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (userService == null)
            {
                throw new ArgumentNullException(nameof(userService));
            }

            if (viewModelFactory == null)
            {
                throw new ArgumentNullException(nameof(viewModelFactory));
            }

            this.provider = provider;
            this.userService = userService;
            this.viewModelFactory = viewModelFactory;
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (this.provider.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/Home/Index" : returnUrl;

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = this.provider.SignInWithPassword(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return this.Redirect(returnUrl);
                case SignInStatus.LockedOut:
                    return this.View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return this.View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.userService.AddUser(model.Username, model.Email, model.FirstName, model.LastName, model.PhoneNumber);
                var result = this.provider.RegisterAndLoginUser(user, model.Password, isPersistent: false, rememberBrowser: false);
                var res = this.provider.AddToRole(user.Id, model.Role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            this.provider.SignOut();

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            this.provider.SignOut();

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Profile(string username)
        {
            var user = this.userService.GetUserByUsername(username)
                .ProjectTo<ProfileViewModel>()
                .ToList()
                .FirstOrDefault();

            if (user == null)
            {
                return View("Error");
            }

            user.IsCurrentUser = this.provider.CurrentUserUsername == user.Username;
            return View(user);
        }

        [ChildActionOnly]
        [Authorize]
        public ActionResult Bookings(ProfileViewModel model)
        {
            return this.PartialView("_Bookings", model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}