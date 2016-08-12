using Autofac.Integration.Mvc;
using SEOP.Autofac;
using System.Linq;
using System.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Sample.App_Start.AutofacWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Sample.App_Start.AutofacWebActivator), "Shutdown")]

namespace Sample.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class AutofacWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start() 
        {
            var container = IoCConfig.GetConfiguredContainer();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new AutofacFilterProvider());

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container.GetAutofacContainer()));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            //icrosoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(Autofac.Integration.Mvc.AutofacDependencyResolver
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            var container = IoCConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}