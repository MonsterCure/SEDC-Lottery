using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lottery.Data.Model
{
    public class LotteryContext : DbContext
    {
        private readonly IConfigurationRoot _configuration;

        public LotteryContext(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Code> Codes { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<UserCode> UserCodes { get; set; }
        public DbSet<UserCodeAward> UserCodeAward { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("LotteryDatabase")); //connection string to base
            //needs Microsoft.EntityFrameworkCore.SqlServer package
            //searches the connection strings in appsettings.json, which will be passed as congiguration to the constructor, and tries to find the specified string
        }
    }
}
