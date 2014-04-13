using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginBlog.Core.Entities;

namespace PluginBlog.Core
{
    public interface IBlogContext
    {
        IQueryable<Post> Posts { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Tag> Tags { get; }
        void Add<T>(T entity) where T: BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        void Delete<T>(int id) where T : BaseEntity;
        T Find<T>(int id) where T : BaseEntity;
        void Commit();
    }

    public class BlogContext : DbContext, IBlogContext
    {
        public BlogContext(): base("PluginBlogConnection")
        {
            
        }

        //http://stackoverflow.com/questions/8881387/mapping-join-tables-in-the-entity-framework
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Post>().HasMany(u => u.Tags).WithMany(x => x.Posts).Map(m =>
            //{
            //    m.ToTable("PostTagMap");
            //    m.MapLeftKey("Post_Id");
            //    m.MapRightKey("Tag_Id");
            //});
            
            //modelBuilder.Entity<Post>().HasOptional(x => x.Category);

            //modelBuilder.Entity<Category>().HasOptional(x => x.Posts);
        }

        public DbSet<Post> PostsDbSet { get; set; }
        public DbSet<Category> CategoriesDbSet { get; set; }
        public DbSet<Tag> TagsDbSet { get; set; }

        //public IQueryable<T> NotTracked<T>(IQueryable<T> query) where T : class {
        //    return this.Set<T>().AsNoTracking();
        //}

        public IQueryable<Post> Posts
        {
            get { return PostsDbSet; }
        }

        public IQueryable<Category> Categories
        {
            get { return CategoriesDbSet; }
        }

        public IQueryable<Tag> Tags
        {
            get { return TagsDbSet; }
        }

        public void Add<T>(T entity) where T: BaseEntity
        {
            this.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            this.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete<T>(int id) where T : BaseEntity
        {
            var entity = this.Set<T>().Find(id);
            this.Set<T>().Remove(entity);
        }

        public T Find<T>(int id) where T : BaseEntity
        {
            var entity = this.Set<T>().Find(id);
            return entity;
        }

        public void Commit()
        {
            this.SaveChanges();
        }


    }
}
