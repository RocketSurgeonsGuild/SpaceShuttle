using DryIoc;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Rocket.Surgery.Conventions;
using Rocket.Surgery.LaunchPad.Foundation;

namespace Extensions.Tests.Validation;

public class HealthCheckOptionsValidationTests(ITestOutputHelper outputHelper) : AutoFakeTest<XUnitTestContext>(XUnitDefaults.CreateTestContext(outputHelper)), IAsyncLifetime
{
    [Fact]
    public Task Should_Validate_Options_And_Throw()
    {
        var a = () => Container.Resolve<IOptions<Options>>().Value;
        a.Should().Throw<OptionsValidationException>();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Should_Validate_Options_And_Throw_If_Out_Of_Bounds()
    {
        var services = new ServiceCollection();
        services
           .AddOptions<Options>()
           .Configure(
                options =>
                {
                    options.Bool = false;
                    options.Double = 50;
                    options.Int = -50;
                    options.String = "";
                }
            );
        Populate(services);
        var a = () => Container.Resolve<IOptions<Options>>().Value;
        a.Should().Throw<OptionsValidationException>();
    }

    [Fact]
    public async Task Should_Validate_Options_And_Throw_After_Application_Has_Started()
    {
        var a = () => Container.Resolve<IOptions<Options>>().Value;
        var failures = a
                      .Should()
                      .Throw<OptionsValidationException>()
                      .Which.Failures;
        await Verify(failures);
    }

    [Fact]
    public void Should_Validate_Options_And_Pass_After_Application_Has_Started()
    {
        var services = new ServiceCollection();
        services
           .AddOptions<Options>()
           .Configure(
                options =>
                {
                    options.Bool = true;
                    options.Double = -50;
                    options.Int = 50;
                    options.String = "Hello";
                }
            );
        Populate(services);
        var a = () => Container.Resolve<IOptions<Options>>().Value;
        a.Should().NotThrow();
    }

    [Fact]
    public async Task Should_Validate_Options_And_Throw_If_Out_Of_Bounds_After_Application_Has_Started()
    {
        var services = new ServiceCollection();
        services
           .AddOptions<Options>()
           .Configure(
                options =>
                {
                    options.Bool = false;
                    options.Double = 50;
                    options.Int = -50;
                    options.String = "";
                }
            );
        Populate(services);
        var a = () => Container.Resolve<IOptions<Options>>().Value;
        var failures = a
                      .Should()
                      .Throw<OptionsValidationException>()
                      .Which.Failures;
        await Verify(failures);
    }

    private class Options
    {
        public string? String { get; set; }
        public int Int { get; set; }
        public bool Bool { get; set; }
        public double Double { get; set; }

        [UsedImplicitly]
        private sealed class Validator : AbstractValidator<Options>
        {
            public Validator()
            {
                RuleFor(z => z.String).NotEmpty().NotNull();
                RuleFor(z => z.Int).GreaterThan(0).LessThanOrEqualTo(100);
                RuleFor(z => z.Bool).NotEqual(false);
                RuleFor(z => z.Double).GreaterThanOrEqualTo(-100d).LessThanOrEqualTo(0d);
            }
        }
    }

    public async Task InitializeAsync()
    {
        var conventionContextBuilder = ConventionContextBuilder
                                      .Create(Imports.Instance)
                                      .Set(
                                           new FoundationOptions
                                           {
                                               RegisterValidationOptionsAsHealthChecks = true,
                                           }
                                       )
                                      .UseLogger(Logger);

        var context = await ConventionContext.FromAsync(conventionContextBuilder);
        Populate(await new ServiceCollection().ApplyConventionsAsync(context));
    }

    public Task DisposeAsync() => Task.CompletedTask;
}
