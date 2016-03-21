using System.Web.Optimization;

namespace App.Client
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bc)
        {
            bc.Add(new StyleBundle("~/css/app").Include(
                "~/Content/bs-theme-" + AppConfig.BootstrapTheme + ".css",
                "~/Content/app.css",
                "~/Content/font-awesome.css"));

            bc.Add(new StyleBundle("~/css/chart").Include(
                "~/Content/morris.css"));

            bc.Add(new StyleBundle("~/css/util").Include(
               "~/Content/bootstrap-select.css"));

            bc.Add(new ScriptBundle("~/script/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive-ajax*",
                "~/Scripts/jquery-ui-*"));

            bc.Add(new ScriptBundle("~/script/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bc.Add(new ScriptBundle("~/script/util").Include(
                "~/Scripts/handlebars.js",
                "~/Scripts/mustache.js",
                "~/Scripts/bootstrap-select.js",
                "~/Scripts/moment.js"));

            bc.Add(new ScriptBundle("~/script/chart").Include(
                "~/Scripts/raphael.js",
                "~/Scripts/chart.js",
                "~/Scripts/morris.js"));

            bc.Add(new ScriptBundle("~/script/app").Include(
                "~/Scripts/App/*.js")); 
        }
    }
}
