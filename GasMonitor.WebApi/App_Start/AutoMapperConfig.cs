using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using GasMonitor.Core.Models;
using GasMonitor.WebApi.Models;

namespace GasMonitor.WebApi
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(ConfigureMapper);
        }

        private static void ConfigureMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Owner, OwnerViewModel>();
            cfg.CreateMap<CreateOwnerCommand, Owner>();

            cfg.CreateMap<Vehicle, VehicleViewModel>();
            cfg.CreateMap<CreateVehicleCommand, Vehicle>();
        }
    }
}