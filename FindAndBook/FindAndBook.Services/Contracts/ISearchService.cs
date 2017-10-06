using System.Linq;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface ISearchService
    {
        IQueryable<Place> FindBy(string category, string searchBy, string pattern);
    }
}
