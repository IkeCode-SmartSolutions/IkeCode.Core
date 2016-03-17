namespace IkeCode.Core.IoC.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using System.Reflection;
    using System.Web.Http.Controllers;
    public class IkeCodeWindsorApiControllerInstaller : IWindsorInstaller
    {
        Assembly _assembly;

        public IkeCodeWindsorApiControllerInstaller(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssembly(_assembly).BasedOn<IHttpController>().LifestylePerWebRequest());
        }
    }
}
