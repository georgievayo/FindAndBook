using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Models.Search;

namespace FindAndBook.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

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
            return null;
        }
    }
}