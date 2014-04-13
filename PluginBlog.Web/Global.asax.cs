using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PluginBlog.Core;
using PluginBlog.Core.Entities;
using PluginBlog.Core.Repositories;
using PluginBlog.Web.Areas.BlogAdmin.Models;
using PluginBlog.Web.Helpers;

namespace PluginBlog.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ModelBinders.Binders.Add(typeof(EditPost), new PostModelBinder(null));
        }

        protected virtual void Application_BeginRequest()
        {
            //HttpContext.Current.Items["_BlogContext"] = new BlogContext();
        }

        protected virtual void Application_EndRequest()
        {
            //var entityContext = HttpContext.Current.Items["_BlogContext"] as BlogContext;
            //if (entityContext != null)
            //    entityContext.Dispose();
        }
    }
}
