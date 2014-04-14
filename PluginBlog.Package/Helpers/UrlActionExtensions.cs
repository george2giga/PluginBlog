using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using PluginBlog.Core.Entities;

namespace PluginBlog.Web.Helpers
{
    public static class UrlActionExtensions
    {
        public static string PostLink(this UrlHelper helper, Post post)
        {
            return helper.Action("Details", "Blog", new { year = post.PostedOn.Year, month = post.PostedOn.Month, title = post.UrlSlug });
        }

        public static string CategoryLink(this UrlHelper helper, Category category)
        {
            return helper.Action("Category", "Blog", new { urlSlug = category.UrlSlug });
        }

        public static string TagLink(this UrlHelper helper, Tag tag)
        {
            return helper.Action("Tag", "Blog", new { urlSlug = tag.UrlSlug });
        }

        public static string CurrentUrl(this UrlHelper helper )
        {
            var controller = helper.RequestContext.RouteData.GetRequiredString("controller");
            var action = helper.RequestContext.RouteData.GetRequiredString("action");
            return helper.Action(action, controller);
        }

        public static string PaginateLink(this UrlHelper helper, int? page, int pageSize=10)
        {
            var query = helper.RequestContext.HttpContext.Request.QueryString;
            var values = query.AllKeys.ToDictionary(key => key, key => (object)query[key]);
            if (query.AllKeys.Contains("page"))
            {
                values["page"] = page;
            }
            if(query.AllKeys.Contains("pagesize"))
            { 
                values["pagesize"] = pageSize;
            }
            var routeValues = new RouteValueDictionary(values);
            var controller = helper.RequestContext.RouteData.GetRequiredString("controller");
            var action = helper.RequestContext.RouteData.GetRequiredString("action");
            return helper.Action(action, controller, routeValues);
        }

    }
}