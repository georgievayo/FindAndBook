using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindAndBook.Web.Models.Consumables
{
    public class ConsumableViewModel
    {
        public ConsumableViewModel()
        {
            
        }

        public ConsumableViewModel(Guid? id)
        {
            PlaceId = id;
        }

        public Guid? Id { get; set; }

        public Guid? PlaceId { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public string Type { get; set; }

        public int Quantity { get; set; }

        public string Ingredients { get; set; }
    }
}