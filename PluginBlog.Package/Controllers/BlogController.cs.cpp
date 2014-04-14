using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using PluginBlog.Core;
using PluginBlog.Core.Entities;
using PluginBlog.Core.Repositories;
using PluginBlog.Web.Helpers;

namespace $rootnamespace$.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _repository;
        private const int PageSize = 10;

        public BlogController()
        {
            _repository = new BlogRepository(new BlogContext());
        }

        public BlogController(IBlogRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int? page)
        {
            var result = _repository.Posts().OrderByDescending(x => x.PostedOn).Take(PageSize);
            var model = new PaginatedList<Post>(result, page ?? 0, PageSize);
            return View(model);
        }

        public ActionResult Details(int year, int month, string title)
        {
            var model = _repository.Post(year, month, title);

            if (model == null)
                throw new HttpException(404, "Post not found");
            return View(model);
        }

        public ActionResult Category(int? page, string urlSlug)
        {
            var result = _repository.PostsForCategory(urlSlug).OrderByDescending(x=>x.PostedOn);
            var model = new PaginatedList<Post>(result, page ?? 0, PageSize);
            return View("Index", model);
        }

        public ActionResult Tag(int? page, string urlSlug)
        {
            var result = _repository.PostsForTag(urlSlug).OrderByDescending(x => x.PostedOn); ;
            var model = new PaginatedList<Post>(result, page ?? 0, PageSize);
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Search(int? page, string searchText)
        {
            var result = _repository.PostsForSearch(searchText).OrderByDescending(x => x.PostedOn); ;
            var model = new PaginatedList<Post>(result, page ?? 0, PageSize);
            return View("Index", model);
        }

        /// <summary>
        /// Generate and return RSS feed.
        /// </summary>
        /// <returns></returns>
        public ActionResult Feed()
        {
            //var blogTitle = ConfigurationManager.AppSettings["BlogTitle"];
            //var blogDescription = ConfigurationManager.AppSettings["BlogDescription"];
            //var blogUrl = ConfigurationManager.AppSettings["BlogUrl"];
            var blogTitle = "ciao";
            var blogDescription = "Desc";
            var blogUrl = "http://www.google.com";


            var posts = _repository.Posts().Take(20).ToList().Select
            (
                p => new SyndicationItem
                    (
                        p.Title,
                        p.Description,
                        new Uri(string.Concat(blogUrl, Url.PostLink(p)))
                    )
            );

            var feed = new SyndicationFeed(blogTitle, blogDescription, new Uri(blogUrl), posts)
            {
                Copyright = new TextSyndicationContent(String.Format("Copyright © {0}", blogTitle)),
                Language = "en-US"
            };

            return new FeedResult(new Rss20FeedFormatter(feed));
        }

	}
}