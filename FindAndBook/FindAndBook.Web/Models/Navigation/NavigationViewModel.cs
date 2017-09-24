namespace FindAndBook.Web.Models.Navigation
{
    public class NavigationViewModel
    {
        public NavigationViewModel(bool isAuthenticated, bool isManager, bool isAdmin)
        {
            this.IsAuthenticated = isAuthenticated;
            this.IsManager = isManager;
            this.IsAdmin = isAdmin;
        }

        public bool IsAuthenticated { get; set; }

        public bool IsManager { get; set; }

        public bool IsAdmin { get; set; }
    }
}