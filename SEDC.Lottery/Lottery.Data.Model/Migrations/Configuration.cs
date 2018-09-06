using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace Lottery.Data.Model.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LotteryContext>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LotteryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var codes = new List<Code>
            {
                new Code { CodeValue = "CC8899", IsWinning = true },
                new Code { CodeValue = "CC7799", IsWinning = false },
                new Code { CodeValue = "CD7439", IsWinning = true },
                new Code { CodeValue = "NS3299", IsWinning = false },
                new Code { CodeValue = "BK7909", IsWinning = false },
                new Code { CodeValue = "CXC34X", IsWinning = false },
                new Code { CodeValue = "YY4SCX", IsWinning = false },
                new Code { CodeValue = "ZZ22ZZ", IsWinning = false },
                new Code { CodeValue = "TT0012", IsWinning = false },
                new Code { CodeValue = "Z1Z2Z3", IsWinning = false },
                new Code { CodeValue = "CT4321", IsWinning = true }
            };

            context.Codes.AddRange(codes); //AddRange is faster than a for loop with the codes and AddOrUpdate for each of them, because it only makes one connection, it is only called once

            var awards = new List<Award>
            {
                new Award { AwardName = "Beer", AwardDescription = "You won a beer!", AwardQuantity = 100, RaffledType = (byte) RaffledType.Immediate },
                new Award { AwardName = "iPhoneX", AwardDescription = "You won an iPhoneX!", AwardQuantity = 20, RaffledType = (byte) RaffledType.PerDay },
                new Award { AwardName = "Volkswagen Polo", AwardDescription = "You won a Volkswagen Polo!", AwardQuantity = 2, RaffledType = (byte) RaffledType.Final }
            };

            context.Awards.AddRange(awards);

            context.SaveChanges();
        }
    }
}
