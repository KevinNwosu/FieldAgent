using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL
{
    public enum FactoryMode
    {
        TEST,
        LIVE
    }
    public class DBFactory
    {
        private readonly IConfigurationRoot Config;
        private readonly FactoryMode Mode;

        public DBFactory(IConfigurationRoot config, FactoryMode mode = FactoryMode.LIVE)
        {
            Config = config;
            Mode = mode;
        }

        public AppDbContext GetDbContext()
        {
            string environment = Mode == FactoryMode.TEST ? "Test" : "Prod";

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(Config[$"ConnectionStrings:{environment}"])
                .Options;
            return new AppDbContext(options);
        }
    }
}
