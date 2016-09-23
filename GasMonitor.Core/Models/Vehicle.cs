using System;
using System.Collections.Generic;
using System.Linq;

namespace GasMonitor.Core.Models
{
    public enum VehicleType : byte
    {
        Unknown,
        Car,
        Truck,
        Van,
        Minivan
    }

    public class Owner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual IList<Vehicle> Vehicles { get; set; }
    }

    public class Vehicle
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public VehicleType VehicleType { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual IList<FillUp> FillUps { get; set; }
    }

    public class FillUp
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public decimal Gallons { get; set; }
        public decimal Miles { get; set; }
        public bool PrimarilyHighway { get; set; }
        public DateTime FilledAt { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
