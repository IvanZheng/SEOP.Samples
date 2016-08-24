using System;
using SEOP.Framework.Config;
using SEOP.Framework.IoC;
using Sample.Domain.Persistence;

namespace Sample.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class IoCConfig
    {
        #region Unity Container
        private static Lazy<IContainer> container = new Lazy<IContainer>(() =>
        {
            Configuration.Instance
                         //.UseAutofacContainer()
                         //.RegisterAssemblyTypes("Sample.Web", "Sample.Application")
                         .UseUnityContainer()
                         .UseUnityMvc()
                         .RegisterCommonComponents();
            
            var container = IoCFactory.Instance.CurrentContainer;
            RegisterTypes(container, Lifetime.Hierarchical);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        private static Lazy<IContainer> mvcContainer = new Lazy<IContainer>(() =>
        {
            var container = GetConfiguredContainer().CreateChildContainer();
            RegisterTypes(container, Lifetime.PerRequest);
            return container;
        });

        public static IContainer GetMvcConfiguredContainer()
        {
           return mvcContainer.Value;
        }



        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Container allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IContainer container, Lifetime lifetime)
        {
            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            Configuration.Instance.RegisterEntityFrameworkComponents(container, lifetime);
            container.RegisterType<ISampleRepository, SampleRepository>(lifetime);
            container.RegisterType<SampleContext, SampleContext>(lifetime);
        }
    }
}
