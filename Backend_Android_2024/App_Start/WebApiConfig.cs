using Newtonsoft.Json.Serialization;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Backend_Android_2024
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            try
            {
                var c = new SwaggerEnabledConfiguration(GlobalConfiguration.Configuration, s => "", new List<string>());
                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failure");
                Console.WriteLine(ex.Message);
            }
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            // configure json formatter
            JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;

            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }


    }
}
