using System;
using System.Collections.Generic;

namespace FindAndBook.Web.Models.Consumables
{
    public class CreateMenuViewModel
    {
        public CreateMenuViewModel()
        {
            
        }

        public CreateMenuViewModel(Guid? id)
        {
            this.PlaceId = id;
        }

        public Guid? PlaceId { get; set; }
    }
}