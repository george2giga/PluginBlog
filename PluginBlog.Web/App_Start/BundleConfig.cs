using System.Web;
using System.Web.Optimization;

namespace PluginBlog.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

        ////    bundles.Add(new ScriptBundle("~/bundles/fontawsome", "http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.min.css").Include(
        ////"~/Scripts/tinymce/tinymce.min.js"));

        //    bundles.Add(new ScriptBundle("~/bundles/tinymce", "http://tinymce.cachefly.net/4.0/tinymce.min.js").Include(
        //            "~/Scripts/tinymce/tinymce.min.js"));

        //    bundles.Add(new ScriptBundle("~/bundles/pluginblog").Include(
        //              "~/Scripts/pluginblog.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            
        }
    }
}
