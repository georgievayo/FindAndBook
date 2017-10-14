using System.Collections.Generic;

namespace FindAndBook.Web.Areas.Administration.Models
{
    public class AllInformationViewModel
    {
        public AllInformationViewModel()
        {
            
        }

        public AllInformationViewModel(ICollection<UserViewModel> users, ICollection<ReviewViewModel> reviews, ICollection<PlaceViewModel> places)
        {
            Users = users;
            Reviews = reviews;
            Places = places;
        }

        public ICollection<UserViewModel> Users { get; set; }

        public ICollection<ReviewViewModel> Reviews { get; set; }

        public ICollection<PlaceViewModel> Places { get; set; }
    }
}