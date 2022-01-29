﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Rocket.Surgery.DependencyInjection;
using Sample.Core.Domain;

namespace Sample.Restful.Tests;

internal class SqliteConnectionService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public SqliteConnectionService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _serviceProvider.WithScoped<RocketDbContext>()
                              .Invoke(z => z.Database.EnsureCreatedAsync(cancellationToken))
                              .ConfigureAwait(false);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
