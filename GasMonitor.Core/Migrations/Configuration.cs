using GasMonitor.Core.Models;

namespace GasMonitor.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<GasMonitor.Core.Database.GasMonitorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GasMonitor.Core.Database.GasMonitorContext context)
        {
        }
    }
}
