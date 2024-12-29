﻿using AutoMapper;
using BiddingApp.Domain.Models.Entities;
using BiddingApp.Infrastructure.Dtos.BiddingDtos;
using BiddingApp.Infrastructure.Dtos.BiddingSessionDtos;
using BiddingApp.Infrastructure.Dtos.UserDtos;
using BiddingApp.Infrastructure.Dtos.VehicleDtos;

namespace BiddingApp.Infrastructure.MapperConfigs
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<UserVm, User>().ReverseMap();
            CreateMap<BiddingSessionVm, BiddingSession>().ReverseMap();
            CreateMap<BiddingVm, Bidding>().ReverseMap();
            CreateMap<VehicleVm, Vehicle>().ReverseMap();
        }
    }
}