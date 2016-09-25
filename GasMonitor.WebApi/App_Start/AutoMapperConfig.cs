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
            cfg.CreateMap<Owner, OwnerWithVehicles>();
            cfg.CreateMap<CreateOwnerCommand, Owner>();

            cfg.CreateMap<Vehicle, VehicleViewModel>();
            cfg.CreateMap<CreateVehicleCommand, Vehicle>();
            cfg.CreateMap<VehiclePatchCommand, Vehicle>()
                .ForMember(v => v.Name, opt => opt.Condition(src => src.Name != null))
                .ForMember(v => v.VehicleType, opt => opt.Condition(src => src.VehicleType != null));

            cfg.CreateMap<FillUp, FillUpViewModel>();
            cfg.CreateMap<CreateFillUpCommand, FillUp>();
        }
    }
}