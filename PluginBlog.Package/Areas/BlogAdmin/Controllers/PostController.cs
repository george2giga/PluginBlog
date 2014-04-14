using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PluginBlog.Core;
using PluginBlog.Core.Entities;
using PluginBlog.Core.Repositories;
using PluginBlog.Web.Areas.BlogAdmin.Models;
using PluginBlog.Web.Helpers;

namespace PluginBlog.Web.Areas.BlogAdmin.Controllers
{
    public class PostController : BaseBlogController
    {
        private readonly IBlogRepository _blogRepository;
        
        public PostController()
        {
            _blogRepository = PluginBlogConfig.GetRepository();
        }

        public PostController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        //
        // GET: /BlogAdmin/Post/
        public ActionResult Index(int? page, int? pageSize)
        {
            var posts = _blogRepository.AllPosts().AsNoTracking().OrderByDescending(x=>x.PostedOn);
            var model = new PaginatedList<Post>(posts, page ?? 0, pageSize ?? PAGE_SIZE);
            return View(model);
        }

        //
        // GET: /BlogAdmin/Post/Details/5
        public ActionResult Details(int id)
        {
            var model = _blogRepository.Post(id);
            return View(model);
        }

        //
        // GET: /BlogAdmin/Post/Create
        public ActionResult Create()
        {
            var model = new EditPost() { Post = new Post(){Tags = new Collection<Tag>()}};
            InitModel(model);
            return View(model);
        }

        //
        // POST: /BlogAdmin/Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(EditPost model)
        {
            
            if (ModelState.IsValid)
            {
                var post = GetPostFromModel(model);
                //Action ac = () => _blogRepository.AddPost(model.Post);
                //getvalidationerrors(ac);
                _blogRepository.AddPost(post);
                SetDbMessage(true, string.Format("New post created: {0}", model.Post.Title));
                return RedirectToAction("Index");
            }
            InitModel(model);
            ShowErrors();
            return View(model);
        }

        private void InitModel(EditPost editPost)
        {
            editPost.Categories = _blogRepository.Categories().ToList();
            editPost.Tags = _blogRepository.Tags().ToList();
        }

        //
        // GET: /BlogAdmin/Post/Edit/5
        public ActionResult Edit(int id)
        {
            var post  = _blogRepository.Post(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var model = new EditPost()
                    {
                        Post = post,
                        SelectedCategory = post.Category.Id,
                        SelectedTags = post.Tags.Select(x => x.Id).ToArray()
                    };
           
            InitModel(model);
            return View(model);
        }

        //
        // POST: /BlogAdmin/Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(EditPost model)
        {
            if (ModelState.IsValid)
            {
                var post = GetPostFromModel(model);
                _blogRepository.EditPost(post);
                //getvalidationerrors(_blogRepository.Context);
                SetDbMessage(true, string.Format("Category edited: {0}", model.Post.Title));
                
                return RedirectToAction("Index");
            }
            InitModel(model);
            ShowErrors();
            return View(model);
        }


        private void getvalidationerrors(Action action)
        {
            try
            {
                //context.SaveChanges();
                action();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

        // GET: /BlogAdmin/Tag/Delete/5
        public ActionResult Delete(int id)
        {
            var post = _blogRepository.Post(id);

            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /BlogAdmin/Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _blogRepository.DeletePost(id);
            SetDbMessage(true, "Post has been cancelled");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(int? page, string searchText, int? pagesize)
        {
            var result = _blogRepository.AllPostsForSearch(searchText).AsNoTracking().OrderByDescending(x => x.PostedOn);
            var model = new PaginatedList<Post>(result, page ?? 0,pagesize?? PAGE_SIZE);
            return View("Index", model);
        }


        #region Add/Edit Post

        private Post GetPostFromModel(EditPost model)
        {
            Post result = null;
            //create
            if (model.Post.Id == 0)
            {
                model.Post.CategoryId = model.SelectedCategory;
                model.Post.PostedOn = DateTime.UtcNow;
                result = model.Post;
            }
            else //edit
            {
                result = _blogRepository.Post(model.Post.Id);
                result.Title = model.Post.Title;
                result.CategoryId = model.SelectedCategory;
                result.ShortDescription = model.Post.ShortDescription;
                result.Description = model.Post.Description;
                result.UrlSlug = model.Post.UrlSlug;
                result.Meta = model.Post.Meta;
                result.LastModified = DateTime.UtcNow;
                result.Published = model.Post.Published;
                result.Tags.Clear();

            }
            //update/insert tags 
            if (model.SelectedTags != null && model.SelectedTags.Any())
            {
                result.Tags = _blogRepository.Tags().Where(x => model.SelectedTags.Contains(x.Id)).ToList();
            }

         
            return result;
        }

     

        #endregion
    }
}
