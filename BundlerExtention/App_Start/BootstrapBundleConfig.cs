using System;
using System.Collections.Generic;
using System.Web.Optimization;

using BundlerExtention.Extentions;


[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(BundlerExtention.App_Start.BootstrapBundleConfig), "RegisterBundles")]

namespace BundlerExtention.App_Start
{
	public class BootstrapBundleConfig
	{
    //    public static void RegisterBundles()
    //    {
    //        // Add @Styles.Render("~/Content/bootstrap") in the <head/> of your _Layout.cshtml view
    //        // For Bootstrap theme add @Styles.Render("~/Content/bootstrap-theme") in the <head/> of your _Layout.cshtml view
    //        // Add @Scripts.Render("~/bundles/bootstrap") after jQuery in your _Layout.cshtml view
    //        // When <compilation debug="true" />, MVC4 will render the full readable version. When set to <compilation debug="false" />, the minified version will be rendered automatically
    //        BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
    //        BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
    //        BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap-theme").Include("~/Content/bootstrap-theme.css"));
    //    }
    //}
   public static void RegisterBundles(BundleCollection bundles) {
            // jQuery
            //bundles.RegisterBundleWithCdnFallback(
            //        bundlePath: "~/bundles/scripts/jquery",
            //        cdnPath: "//cdnjs.cloudflare.com/ajax/libs/jquery/2.0.1/jquery.min.js",
            //        libPath: "./Content/js/lib/jquery-2.1.1.js",
            //        fallbackExpression: "window.jquery");


            var extendedBundleList = new List<ExtendedBundleObject>();
            extendedBundleList.Add(new ExtendedBundleObject {
                                        CdnPath= "//cdnjs.cloudflare.com/ajax/libs/jquery/2.0.1/jquery.min.js",
                                        BundleType = BundleType.Script,
                                        FallbackExpression = "window.jQuery",
                                        VirtualPath = "~/bundles/scripts/jquery",
                                        LocalPath = "~/Content/js/lib/jquery-2.1.1.js"});

            extendedBundleList.Add(new ExtendedBundleObject {
                CdnPath = "//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js",
                BundleType = BundleType.Script,
                FallbackExpression = "$().button === 'function' ",
                VirtualPath = "~/bundles/scripts/bootstrap",
                LocalPath = "~/Content/lib/bootstrap-3.2.0-dist/js/bootstrap.js"
            });

            extendedBundleList.Add(new ExtendedBundleObject {
                CdnPath ="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css",
                BundleType = BundleType.Style,
                VirtualPath = "~/bundles/styles/bootstrap",
                LocalPath = "~/Content/lib/bootstrap-3.2.0-dist/css/*.css"
            });


            
            bundles.BundleCollectionInitilizationExtention(extendedBundleList);


            var customLoadOrder = new List<String> {
                                                       "jquery.js", "jquery.min.js", "jquery-*", "jquery.*", "jquery.validate",
                                                       "bootstrap.js", "bootstrap.min.js", "bootstrap.css","bootstrap.min.css",
                                                       "bootstrap-datepicker.js", "bootstrap-datepicker.min.js",
                                                       "bootstrap-datepicker.css","bootstrap-datepicker.min.css",
                                                       "font-awesome.css","font-awesome.min.css"
                                                   };

            bundles.BundleCollectionLoadOrderExtention(customLoadOrder);


            //bundles.RegisterBundleWithCdnFallback(
            //        bundlePath: "~/bundles/styles/bootstrap",
            //        cdnPath: "//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css",
            //        libPath: "~/Content/lib/bootstrap-3.2.0-dist/css/*.css");

            //bundles.RegisterBundleWithCdnFallback(
            //        bundlePath: "~/bundles/scripts/bootstrap",
            //        cdnPath: "//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js",
            //        libPath: "~/Content/lib/bootstrap-3.2.0-dist/js/bootstrap",
            //        fallbackExpression: "$().button === 'function' ");



            //bundles.Add(
            //    new ScriptBundle(
            //        virtualPath: "~/bundles/scripts/jquery",
            //        cdnPath: "//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.1/jquery.min.js") {
            //            CdnFallbackExpression = "window.jQuery"
            //        }
            //        .Include("~/Content/js/lib/jquery-2.1.1.js")

            //    );
            // Font-Awesome 

            //bundles.RegisterBundleWithCdnFallback(
            //        bundlePath: "~/bundles/styles/font-awesome",
            //        cdnPath: "//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.1.0/css/font-awesome.min.css",
            //        libPath: "./Content/lib/font-awesome-4.1.0/css/font-awesome.css");


            //// Bootstrap
            //bundles.Add(
            //    new ScriptBundle(
            //        virtualPath: "~/bundles/scripts/bootstrap",
            //        cdnPath: "//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css") {
            //            CdnFallbackExpression = "$().button === 'function' "
            //        }
            //        .Include("~/Content/lib/bootstrap-3.2.0-dist/js/bootstrap"));

            //bundles.Add(new StyleBundle(
            //        virtualPath:"~/bundles/styles/bootstrap",
            //        cdnPath: "//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css"
            //      ).Include("~/Content/lib/bootstrap-3.2.0-dist/css/*.css"));


            //// Bootstrap Datepicker
            //bundles.RegisterBundleWithCdnFallback(
            //        bundlePath: "~/bundles/styles/bootstrap-datepicker",
            //        cdnPath: "//cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/css/datepicker.min.css",
            //        libPath: "~/Content/lib/datepicker/css/date3picker.css");

            //bundles.RegisterBundleWithCdnFallback(
            //    bundlePath: "~/bundles/styles/bootstrap-datepicker",
            //    cdnPath: "//cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.min.js",
            //    libPath: "~/Content/lib/datepicker/js/bootstrap-datepicker");



            BundleTable.EnableOptimizations = true;

        }
    }

