using System.Web;
using System.Web.Optimization;

namespace PeruTourism
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/layout.css",
                      "~/Content/css/home.min.css",
                      "~/Content/css/propuestas.min.css",
                      "~/Content/css/chat.css",
                      "~/Content/css/flotante.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                     "~/Scripts/third/bootstrap.min.js",
                     "~/Scripts/shared/chat.js",
                     "~/Scripts/shared/flotante.js"));
            
            //BundleTable.EnableOptimizations = true;
        }
    }
}
