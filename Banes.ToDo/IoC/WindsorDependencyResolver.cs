using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Banes.ToDo.IoC
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer Container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            Container = container;
        }

        public IDependencyScope BeginScope() => new WindsorDependencyScope(Container);

        public object GetService(Type serviceType) => Container.Kernel.HasComponent(serviceType) ? Container.Resolve(serviceType) : null;

        public IEnumerable<object> GetServices(Type serviceType) => (!Container.Kernel.HasComponent(serviceType)) ? new object[0] : Container.ResolveAll(serviceType).Cast<object>();

        public void Dispose() => Container.Dispose();
    }

    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer Container;
        private readonly IDisposable Scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            Container = container;
            Scope = container.BeginScope();
        }

        public object GetService(Type serviceType) => (Container.Kernel.HasComponent(serviceType)) ? Container.Resolve(serviceType) : null;

        public IEnumerable<object> GetServices(Type serviceType) => Container.ResolveAll(serviceType).Cast<object>();

        public void Dispose() => Scope.Dispose();
    }

    public class ApiControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store) => container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestylePerWebRequest());
    }
}