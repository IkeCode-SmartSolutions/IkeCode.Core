namespace IkeCode.Core.IoC
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Installers;
    using System.Reflection;
    using System.Web.Http;
    using System.Linq;
    using System.Web.Http.Dispatcher;

    public class IkeCodeWindsor
    {
        public static WindsorContainer ApiInitializer(HttpConfiguration httpConfiguration, Assembly assembly, params IWindsorInstaller[] installers)
        {
            var container = new WindsorContainer();
            var parsedInstallers = installers.ToList();
            parsedInstallers.Add(new IkeCodeWindsorApiControllerInstaller(assembly));

            container.Install(parsedInstallers.ToArray());

            httpConfiguration.DependencyResolver = new IkeCodeWindsorHttpDependencyResolver(container.Kernel);
            httpConfiguration.Services.Replace(typeof(IHttpControllerActivator), new IkeCodeWindsorHttpControllerActivator(container));

            return container;
        }
    }
}
