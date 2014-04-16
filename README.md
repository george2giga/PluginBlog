PluginBlog
==========
A simple MVC5 blog for existing sites.
==========
The package adds a *BlogAdmin* area to an existing web project, providing CRUD methods for posts/categories/tags.
An example of a *BlogController* is also added to show/list posts.

For a demo visit http://pluginblog.apphb.com/.


----------
> **Download instructions:**
> 
> - Download the [Nuget][1] package using **PM> Install-Package PluginBlog**  
> - Create a database according to the PluginBlog connection string in the web.config and use the script.sql file in the App_Data folder to create the tables .
> - If the hosting site already uses authentication then remove from the web.config the one added by the plugin. For more details check the Setup Guide below.
> - Login to the admin area: **yoursite/blogadmin/auth/login** (default credentials in the config) and start adding new posts. (see [<i class="icon-share"></i> Synchronization](#synchronization) section).

Setup
---------

**PluginBlog** adds a *PluginBootstrapper* class in the hosting App_Start folder, in here you can setup your DI, change the routing and eventually change the default WYSIWYG, below the default one:

```C#

public class PluginBlogBootstrapper
{
	public static void Start()
	{
		//Register a custom repository by implementing IBlogRepository 
		PluginBlogConfig.RegisterRepository = () => new BlogRepository(new BlogContext());

		//Register the authorization used to access the BlogAdminArea
		PluginBlogConfig.RegisterAuthorization = () =>
		{
			var authProvider = new SampleAuthProvider();
			return authProvider;
		};
	}

	//After App_Start is executed register the bundles
	public static void PostStart()
	{
		BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
		BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap", "http://netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.js").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));
		BundleTable.Bundles.Add(new ScriptBundle("~/bundles/tinymce", "http://tinymce.cachefly.net/4.0/tinymce.min.js").Include("~/Scripts/tinymce/tinymce*"));
		BundleTable.Bundles.Add(new ScriptBundle("~/bundles/pluginblog").Include("~/Scripts/pluginblog.js"));
		BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap","http://netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.css").Include("~/Content/bootstrap.css"));
		BundleTable.Bundles.Add(new StyleBundle("~/Content/pluginblog").Include("~/Content/pluginblog.css"));
		BundleTable.EnableOptimizations = true;
	}
}
```
#### <i class="icon-cog"></i> Start

The two functions register respectively the repository and the authorization for the blog. You can replace (delete) them with your own DI framework, both IAuthProvider and *IBlogRepository* are passed via constructor to the blog controllers.
To replace the default authentication, create a new implementation of *IAuthProvider* and replace *SampleAuthProvider*.

#### <i class="icon-cog"></i> PostStart
After the start, client dependencies are registered. By default the [TinyMce][1] wysiwyg is used along with [Bootstrap][2] for the layout. 


  [1]: http://docs.nuget.org/docs/start-here/installing-nuget
