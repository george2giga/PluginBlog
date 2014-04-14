using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PluginBlog.Core.Entities;
using PluginBlog.Core;
using PluginBlog.Core.Repositories;
using PluginBlog.Web.Areas.BlogAdmin.Models;
using PluginBlog.Web.Helpers;

namespace PluginBlog.Web.Areas.BlogAdmin.Controllers
{
    public class TagController : BaseBlogController
    {
        private IBlogRepository _blogRepository;
        private const int PAGE_SIZE = 10;

        public TagController()
        {
            _blogRepository = PluginBlogConfig.GetRepository();
        }

        public TagController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        // GET: /BlogAdmin/Tag/
        public ActionResult Index(int? page, int? pageSize)
        {
            var tags = _blogRepository.Tags().AsNoTracking().OrderBy(x=>x.Name);
            var model = new PaginatedList<Tag>(tags, page ?? 0, pageSize ?? PAGE_SIZE);
            return View(model);
        }

        // GET: /BlogAdmin/Tag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tag tag = db.Tags.Find(id);
            var tag = _blogRepository.Tag(id.Value);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: /BlogAdmin/Tag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BlogAdmin/Tag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,UrlSlug,Description")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.AddTag(tag);
                SetDbMessage(true, string.Format("New tag created: {0}", tag.Name));
                return RedirectToAction("Index");
            }
            ShowErrors();
            return View(tag);
        }

        // GET: /BlogAdmin/Tag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = _blogRepository.Tag(id.Value);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: /BlogAdmin/Tag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,UrlSlug,Description")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.EditTag(tag);
                SetDbMessage(true, string.Format("Tag edited: {0}", tag.Name));
                return RedirectToAction("Index");
            }
            ShowErrors();
            return View(tag);
        }

        // GET: /BlogAdmin/Tag/Delete/5
        public ActionResult Delete(int id)
        {
            var tag = _blogRepository.Tag(id);
            
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: /BlogAdmin/Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cancel = _blogRepository.Tag(id).Posts.Any() == false;
            if (cancel)
            {
                _blogRepository.DeleteTag(id);
                SetDbMessage(true, "Tag has been cancelled");
            }
            else
            {
                SetDbMessage(false, "Cancellation failed : tag in use!");
            }
            return RedirectToAction("Index");
        }

    }
}
