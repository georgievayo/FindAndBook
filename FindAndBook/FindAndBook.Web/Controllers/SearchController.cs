using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using FindAndBook.Services.Contracts;
using AutoMapper.QueryableExtensions;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Places;
using FindAndBook.Web.Models.Search;
using PagedList;

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

        [HttpGet]
        public ActionResult Search([Bind(Prefix = "category")] string category, 
            [Bind(Prefix = "searchBy")] string searchBy, [Bind(Prefix = "pattern")] string pattern, int count = 10, int page =1)
        {
            var found = this.searchService
                .FindBy(category, searchBy, pattern)
                .Include(p => p.Reviews)
                .Include(p => p.Address)
                .ProjectTo<PlaceShortViewModel>()
                .ToList();

            var model = found.ToPagedList(page, count);
            return View("List", model);
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            return RedirectToAction("Search", new { category = model.Category, searchBy = model.SearchBy, pattern = model.Pattern });
        }
    }
}