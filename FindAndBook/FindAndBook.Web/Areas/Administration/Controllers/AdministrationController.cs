﻿using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using FindAndBook.Authentication.Contracts;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Areas.Administration.Models;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Reviews;

namespace FindAndBook.Web.Areas.Administration.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IPlaceService placeService;
        private readonly IReviewsService reviewService;
        private readonly IUserService userService;
        private readonly IQuestionService questionService;
        private readonly IAuthenticationProvider authProvider;
        private readonly IViewModelFactory factory;

        public AdministrationController(IPlaceService placeService, IReviewsService reviewService,
            IAuthenticationProvider authProvider, IUserService userService, 
            IQuestionService questionService, IViewModelFactory factory)
        {
            if (placeService == null)
            {
                throw new ArgumentNullException(nameof(placeService));
            }

            if (reviewService == null)
            {
                throw new ArgumentNullException(nameof(reviewService));
            }

            if (authProvider == null)
            {
                throw new ArgumentNullException(nameof(authProvider));
            }

            if (userService == null)
            {
                throw new ArgumentNullException(nameof(userService));
            }

            if (questionService == null)
            {
                throw new ArgumentNullException(nameof(questionService));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.placeService = placeService;
            this.reviewService = reviewService;
            this.userService = userService;
            this.questionService = questionService;
            this.authProvider = authProvider;
            this.factory = factory;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var places = this.placeService
                .GetAll()
                .ProjectTo<PlaceViewModel>()
                .ToList();

            var reviews = this.reviewService
                .GetAll()
                .ProjectTo<ReviewViewModel>()
                .ToList();

            var users = this.userService
                .GetAll()
                .ToList()
                .Select(user =>
                {
                    var isAdmin = this.authProvider.IsInRole(user.Id, "Admin");
                    return this.factory.CreateUserViewModel(user, isAdmin);
                })
                .ToList();

            var questions = this.questionService
                .GetAll()
                .ProjectTo<QuestionViewModel>()
                .ToList();

            var model = this.factory.CreateAllInformationViewModel(users, reviews, places, questions);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePlace(Guid? id)
        {
            this.placeService.DeletePlace(id);
            return new EmptyResult();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteReview(Guid? id)
        {
            this.reviewService.DeleteReview(id);
            return new EmptyResult();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddAdmin(string id)
        {
            this.authProvider.AddToRole(id, "Admin");
            return PartialView("_RemoveFromRole", id);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveAdmin(string id)
        {
            this.authProvider.RemoveFromRole(id, "Admin");

            return PartialView("_AddToRole", id);
        }
    }
}