using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginBlog.Core.Entities;

namespace PluginBlog.Core.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IBlogContext _blogContext;

        public BlogRepository()
        {
            _blogContext = new BlogContext();
        }

        public BlogRepository(IBlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public IQueryable<Post> AllPosts()
        {
            var result = _blogContext.Posts;
            return result; 
        }

        public IQueryable<Post> Posts(bool published = true)
        {
            var result = AllPosts().Where(x => x.Published == published);
            return result;
        }
      
        public IQueryable<Post> PostsForTag(string tagSlug)
        {
            var result = Posts().Where(x => x.Tags.Any(t => t.UrlSlug.Equals(tagSlug)));
            return result;
        }

        public IQueryable<Post> PostsForCategory(string categorySlug)
        {
            var result = Posts().Where(x => x.Category.UrlSlug.Equals(categorySlug));
            return result;
        }

        public IQueryable<Post> AllPostsForSearch(string search)
        {
            var result =
                AllPosts()
                    .Where(
                        x =>
                            (x.Title.Contains(search) || x.Category.Name.Equals(search) || x.Tags.Any(t => t.Name.Equals(search))));
            return result;
        }


        public IQueryable<Post> PostsForSearch(string search)
        {
            var result =
                Posts()
                    .Where(
                        x =>
                            (x.Title.Contains(search) || x.Category.Name.Equals(search) || x.Tags.Any(t => t.Name.Equals(search))));
            return result;
        }

        
     
        public Post Post(int year, int month, string titleSlug)
        {
            var result = Posts().Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug));

            return result.Single();
        }

        public Post Post(int id)
        {
            return _blogContext.Find<Post>(id);
        }

        public int AddPost(Post post)
        {
            _blogContext.Add(post);
            _blogContext.Commit();
            return post.Id;
        }

        public void EditPost(Post post)
        {
            _blogContext.Update(post);
            _blogContext.Commit();
        }

        public void DeletePost(int id)
        {
            _blogContext.Delete<Post>(id);
            _blogContext.Commit();
        }


        public IQueryable<Category> Categories()
        {
            return _blogContext.Categories;
        }


        public Category Category(string categorySlug)
        {
            return Categories().FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }

        public Category Category(int id)
        {
            return _blogContext.Find<Category>(id);
        }

        public int AddCategory(Category category)
        {
            _blogContext.Add(category);
            _blogContext.Commit();
            return category.Id;
        }

        public void EditCategory(Category category)
        {
            _blogContext.Update(category);
            _blogContext.Commit();
        }

        public void DeleteCategory(int id)
        {
            _blogContext.Delete<Category>(id);
            _blogContext.Commit();
        }

        public IQueryable<Tag> Tags()
        {
            return _blogContext.Tags.AsQueryable();
        }

        public int TotalTags()
        {
            return Tags().Count();
        }
       
        public Tag Tag(string tagSlug)
        {
            return Tags().FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }
      
        public Tag Tag(int id)
        {
            return _blogContext.Find<Tag>(id);
        }

        public int AddTag(Tag tag)
        {
            _blogContext.Add(tag);
            _blogContext.Commit();
            return tag.Id;
        }
        
        public void EditTag(Tag tag)
        {
            _blogContext.Update(tag);
            _blogContext.Commit();
        }

        public void DeleteTag(int id)
        {
            _blogContext.Delete<Tag>(id);
            _blogContext.Commit();
        }

        //public void SaveChanges()
        //{
        //    _blogContext.SaveChanges();
        //}





        //#region IDisposable
        //private bool _disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this._disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    this._disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //#endregion

    }
}
