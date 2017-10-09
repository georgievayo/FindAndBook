using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IBookedTablesFactory
    {
        BookedTables CreateBookedTable(Guid? bookingId, Guid? tableId, int count);
    }
}
