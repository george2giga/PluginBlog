using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginBlog.Entities
{
    /// <summary>
    /// Represents a category that contains group of blog posts.
    /// </summary>
    public class Tag : BaseEntity
    {
        [Required(ErrorMessage = "Name: Field is required")]
        [StringLength(500, ErrorMessage = "Name: Length should not exceed 500 characters")]
        public virtual string Name
        { get; set; }

        [Required(ErrorMessage = "UrlSlug: Field is required")]
        [StringLength(500, ErrorMessage = "UrlSlug: Length should not exceed 500 characters")]
        public virtual string UrlSlug
        { get; set; }

        public virtual string Description
        { get; set; }
        
        public virtual IList<Post> Posts
        { get; set; }
    }
}
