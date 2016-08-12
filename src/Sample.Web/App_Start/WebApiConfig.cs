
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Sample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //传输以JSON格式
            //config.Formatters.Clear();
            //config.Formatters.Add(new JsonFormatter());
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //config.Formatters.Add(new JsonFormatter());
            //config.Formatters.Add(new StackTextJsonMediaTypeFormatter());
            //config.Formatters.Add(config.Formatters.JsonFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

           

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
