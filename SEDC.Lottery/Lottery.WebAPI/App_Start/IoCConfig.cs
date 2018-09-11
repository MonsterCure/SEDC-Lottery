using Autofac;
using Autofac.Integration.WebApi;
using Lottery.Service;
using Lottery.Service.IoC.Autofac;
using System.Reflection;
using System.Web.Http;

namespace Lottery.WebAPI.App_Start
{
    public class IocConfig
    {
        public static IContainer Container; // one container for the whole app, static

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterDependencies(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static IContainer RegisterDependencies(ContainerBuilder builder)
        {
            //Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<LotteryManager>()
                .As<ILotteryManager>()
                .InstancePerRequest();

            builder.RegisterModule(new ServiceModule()); // registering the ServiceModule from the business layer
            //one IoC container, configured here, with possible multiple modules registered to it, from all over the app

            return builder.Build();
        }
    }
}