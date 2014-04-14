using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PluginBlog.Core;
using PluginBlog.Core.Entities;
using PluginBlog.Core.Repositories;
using PluginBlog.Web.Areas.BlogAdmin.Models;

namespace PluginBlog.Web.Helpers
{
    /// <summary>
    /// Bind POST model to actions.
    /// </summary>
    public class PostModelBinder : DefaultModelBinder
    {
        private IBlogRepository _blogRepository;

        //public static BlogContext BlogContext
        //{
        //    get { return System.Web.HttpContext.Current.Items["_BlogContext"] as BlogContext; }
        //}

        //public PostModelBinder()
        //{
        //    _blogRepository = new BlogRepository(BlogContext);
        //}

        public PostModelBinder(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //var blogRepository = new BlogRepository(BlogContext);
            if (_blogRepository == null)
            {
                _blogRepository = new BlogRepository(new BlogContext());
            }
            var post = (EditPost)base.BindModel(controllerContext, bindingContext);
            if (post.SelectedCategory != 0)
            {
                //post.Post.Category = new Category();
                post.Post.CategoryId = post.SelectedCategory;
            }

            if (post.SelectedTags != null && post.SelectedTags.Length > 0)
            {
                var tags = post.SelectedTags;
           
                post.Tags = new List<Tag>();

                foreach (var tag in tags)
                {
                    post.Tags.Add(_blogRepository.Tag(tag));
                }
           
            }
            if (controllerContext.RouteData.Values["action"].ToString() == "Create")
            {
                //if (bindingContext.ValueProvider.GetValue("oper").AttemptedValue.Equals("edit"))
                post.Post.PostedOn = DateTime.UtcNow;
            }
            else
            {
                post.Post = GetPostFromModel(post);
                post.Post.LastModified = DateTime.UtcNow; // dates are stored in UTC timezone.
            }
            //_blogRepository = null;
            //_blogRepository.Context.Entry(post.Tags).State = EntityState.Detached;
            //_blogRepository.Context.Entry(post.Post).State = EntityState.Detached;
            return post;
        }

        private Post GetPostFromModel(EditPost model)
        {
            var dbPost = _blogRepository.Post(model.Post.Id);
            
            dbPost.Title = model.Post.Title;
            dbPost.CategoryId = model.SelectedCategory;
            dbPost.ShortDescription = model.Post.ShortDescription;
            dbPost.Description = model.Post.Description;
            dbPost.UrlSlug = model.Post.UrlSlug;
            dbPost.Meta = model.Post.Meta;
            dbPost.LastModified = DateTime.UtcNow;
            dbPost.Published = model.Post.Published;

            if (model.SelectedTags != null && model.SelectedTags.Length > 0)
            {
                dbPost.Tags.Clear();
                foreach (var tag in model.SelectedTags)
                {
                    dbPost.Tags.Add(_blogRepository.Tag(tag));
                }

            }
            
            return dbPost;
        }
    }
}