using System;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IBookedTablesService
    {
        void AddBookedTables(Guid? bookingId, Guid? tableId, int tablesCount);

        BookedTables GetBookedTable(Guid? bookingId);
    }
}
