using Sample.Domain.Persistence;
using SEOP.Framework.Config;
using SEOP.Framework.IoC;
using SEOP.IoC.WebApi;
using System.Web.Http;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Sample.App_Start.IoCWebApiActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Sample.App_Start.IoCWebApiActivator), "Shutdown")]

namespace Sample.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET</summary>
    public static class IoCWebApiActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            var resolver = new HierarchicalDependencyResolver(IoCConfig.GetConfiguredContainer());
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = IoCConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}
