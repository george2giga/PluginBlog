using System.Linq;
using PluginBlog.Core.Entities;

namespace PluginBlog.Core.Repositories
{
    public interface IBlogRepository
    {
        IQueryable<Post> AllPosts();
        IQueryable<Post> Posts(bool published = true);
        IQueryable<Post> PostsForTag(string tagSlug);
        IQueryable<Post> PostsForCategory(string categorySlug);
        IQueryable<Post> AllPostsForSearch(string search);
        IQueryable<Post> PostsForSearch(string search);
        Post Post(int year, int month, string titleSlug);
        Post Post(int id);
        int AddPost(Post post);
        void EditPost(Post post);
        void DeletePost(int id);
        IQueryable<Category> Categories();
        Category Category(string categorySlug);
        Category Category(int id);
        int AddCategory(Category category);
        void EditCategory(Category category);
        void DeleteCategory(int id);
        IQueryable<Tag> Tags();
        int TotalTags();
        Tag Tag(string tagSlug);
        Tag Tag(int id);
        int AddTag(Tag tag);
        void EditTag(Tag tag);
        void DeleteTag(int id);
    }
}