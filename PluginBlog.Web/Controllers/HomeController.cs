using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PluginBlog.Core;
using PluginBlog.Core.Repositories;

namespace PluginBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogRepository _repository;

        public HomeController()
        {
            _repository = new BlogRepository(new BlogContext());
        }

        public HomeController(IBlogRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var result = _repository.Posts().OrderByDescending(x => x.PostedOn).Take(3).ToList();
            return View(result);
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