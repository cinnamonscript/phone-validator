﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PhoneValidatorAPI.Controllers;

namespace PhoneValidatorAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DownloadCsv",
                routeTemplate: "api/downloadcsv/{downloadId}",
                defaults: new { controller = "Values", action = "DownloadCsv" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
