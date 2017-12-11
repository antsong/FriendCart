using System.Web;
using System.Web.Optimization;

namespace AntWork
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //Admin js
            bundles.Add(new ScriptBundle("~/bundles/admin/index").
                Include("~/Content/admin/jquery/jquery.min.js").
                Include("~/Content/admin/bootstrap/js/bootstrap.min.js").
                Include("~/Content/admin/metisMenu/metisMenu.min.js").
                Include("~/Content/admin/raphael/raphael.min.js").
                Include("~/Content/admin/morrisjs/morris.min.js").
                Include("~/Scripts/data/morris-data.js").
                Include("~/Content/admin/dist/js/sb-admin-2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/table").
                Include("~/Content/admin/jquery/jquery.min.js").
                Include("~/Content/admin/bootstrap/js/bootstrap.min.js").
                Include("~/Content/admin/metisMenu/metisMenu.min.js").
                Include("~/Content/admin/datatables/js/jquery.dataTables.min.js").
                Include("~/Content/admin/datatables-plugins/*.js").
                Include("~/Content/admin/datatables-responsive/*.js").
                Include("~/Content/admin/dist/js/sb-admin-2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/uielement").
                Include("~/Content/admin/jquery/jquery.min.js").
                Include("~/Content/admin/bootstrap/js/bootstrap.min.js").
                Include("~/Content/admin/metisMenu/metisMenu.min.js").
                Include("~/Content/admin/webuploader/webuploader.min.js").
                Include("~/Content/admin/dist/js/sb-admin-2.min.js"));

            //Admin js
            bundles.Add(new ScriptBundle("~/bundles/admin/flot").
                Include("~/Content/admin/jquery/jquery.min.js").
                Include("~/Content/admin/bootstrap/js/bootstrap.min.js").
                Include("~/Content/admin/metisMenu/metisMenu.min.js").

                Include("~/Content/admin/flot/excanvas.min.js").
                Include("~/Content/admin/flot/jquery.flot.js").
                Include("~/Content/admin/flot/jquery.flot.pie.js").
                Include("~/Content/admin/flot/jquery.flot.resize.js").
                Include("~/Content/admin/flot/jquery.flot.time.js").
                Include("~/Content/admin/flot-tooltip/jquery.flot.tooltip.min.js").
                Include("~/Scripts/data/flot-data.js").

                Include("~/Content/admin/dist/js/sb-admin-2.min.js"));



            //Admin css
            bundles.Add(new StyleBundle("~/Content/admin").
                Include("~/Content/admin/bootstrap/css/bootstrap.min.css").
                Include("~/Content/admin/metisMenu/metisMenu.min.css").
                Include("~/Content/admin/datatables-plugins/*.css").
                Include("~/Content/admin/datatables-responsive/*.css").
                Include("~/Content/admin/dist/css/sb-admin.min.css").
                Include("~/Content/admin/morrisjs/*.css").
                Include("~/Content/admin/font-awesome/css/*.css").
                Include("~/Content/admin/webuploader/webuploader.css"));
        }
    }
}
