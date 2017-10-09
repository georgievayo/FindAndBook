using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindAndBook.Models
{
    public partial class Consumable
    {
        public Consumable()
        {
            
        }

        public Consumable(Guid? placeId, string name, string type, 
            decimal? price, int? quantity, string ingredients)
        {
            PlaceId = placeId;
            Name = name;
            Type = type;
            Price = price;
            Quantity = quantity;
            Ingredients = ingredients;
        }

        public Guid Id { get; set; }

        public Guid? PlaceId { get; set; }

        public virtual Place Place { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public string Ingredients { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
