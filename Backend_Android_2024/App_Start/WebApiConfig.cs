using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Headers;
using System;

namespace Backend_Android_2024
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Cấu hình CORS: http://localhost:57790
            var cors = new EnableCorsAttribute("*", "*", "*");
            


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
           .Add(new RequestHeaderMapping("Accept",
                             "text/html",
                             StringComparison.InvariantCultureIgnoreCase,
                             true,
                             "application/json"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = "Get" }
            );

            // Cấu hình routes
            config.MapHttpAttributeRoutes();
            config.EnableCors(cors);

            // Cấu hình JSON formatter
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
