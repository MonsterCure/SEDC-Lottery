using System.Data.Entity;
using Autofac;
using Lottery.Data;
using Lottery.Data.Model;

namespace Lottery.Service.IoC.Autofac
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LotteryContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>));
        }
    }
}
