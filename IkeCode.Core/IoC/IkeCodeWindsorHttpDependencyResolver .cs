namespace IkeCode.Core.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;
    using Castle.MicroKernel;

    public class IkeCodeWindsorHttpDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public IkeCodeWindsorHttpDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new IkeCodeWindsorDependencyScope(kernel);
        }

        public object GetService(Type type)
        {
            return kernel.HasComponent(type) ? kernel.Resolve(type) : null;
        }

        public IEnumerable<object> GetServices(Type type)
        {
            return kernel.ResolveAll(type).Cast<object>();
        }

        public void Dispose()
        {
        }
    }
}
