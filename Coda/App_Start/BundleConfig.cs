﻿using System.Web;
using System.Web.Optimization;

namespace Coda
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                       "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/bundles/rating").Include(
                     "~/Scripts/bootstrap-rating*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      ////"~/Content/bootstrap.css",
                      "~/Content/bootstrap-cyborg.css",
                      "~/Content/font-awesome.css",
                      "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;
            bundles.UseCdn = true;
        }
    }
}
