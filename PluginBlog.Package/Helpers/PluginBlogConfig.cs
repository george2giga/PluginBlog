using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PluginBlog.Core;
using PluginBlog.Core.Repositories;

namespace PluginBlog.Web.Helpers
{
    public class PluginBlogConfig
    {
        ///// <summary>
        ///// Globally enable/disable the edit functionality for the admin are. To override it register a custom function for RegisterAuthorization
        ///// </summary>
        //public static bool AuthorizeAll { get; set; }


        /// <summary>
        /// Lookup for a registered repository, if not present return the BlogRepository
        /// </summary>
        /// <returns>Repository instance</returns>
        public static IBlogRepository GetRepository()
        {
            var repository = RegisterRepository() ?? new BlogRepository(new BlogContext());

            return repository;
        }

        /// <summary>
        /// Execute the AuthorizeAccess function.
        /// </summary>
        /// <returns>true if user is authorized</returns>
        public static IAuthProvider GetAuthorization()
        {
            return RegisterAuthorization == null ? new SampleAuthProvider() : RegisterAuthorization();
        }

        /// <summary>
        /// Consumer defined function used to register the repository (note: if none is defined the BlogRepository class will be used)
        /// </summary>
        public static Func<IBlogRepository> RegisterRepository { get; set; }

        /// <summary>
        /// Consumer defined function to authorize editing access.
        /// </summary>
        public static Func<IAuthProvider> RegisterAuthorization { get; set; }
    
    }
}