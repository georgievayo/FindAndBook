﻿using System;
using System.Collections.Generic;
using FindAndBook.Models;

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

        public ICollection<Consumable> Menu { get; set; }
       
    }
}