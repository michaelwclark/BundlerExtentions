using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

using Microsoft.Ajax.Utilities;

using WebGrease.Css.Extensions;


namespace BundlerExtention.Extentions {
    public static class BundleCollectionExtention {

        /// <summary>
        /// Makes adding CDN fallbacks easier by adding and registering a style or script bundle to BundleCollection.  If you aren't seeing your bundles, check the debug output.
        /// </summary>
        /// <param name="bundles">Bundle Collection being used. (See BundleTable.bundles)</param>
        /// <param name="bundlePath">Virtual Path to use. Will be referenced in code to add bundler links.</param>
        /// <param name="cdnPath">Path to CDN</param>
        /// <param name="libPath">Path to local Fallback. Note this will only be used if CDN is not responding.</param>
        /// <param name="fallbackExpression">Expression to test if library has loaded.</param>
        public static void RegisterBundleWithCdnFallback(
            this BundleCollection bundles,
            String bundlePath,
            String cdnPath,
            String libPath,
            String fallbackExpression = null) {
            //bundles.Add(new Bundle(bundlePath, cdnPath){CdnFallbackExpression = fallbackExpression}.Include(libPath));
            
                bundles.UseCdn = true;

            
                var bundle = (fallbackExpression == null) ?  
                    new Bundle(bundlePath, cdnPath) : 
                    new Bundle(bundlePath, cdnPath){CdnFallbackExpression = fallbackExpression};
                

                if(Directory.Exists(libPath)) {
                    bundle.Include(libPath);
                }

                bundles.Add(bundle);
          
        }


        public static void BundleCollectionInitilizationExtention(
            this BundleCollection bundleCollection,
            IEnumerable<ExtendedBundleObject> extendedBundles) {
                    

                foreach(var bun in extendedBundles) {
                    //Setup each bundle in the Bundle Table
                    var bundle = new Bundle(bun.VirtualPath); //Setup virtual path reference for calling in Razor

                    bundle.Include(bun.LocalPath); //Will fail if path isn't found. Let it.
    
                    if(!bun.CdnPath.IsNullOrWhiteSpace()) { //Setup CDNPath and Fallback if it exists.
                        bundleCollection.UseCdn = true;
                        bundle.CdnPath = bun.CdnPath;

                        //won't have fallback without CDN Path
                        bundle.CdnFallbackExpression = bun.FallbackExpression.IsNullOrWhiteSpace()
                                                   ? null
                                                   : bun.FallbackExpression;
                    }

                    bundleCollection.Add(bundle);

                    //Was going to handle load order here, but was too conviluted to do for each one.
                    //Will be easier to process at the end of the script setup.
                }            
                bundleCollection.Add(new StyleBundle(
                            "~/bundles/styles/bootstrap",
                             "//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"
                       ).Include("~/Content/lib/bootstrap-3.2.0-dist/css/*.css"));

        }


        /// <summary>
        /// Provide a custom Load order for all dependancies. Note that this will not CLEAR out the default load orders. Lists are passed byRef so if you want to wipe the default order out, first call BundleFileSetOrdering.Clear();
        /// </summary>
        /// <param name="bundleCollection">Bundle collection you are using</param>
        /// <param name="loadOrder">List of strings ordered by proper load order for all dependancies. Can handle wildcards.</param>
        public static void BundleCollectionLoadOrderExtention(
            this BundleCollection bundleCollection, 
            List<String> loadOrder) {
                var depCount = loadOrder.Count;
                if(depCount > 0) {
                    bundleCollection.Clear(); //Clear out the existing Dep Order
                    var depSetOrder = new BundleFileSetOrdering("CustomAppDepOrder");
                    for(var i = depCount; i > 0; i--) { //count downwards for optimization.
                        var ix = depCount - i;
                        depSetOrder.Files.Add(loadOrder[ix]);
                    }
                }
                else {
                    throw new ArgumentNullException("loadOrder");
                }            
        }

    }



    public enum BundleType {
        Script=1,
        Style=2,
        Font=3,
        Config=4,
        Other=5
    }
    public enum BundleOptimizationLevel {
        None=0,
        Concat=1,
        Minify=2,
        Both=3,
        
    }

    /// <summary>
    /// Used to add dependancy ordering, cdn fallback, local versioning, and
    /// </summary>
    public class ExtendedBundleObject {
        public String CdnPath { get; set; }
        public BundleType BundleType { get; set; }
        public String FallbackExpression { get; set; }
        public String VirtualPath { get; set; }
        public String LocalPath { get; set; }
        public BundleOptimizationLevel OptimizationLevel  { get; set; }
    }
}
