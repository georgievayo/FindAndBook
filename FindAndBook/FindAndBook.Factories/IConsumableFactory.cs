using System;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IConsumableFactory
    {
        Consumable CreateConsumable(Guid? placeId, string name, int quantity, decimal? price, string type, string ingredients);
    }
}
