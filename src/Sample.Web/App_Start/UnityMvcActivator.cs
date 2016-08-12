using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;
using SEOP.Unity;
using SEOP.Framework.Config;
using SEOP.Framework.IoC;
using Sample.Domain.Persistence;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Sample.App_Start.UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Sample.App_Start.UnityWebActivator), "Shutdown")]

namespace Sample.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
        /// <summary>Integrates Unity when the application starts.</summary>
        static IContainer _container;
        public static void Start() 
        {
            _container = IoCConfig.GetMvcConfiguredContainer();
          

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(_container.GetUnityContainer()));

            DependencyResolver.SetResolver(new SEOP.Unity.Mvc.UnityDependencyResolver(_container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            _container?.Dispose();
        }
    }
}