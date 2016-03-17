
namespace IkeCode.Core.IoC
{
    using Castle.Windsor;
    using System;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    public class IkeCodeWindsorHttpControllerActivator : IHttpControllerActivator
    {
        private readonly IWindsorContainer container;

        public IkeCodeWindsorHttpControllerActivator(IWindsorContainer container)
        {
            this.container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var resolved = container.Resolve(controllerType);
            var controller = resolved as IHttpController;
            request.RegisterForDispose(new Release(() => container.Release(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release)
            {
                this.release = release;
            }

            public void Dispose()
            {
                release();
            }
        }
    }
}
