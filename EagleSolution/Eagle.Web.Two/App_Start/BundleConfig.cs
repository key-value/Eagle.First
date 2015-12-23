using System.Web;
using System.Web.Optimization;

namespace Eagle.Web.Two
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.4.0.js",
                "~/Scripts/knockout.mapping-latest.js",
                "~/Scripts/knockout.viewmodel.2.0.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/Amaze").Include(
                "~/Scripts/Amaze/amazeui.min.js").Include(
                "~/Scripts/Amaze/app.js"));


            bundles.Add(new StyleBundle("~/Content/amazeui").Include(
                      "~/Content/themes/css/amazeui.min.css").Include(
                      "~/Content/themes/css/admin.css"));


        }
    }
}
