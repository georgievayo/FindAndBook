using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FindAndBook.Authentication.Contracts
{
    public interface IAuthenticationProvider
    {
        bool IsAuthenticated { get; }

        string CurrentUserId { get; }

        string CurrentUserUsername { get; }

        IdentityResult RegisterAndLoginUser(User user, string password, bool isPersistent, bool rememberBrowser);

        SignInStatus SignInWithPassword(string email, string password, bool rememberMe, bool shouldLockout);

        void SignOut();

        bool IsInRole(string userId, string roleName);

        IdentityResult AddToRole(string userId, string roleName);

        IdentityResult RemoveFromRole(string userId, string roleName);

        void BanUser(string userId);

        void UnbanUser(string userId);
    }
}
