﻿using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Rocket.Surgery.LaunchPad.EntityFramework;

namespace Sample.Core.Domain
{
    public class RocketDbContext : LpContext<RocketDbContext>
    {
        public RocketDbContext(DbContextOptions<RocketDbContext> options) : base(options) { }
        public DbSet<ReadyRocket> Rockets { get; set; } = null!;
        public DbSet<LaunchRecord> LaunchRecords { get; set; } = null!;
    }
}