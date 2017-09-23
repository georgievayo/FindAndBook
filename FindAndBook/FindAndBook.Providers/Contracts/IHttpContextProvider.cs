using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Microsoft.Owin;

namespace FindAndBook.Providers.Contracts
{
    public interface IHttpContextProvider
    {
        HttpContext CurrentHttpContext { get; }

        IOwinContext CurrentOwinContext { get; }

        IIdentity CurrentIdentity { get; }

        TManager GetUserManager<TManager>();

        Cache ContextCache { get; }
    }
}
