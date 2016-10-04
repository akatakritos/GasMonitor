using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using GasMonitor.Core.Models;

namespace GasMonitor.Core.Database
{
    public class GasMonitorContext : DbContext
    {
        public GasMonitorContext()
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<FillUp> FillUps { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.HasDefaultSchema("gas");

            builder.Entity<Owner>().HasKey(m => m.Id);
            builder.Entity<Owner>().HasMany(m => m.Vehicles);

            builder.Entity<Vehicle>().HasKey(m => m.Id);
            builder.Entity<Vehicle>().HasMany(m => m.FillUps);
            builder.Entity<Vehicle>().HasRequired(m => m.Owner);
            builder.Entity<Vehicle>().Property(m => m.VehicleType).HasColumnType("tinyint");

            builder.Entity<FillUp>().HasKey(m => m.Id);
            builder.Entity<FillUp>().HasRequired(m => m.Vehicle);

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync()
        {
            var entities = ChangeTracker.Entries<TimestampedEntity>();

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedAt = DateTime.UtcNow;
                        entity.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
