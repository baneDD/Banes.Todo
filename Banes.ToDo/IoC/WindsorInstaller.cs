using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Banes.ToDo.Repositories;
using System;
using System.Configuration;

namespace Banes.ToDo.IoC
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<LoggingFacility>(f => f.UseLog4Net(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile))
                     .Register(Component.For<IToDoTaskRepository>().ImplementedBy<ToDoTaskRepository>().LifestylePerWebRequest()
                            .DependsOn(Dependency.OnValue("databaseName", ConfigurationManager.AppSettings["LiteDbConnectionString"]))
                            .DependsOn(Dependency.OnValue("collectionName", ConfigurationManager.AppSettings["LiteDbCollection"])));
        }
    }
}