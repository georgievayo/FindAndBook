using System.Collections.Generic;
using FindAndBook.Models;

namespace FindAndBook.Web.Areas.Administration.Models
{
    public class AllInformationViewModel
    {
        public AllInformationViewModel()
        {
            
        }

        public AllInformationViewModel(ICollection<User> users, ICollection<Review> reviews, ICollection<Place> places)
        {
            Users = users;
            Reviews = reviews;
            Places = places;
        }

        public ICollection<User> Users { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Place> Places { get; set; }
    }
}