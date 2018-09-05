using Autofac;
using Autofac.Integration.WebApi;
using Lottery.Service;
using System.Reflection;
using System.Web.Http;

namespace Lottery.WebAPI.App_Start
{
    public class IocConfig
    {
        public static IContainer Container;

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

            //builder.RegisterModule(new ServiceModule());

            return builder.Build();
        }
    }
}