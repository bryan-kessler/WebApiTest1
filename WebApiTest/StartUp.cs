using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiTest.Configuration;

namespace WebApiTest
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var container = new Container();

            // Initialize SimpleInjector
            SimpleInjectorInitializerApi.Initialize(app, container);
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            // Register Web API routes
            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }

    }
}

