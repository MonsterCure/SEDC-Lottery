using Lottery.Data;
using Lottery.Data.Model;
using Lottery.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Lottery.RaffleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = Configure();
            var lotteryManager = serviceProvider.GetService<ILotteryManager>();
            var configuration = serviceProvider.GetService<IConfigurationRoot>();
            var finalRaffle = DateTime.Parse(configuration.GetSection("FinalRaffle").Value);

            if(DateTime.Now <= finalRaffle)
            {
                lotteryManager.GiveAwards(RaffledType.PerDay);
            }
            else if(DateTime.Now == finalRaffle)
            {
                lotteryManager.GiveAwards(RaffledType.Final);
            }
        }

        static IServiceProvider Configure()
        {
            //set "Copy to Output Directory" property of appsettings.json to "Copy Always"
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var serviceProvider = new ServiceCollection() //registering the dependencies in the container
                .AddSingleton(provider => configuration)
                .AddSingleton<DbContext, LotteryContext>()
                .AddSingleton<ILotteryManager, LotteryManager>()
                .AddSingleton(typeof(IRepository<>), typeof(Repository<>))
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
