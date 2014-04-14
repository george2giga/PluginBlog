using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PluginBlog.Core.Entities;
using PluginBlog.Core.Repositories;

namespace PluginBlog.Core.Tests
{
    [TestFixture]
    public class Class1
    {

        //[Test]
        //public void CreateDbTables()
        //{
        //    // Set the initializer here
        //    Database.SetInitializer(new BlogContextInitializer());
        //    var context = new BlogContext();
        //    context.Database.Initialize(false);
        //}

        //[Test]
        //public void GetAllPosts()
        //{
        //    var blogRepo = new BlogRepository(new BlogContext());
        //    var posts = blogRepo.AllPosts().ToList();
        //    string e = "";

        //}

        //[Test]
        //public void GetOnePost()
        //{
        //    var blogRepo = new BlogRepository(new BlogContext());
        //    var posts = blogRepo.Post(1);
        //    string e = "";

        //}

        //[Test]
        //public void GetAllTags()
        //{
        //    var blogRepo = new BlogRepository(new BlogContext());
        //    var tags = blogRepo.Tags().OrderByDescending(x => x.Id).ToList();
        //    string e = "";

        //}

        //[Test]
        //public void UpdateOneCategory()
        //{
        //    var context = new BlogContext();
        //    var post = context.Find<Post>(16);
        //    post.Category = context.Find<Category>(2);
        //    //var blogRepo = new BlogRepository(new BlogContext());
        //    //var post = blogRepo.Post(16);
        //    //post.Category = blogRepo.Category(1);
        //    //blogRepo.EditPost(post);
        //    context.Entry(post).State = EntityState.Modified;
        //    context.SaveChanges();
        //}

        //[Test]
        //public void UpdateOneCategoryNewPost()
        //{
        //    var context = new BlogContext();
            
        //    var Newpost = new Post()
        //               {
        //                   Id = 1,
        //                   LastModified = DateTime.Now,
        //                   PostedOn = DateTime.Now,
        //                   Title = "sosdkfosf",
        //                   ShortDescription = "isodjfsfjisofj",
        //                   Description = "isodjfsfjisofj",
        //                   UrlSlug = "sdfsfs",
        //                   Meta = "sdfsfs",
        //                   Published = false,
        //                   CategoryId = 1//, Tags = new Collection<Tag>()
        //               };
        //    //Newpost.Category = context.Categories.Find(1);

        //    //context.Entry(Newpost.Tags).State = EntityState.Modified;
        //    context.Entry(Newpost).State = EntityState.Modified;
        //    context.Entry(Newpost).Reload();
        //    //Newpost.Tags.Clear();
        //    //var tag = context.Posts.Find(1).Tags;
        //    //Newpost.Tags.Add(tag);
            

        //    Newpost.Tags.Clear();

            
        //    try
        //    {
        //        context.SaveChanges();
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
        //    {
        //        Exception raise = dbEx;
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                string message = string.Format("{0}:{1}",
        //                    validationErrors.Entry.Entity.ToString(),
        //                    validationError.ErrorMessage);
        //                // raise a new exception nesting
        //                // the current instance as InnerException
        //                raise = new InvalidOperationException(message, raise);
        //            }
        //        }
        //        throw raise;
        //    }

        //}

        //[Test]
        //public void UpdateOneCategoryNewPostBis()
        //{
        //    var context = new BlogContext();

        //    var Newpost = new Post()
        //    {
        //        LastModified = DateTime.Now,
        //        PostedOn = DateTime.Now,
        //        Title = "sosdkfosf",
        //        ShortDescription = "isodjfsfjisofj",
        //        Description = "isodjfsfjisofj",
        //        UrlSlug = "sdfsfs",
        //        Meta = "sdfsfs",
        //        Published = false,
        //        CategoryId = 2
        //    };

        //    var post = context.Posts.SingleOrDefault(x=>x.Id == 1);
        //    post.CategoryId = Newpost.CategoryId;
        //    var firstTag = context.Find<Tag>(4);
        //    if(!post.Tags.Any(x=>x.Id == firstTag.Id))
        //        post.Tags.Add(firstTag);

        //    //Newpost.Category = context.Categories.Find(1);
        //    //context.Entry(Newpost.Tags).State = EntityState.Modified;
        //    //context.Entry(Newpost).State = EntityState.Modified;
        //    //Newpost.Tags.Clear();
        //    //var tag = context.Posts.Find(1).Tags;
        //    //Newpost.Tags.Add(tag);


        //    //Newpost.Tags.Clear();


        //    try
        //    {
        //        context.SaveChanges();
        //    }
        //    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
        //    {
        //        Exception raise = dbEx;
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                string message = string.Format("{0}:{1}",
        //                    validationErrors.Entry.Entity.ToString(),
        //                    validationError.ErrorMessage);
        //                // raise a new exception nesting
        //                // the current instance as InnerException
        //                raise = new InvalidOperationException(message, raise);
        //            }
        //        }
        //        throw raise;
        //    }

        //}
    }
}
