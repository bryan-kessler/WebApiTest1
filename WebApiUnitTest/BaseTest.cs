using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using WebApiTest;
using WebApiTest.Configuration;

namespace WebApiUnitTest
{
    [TestClass]
    public class BaseTest
    {

        protected static HttpClient HttpClient;
        protected static IWebService WebService;
        private const string _contentType = "application/json";

        [AssemblyInitialize]
        public static void Setup(TestContext context)
        {
            HttpClient = GetHttpClient<WebApiConfigurator>();
            WebService = new WebService();
        }

        protected static HttpClient GetHttpClient<T>()
        {
            TestServer testWebApp = TestServer.Create<T>();
            HttpClient testHttpClient = testWebApp.HttpClient;
            testHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentType));
            return testHttpClient;
        }

    }

    public class WebApiConfigurator
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            var container = new Container();

            SimpleInjectorInitializerApi.Initialize(app, container);
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            WebApiConfig.Register(config);
            config.Services.Replace(typeof(IAssembliesResolver), new WebApiResolver());
            app.UseWebApi(config);
        }
    }

    public class WebApiResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            var baseAssemblies = base.GetAssemblies();
            var assemblies = new List<Assembly>(baseAssemblies);
            var controllersAssembly = Assembly.Load("WebApiTest");
            baseAssemblies.Add(controllersAssembly);
            return assemblies;
        }
    }

}



