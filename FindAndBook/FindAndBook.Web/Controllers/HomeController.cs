using System;
using System.Web.Mvc;
using FindAndBook.Services.Contracts;
using FindAndBook.Web.Factories;
using FindAndBook.Web.Models.Home;

namespace FindAndBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IViewModelFactory viewModelFactory;
        private readonly IQuestionService questionService;

        public HomeController(IViewModelFactory viewModelFactory, IQuestionService questionService)
        {
            if (viewModelFactory == null)
            {
                throw new ArgumentNullException(nameof(viewModelFactory));
            }

            if (questionService == null)
            {
                throw new ArgumentNullException(nameof(questionService));
            }

            this.viewModelFactory = viewModelFactory;
            this.questionService = questionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = this.viewModelFactory.CreateSearchViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendQuestion(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", model);
            }

            var name = model.FirstName + " " + model.LastName;
            this.questionService.AddQuestion(name, model.Email, model.Subject, model.Question);

            return View("SentQuestion");
        }
    }
}