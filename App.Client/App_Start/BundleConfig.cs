using System.Web.Optimization;

namespace App.Client
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundleCollection)
        {
            bundleCollection.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive-ajax*",
                "~/Scripts/jquery-ui-*"));

            bundleCollection.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundleCollection.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/App/*.js",
                "~/Scripts/Chart.js",
                "~/Scripts/Chart.js"));

            bundleCollection.Add(new ScriptBundle("~/bundles/kendo").Include(
                "~/Scripts/KendoUi/kendo.ui.core.js"));

            bundleCollection.Add(new StyleBundle("~/Content/app").Include(
                "~/Content/Themes/Bootstrap/" + AppConfig.BootstrapTheme + "/bs-theme.css",
                "~/Content/Themes/KendoUi/Web/kendo.common.core.css",
                "~/Content/Themes/KendoUi/Web/kendo." + AppConfig.KendoTheme + ".css",
                "~/Content/Themes/Bootstrap/" + AppConfig.BootstrapTheme + "/app.css",
                "~/Content/Themes/Bootstrap/" + AppConfig.BootstrapTheme + "/morris.css",
                "~/Content/font-awesome.css"));

        }
    }
}
