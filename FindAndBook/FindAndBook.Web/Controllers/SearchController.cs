using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Models.Search;
using AutoMapper.QueryableExtensions;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;

namespace FindAndBook.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;
        private readonly IViewModelFactory factory;

        public SearchController(ISearchService searchService)
        {
            if (searchService == null)
            {
                throw new ArgumentNullException(nameof(searchService));
            }

            this.searchService = searchService;
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            var found = this.searchService
                .FindBy(model.Category, model.SearchBy, model.Pattern)
                .Include(p => p.Reviews)
                .Include(p => p.Address)
                .ProjectTo<PlaceShortViewModel>()
                .ToList();

            return View("List", found);
        }
    }
}