using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginBlog.Core
{
    public class BlogContextInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            var initData = Convert.ToBoolean(ConfigurationManager.AppSettings["InitData"]);
            if (initData)
            {
                //var post
            }
        }
    }
}
