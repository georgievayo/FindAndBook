using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAndBook.Services.Contracts
{
    public interface IBookedTablesService
    {
        void AddBookedTables(Guid? bookingId, Guid? tableId, int tablesCount);
    }
}
