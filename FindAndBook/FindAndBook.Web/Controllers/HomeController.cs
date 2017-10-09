using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FindAndBook.Web.Factories;

namespace FindAndBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelFactory viewModelFactory;

        public HomeController(IViewModelFactory viewModelFactory)
        {
            if (viewModelFactory == null)
            {
                throw new ArgumentNullException(nameof(viewModelFactory));
            }

            this.viewModelFactory = viewModelFactory;
        }

        public ActionResult Index()
        {
            var model = this.viewModelFactory.CreateSearchViewModel();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}