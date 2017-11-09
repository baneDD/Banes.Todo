using Castle.Windsor;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Banes.ToDo.IoC
{
    public class WindsorCompositionRoot : IHttpControllerActivator
    {
        private readonly IWindsorContainer Container;

        public WindsorCompositionRoot(IWindsorContainer container)
        {
            Container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (IHttpController)Container.Resolve(controllerType);

            request.RegisterForDispose(new ContainerCleanup(() => Container.Release(controller)));

            return controller;
        }

        private sealed class ContainerCleanup : IDisposable
        {
            private readonly Action Release;

            public ContainerCleanup(Action release)
            {
                Release = release;
            }

            public void Dispose() => Release();
        }
    }
}