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
    public class Post : BaseEntity
    {
        /// <summary>
        /// The heading of the post.
        /// </summary>
        [Required(ErrorMessage = "Title: Field is required")]
        [StringLength(500, ErrorMessage = "Title: Length should not exceed 500 characters")]
        public virtual string Title
        { get; set; }

        /// <summary>
        /// A brief paragraph about the post.
        /// </summary>
        [Required(ErrorMessage = "ShortDescription: Field is required")]
        public virtual string ShortDescription
        { get; set; }

        /// <summary>
        /// The complete post content.
        /// </summary>
        [Required(ErrorMessage = "Description: Field is required")]
        public virtual string Description
        { get; set; }

        /// <summary>
        /// The information about the post that has to be displayed in the &lt;meta&gt; tag (SEO).
        /// </summary>
        /// <remarks>
        /// Not sure Google still uses this for ranking but other search providers might be.
        /// </remarks>
        [Required(ErrorMessage = "Meta: Field is required")]
        [StringLength(1000, ErrorMessage = "Meta: Length should not exceed 1000 characters")]
        public virtual string Meta
        { get; set; }

        /// <summary>
        /// The url slug that is used to define the post address.
        /// </summary>
        [Required(ErrorMessage = "Meta: Field is required")]
        [StringLength(1000, ErrorMessage = "Meta: UrlSlug should not exceed 50 characters")]
        public virtual string UrlSlug
        { get; set; }

        /// <summary>
        /// Flag to represent whether the article is published or not.
        /// </summary>
        public virtual bool Published
        { get; set; }

        /// <summary>
        /// The post published date.
        /// </summary>
        [Required(ErrorMessage = "PostedOn: Field is required")]
        public virtual DateTime PostedOn
        { get; set; }

        /// <summary>
        /// The post's last modified date.
        /// </summary>
        public virtual DateTime? Modified
        { get; set; }

        /// <summary>
        /// The category to which the post belongs to.
        /// </summary>
        public virtual Category Category
        { get; set; }

        /// <summary>
        /// Collection of tags labelled over the post.
        /// </summary>
        public virtual IList<Tag> Tags
        { get; set; }
    }
}
