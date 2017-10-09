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

            this.CategoryOptions = new List<SelectListItem>();
            this.CategoryOptions.Add(new SelectListItem {Text = "Restaurant", Value = "Restaurant"});
            this.CategoryOptions.Add(new SelectListItem { Text = "Club", Value = "Club" });
            this.CategoryOptions.Add(new SelectListItem { Text = "Cafe", Value = "Cafe" });

        }

        public string Category { get; set; }

        public string SearchBy { get; set; }

        public string Pattern { get; set; }

        public List<SelectListItem> SearchByOptions { get; set; }

        public List<SelectListItem> CategoryOptions { get; set; }

    }
}