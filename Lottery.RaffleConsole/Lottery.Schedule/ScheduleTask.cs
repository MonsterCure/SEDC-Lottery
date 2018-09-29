using Lottery.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Schedule
{
    public class ScheduleTask : ScheduledProcessor
    {
        private readonly ILotteryManager _lotteryManager;

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory, ILotteryManager lotteryManager) : base (serviceScopeFactory)
        {
            _lotteryManager = lotteryManager;
        }

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            try
            {
                Console.WriteLine($"Raffle started at {DateTime.Now:dd.MM.yyy HH:mm:ss}");
                _lotteryManager.Raffle();
                Console.WriteLine($"Raffle finished at {DateTime.Now: dd.MM.yyyy HH:mm:ss}");
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }

        protected override string Schedule => "* * * * *";

    }
}
