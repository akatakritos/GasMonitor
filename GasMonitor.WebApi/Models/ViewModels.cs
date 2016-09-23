using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using GasMonitor.Core.Models;

namespace GasMonitor.WebApi.Models
{
    public class OwnerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateOwnerCommand
    {
        public string Name { get; set; }
    }

    public class CreateVehicleCommand
    {
        public string Name { get; set; }
        public VehicleType VehicleType { get; set; }
    }

    public class VehicleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}