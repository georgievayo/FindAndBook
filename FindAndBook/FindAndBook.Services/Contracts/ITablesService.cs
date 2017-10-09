using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface ITablesService
    {
        Table CreateTableType(Guid? placeId, int numberOfPeople, int numberOfTables);

        int GetTablesCount(Guid? id, int peopleCount);

        Table GetByPlaceAndNumberOfPeople(Guid? placeId, int numberOfPeople);
    }
}
