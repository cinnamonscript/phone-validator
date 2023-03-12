using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhoneValidatorAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            // Route for downloading CSV file
            routes.MapRoute(
                name: "DownloadCsvFile",
                url: "api/download-csv-file/{phoneNumber}/{countryCode}",
                defaults: new { controller = "ValuesController", action = "DownloadCsvFile" }
            );
        }
    }
}
