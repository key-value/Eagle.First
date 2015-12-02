using System.Web;
using System.Web.Optimization;

namespace Eagle.Web.Two
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/font").Include(
                "~/Content/themes/xenon/fonts/font-awesome.min.css",
                "~/Content/themes/xenon/fonts/linecons.css")
                );

            bundles.Add(new StyleBundle("~/Content/xenon-core").Include(
                "~/Content/themes/xenon/xenon-core.css",
                "~/Content/themes/xenon/xenon-forms.css",
                "~/Content/themes/xenon/xenon-components.css",
                "~/Content/themes/xenon/xenon-skins.css",
                "~/Content/themes/xenon/custom.css")
                );


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

            bundles.Add(new ScriptBundle("~/bundles/reactJs").Include(
                "~/Scripts/react/react-with-addons.min.js",
                "~/Scripts/react/react-dom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-3.4.0.js",
                "~/Scripts/knockout.mapping-latest.js",
                "~/Scripts/knockout.viewmodel.2.0.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/xenon-core").Include(
                "~/Scripts/xenon/TweenMax.min.js",
                "~/Scripts/xenon/resizeable.js",
                "~/Scripts/xenon/xenon-api.js",
                //"~/Scripts/xenon/xenon-notes.js",
                "~/Scripts/xenon/xenon-toggles.js",
                "~/Scripts/xenon/xenon-widgets.js",
                "~/Scripts/xenon/toastr.min.js",
                "~/Scripts/xenon/xenon-custom.js"));
        }
    }
}
