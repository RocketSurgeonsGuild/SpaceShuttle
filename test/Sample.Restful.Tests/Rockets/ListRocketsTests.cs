﻿using System.Threading.Tasks;
using Bogus;
using Sample.Core;
using Sample.Core.Domain;
using Xunit;
using Xunit.Abstractions;

namespace Sample.Restful.Tests.Rockets;

public class ListRocketsTests : HandleWebHostBase
{
    [Fact]
    public async Task Should_List_Rockets()
    {
        var client = new RocketClient(Factory.CreateClient());
        await ServiceProvider.WithScoped<RocketDbContext>()
                             .Invoke(
                                  async z =>
                                  {
                                      var faker = new RocketFaker();
                                      z.AddRange(faker.Generate(10));

                                      await z.SaveChangesAsync();
                                  }
                              );

        var response = await client.ListRocketsAsync();

        response.Result.Should().HaveCount(10);
    }

    public ListRocketsTests(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    private static readonly Faker Faker = new Faker();
}
