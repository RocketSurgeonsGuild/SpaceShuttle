﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rocket.Surgery.LaunchPad.EntityFramework;
using Sample.Core.Models;

namespace Sample.Core.Domain;

/// <summary>
///     A rocket in inventory
/// </summary>
public class ReadyRocket // : IReadyRocket
{
    public RocketId Id { get; set; }
    public string SerialNumber { get; set; } = null!;
    public RocketType Type { get; set; }

    public IEnumerable<LaunchRecord> LaunchRecords { get; set; } = null!;

    private class EntityConfiguration : IEntityTypeConfiguration<ReadyRocket>
    {
        public void Configure(EntityTypeBuilder<ReadyRocket> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(z => z.Id)
                   .ValueGeneratedOnAdd()
                   .HasValueGenerator(StronglyTypedIdValueGenerator.Create(RocketId.New));
            builder.ToTable("Rockets");
        }
    }
}

public interface IReadyRocket
{
    RocketId Id { get; set; }
    string SerialNumber { get; set; }
    RocketType Type { get; set; }
}
