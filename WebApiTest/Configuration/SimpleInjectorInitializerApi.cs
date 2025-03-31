using System.Collections.Generic;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace WebApiTest.Configuration
{

    public static class SimpleInjectorInitializerApi
    {

        public static Container Initialize(IAppBuilder app, Container container)
        {
            container = GetInitializeContainer(app, container);
            container.Verify(VerificationOption.VerifyAndDiagnose);
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            return container;
        }

        public static Container GetInitializeContainer(IAppBuilder app, Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.RegisterInstance<IAppBuilder>(app);
            container.Register(() => new OwinContext(new Dictionary<string, object>()).Authentication, Lifestyle.Scoped);
            return container;
        }
    }
}