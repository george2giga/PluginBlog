using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PluginBlog.Core.Entities;

namespace PluginBlog.Web.Areas.BlogAdmin.Models
{
    public class EditPost
    {
        public Post Post { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public int[] SelectedTags { get; set; }

        [Required(ErrorMessage = "Category: Field is required")]
        public int SelectedCategory { get; set; } 
    }
}