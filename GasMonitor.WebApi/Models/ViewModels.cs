﻿using System;
using System.Collections.Generic;
using System.Linq;

using GasMonitor.Core.Models;

namespace GasMonitor.WebApi.Models
{
    public class OwnerViewModel
    {
        /// <summary>
        ///     The unique identifier for the owner
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The name of the vehicle owner
        /// </summary>
        public string Name { get; set; }
    }

    public class OwnerWithVehicles : OwnerViewModel
    {
        /// <summary>
        ///     The vehicles this owner has
        /// </summary>
        public IEnumerable<VehicleViewModel> Vehicles { get; set; }
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

    public class VehiclePatchCommand
    {
        public string Name { get; set; }
        public VehicleType? VehicleType { get; set; }
    }

    public class VehicleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public VehicleType VehicleType { get; set; }
    }

    public class VehicleWithStats : VehicleViewModel
    {
        public VehicleStats Stats { get; set; }
    }

    public class VehicleStats
    {
        public decimal TotalMiles { get; set; }
        public decimal TotalsGallons { get; set; }
        public int NumberOfFillups { get; set; }
        public decimal AverageMilesPerGallon => TotalsGallons == 0 ? 0 : TotalMiles / TotalsGallons;
    }

    public class FillUpViewModel
    {
        public Guid Id { get; set; }
        public decimal Gallons { get; set; }
        public decimal Miles { get; set; }
        public bool PrimarilyHighway { get; set; }
        public DateTime FilledAt { get; set; }
    }

    public class CreateFillUpCommand
    {
        public decimal Gallons { get; set; }
        public decimal Miles { get; set; }
        public bool PrimarilyHighway { get; set; }
        public DateTime? FilledAt { get; set; }
    }

    public class Status
    {
        public TimeSpan Uptime { get; set; }
        public string Version { get; set; }
        public string Commit { get; set; }
    }
}