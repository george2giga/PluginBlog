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
    public class CategoryController : BaseBlogController
    {
        private readonly IBlogRepository _blogRepository;

        public CategoryController()
        {
            _blogRepository = PluginBlogConfig.GetRepository();
        }

        public CategoryController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        // GET: /BlogAdmin/Category/
        public ActionResult Index(int? page, int? pageSize)
        {
            var categories = _blogRepository.Categories().AsNoTracking().OrderBy(x=>x.Name);
            var model = new PaginatedList<Category>(categories, page ?? 0, pageSize ?? PAGE_SIZE);
            return View(model);
        }

        // GET: /BlogAdmin/Category/Details/5
        public ActionResult Details(int id)
        {
            var category = _blogRepository.Category(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /BlogAdmin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BlogAdmin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,UrlSlug,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.AddCategory(category);
                SetDbMessage(true, string.Format("New category created: {0}", category.Name));
                return RedirectToAction("Index");
            }
            ShowErrors();
            return View(category);
        }

        // GET: /BlogAdmin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _blogRepository.Category(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /BlogAdmin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,UrlSlug,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
               _blogRepository.EditCategory(category);
               SetDbMessage(true, string.Format("Category edited: {0}", category.Name));
                return RedirectToAction("Index");
            }
            ShowErrors();
            return View(category);
        }

        // GET: /BlogAdmin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _blogRepository.Category(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /BlogAdmin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cancel = _blogRepository.Category(id).Posts.Any() == false;
            if (cancel)
            {
                _blogRepository.DeleteCategory(id);
                SetDbMessage(true, "Category has been cancelled");
            }
            else
            {
                SetDbMessage(false, "Cancellation failed : category in use!");
            }
           
            return RedirectToAction("Index");
        }

    }
}
