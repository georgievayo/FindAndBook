using System.Collections.Generic;
using System.Web.Mvc;

namespace FindAndBook.Web.Models.Search
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            this.SearchByOptions = new List<SelectListItem>();
            this.SearchByOptions.Add(new SelectListItem {Text = "Name", Value = "Name"});
            this.SearchByOptions.Add(new SelectListItem { Text = "Description", Value = "Description" });
            this.SearchByOptions.Add(new SelectListItem { Text = "Address", Value = "Address" });
            this.SearchByOptions.Add(new SelectListItem { Text = "Average bill", Value = "Average bill" });
        }

        public string Category { get; set; }

        public string SearchBy { get; set; }

        public string Pattern { get; set; }

        public List<SelectListItem> SearchByOptions { get; set; }
    }
}