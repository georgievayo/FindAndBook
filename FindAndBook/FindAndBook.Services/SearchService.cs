using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Data.Contracts;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class SearchService : ISearchService
    {
        private readonly IPlaceService placeService;

        public SearchService(IPlaceService placeService)
        {
            if (placeService == null)
            {
                throw new ArgumentNullException(nameof(placeService));
            }

            this.placeService = placeService;
        }

        public IQueryable<Place> FindBy(string category, string searchBy, string pattern)
        {
            if (searchBy == "Name")
            {
                return this.placeService.FindInName(category, pattern);
            }
            else if (searchBy == "Description")
            {
                return this.placeService.FindInDescription(category, pattern);
            }
            else if (searchBy == "Address")
            {
                return this.placeService.FindInAddress(category, pattern);
            }
            else
            {
                return this.placeService.FindInBill(category, pattern);
            }
        }
    }
}
