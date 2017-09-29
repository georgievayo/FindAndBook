using FindAndBook.Web.Models.Home;
using FindAndBook.Web.Models.Navigation;
using FindAndBook.Web.Models.Places;

namespace FindAndBook.Web.Factories
{
    public interface IViewModelFactory
    {
        HomeViewModel CreateHomeViewModel(bool isAuthenticated);

        CreateViewModel CreateCreateViewModel();

        NavigationViewModel CreateNavigationViewModel(bool isAuthenticated, bool isManager, bool isAdmin, string username, string userId);
    }
}