using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PluginBlog.Web.Areas.BlogAdmin.Models;
using PluginBlog.Web.Helpers;

namespace PluginBlog.Web.Areas.BlogAdmin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AuthController()
        {
            _authProvider = PluginBlogConfig.GetAuthorization();
        }

        public AuthController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        public ActionResult Logout()
        {
            _authProvider.Logout();
            return Redirect("~/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var login = _authProvider.Login(model.Username, model.Password);
                if (login)
                {
                    return RedirectToAction("Index", "Post");
                }
            }
            return View(model);
        }
	}
}