using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface ITablesFactory
    {
        Table CreateTableType(Guid? placeId, int numberOfPeople, int numberOfTables);
    }
}
