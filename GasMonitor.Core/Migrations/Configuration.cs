using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GasMonitor.Core.Migrations
{
    using System;
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