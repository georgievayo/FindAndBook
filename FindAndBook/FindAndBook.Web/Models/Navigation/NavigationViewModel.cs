namespace FindAndBook.Web.Models.Navigation
{
    public class NavigationViewModel
    {
        public NavigationViewModel(bool isAuthenticated, bool isManager, bool isAdmin, string username, string userId)
        {
            this.IsAuthenticated = isAuthenticated;
            this.IsManager = isManager;
            this.IsAdmin = isAdmin;
            this.Username = username;
        }

        public bool IsAuthenticated { get; set; }

        public bool IsManager { get; set; }

        public bool IsAdmin { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }
    }
}