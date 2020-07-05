﻿using System;
using AutoMapper;
using NodaTime;
using Sample.Core.Domain;

namespace Sample.Core.Models
{
    public class LaunchRecordModel
    {
        public Guid Id { get; set; }

        public string Partner { get; set; } = null!;
        public string Payload { get; set; } = null!;
        public long PayloadWeightKg { get; set; }
        public Instant? ActualLaunchDate { get; set; }
        public Instant ScheduledLaunchDate { get; set; }
        public string RocketSerialNumber { get; set; } = null!;
        public RocketType RocketType { get; set; }

        class Mapper : Profile
        {
            public Mapper()
            {
                CreateMap<LaunchRecord, LaunchRecordModel>();
            }
        }
    }
}