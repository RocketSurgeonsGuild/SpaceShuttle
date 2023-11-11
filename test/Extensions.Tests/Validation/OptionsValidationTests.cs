#if NET6_0_OR_GREATER
using DryIoc;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Testing;
using Rocket.Surgery.Extensions.Testing;
using Rocket.Surgery.LaunchPad.Foundation;

namespace Extensions.Tests.Validation;

[UsesVerify]
public class OptionsValidationTests(ITestOutputHelper outputHelper) : AutoFakeTest(outputHelper), IAsyncLifetime
{
    [Fact]
    public async Task Should_Validate_Options_And_Throw()
    {
        var a = () => Container.Resolve<IOptions<Options>>().Value;
        var failures = a.Should().Throw<OptionsValidationException>()
                        .Which.Failures;
        await Verify(failures);
    }

    [Fact]
    public void Should_Validate_Options_And_Pass()
    {
        var services = new ServiceCollection();
        services.AddOptions<Options>().Configure(
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
    public async Task Should_Validate_Options_And_Throw_If_Out_Of_Bounds()
    {
        var services = new ServiceCollection();
        services.AddOptions<Options>().Configure(
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
        var failures = a.Should().Throw<OptionsValidationException>()
                        .Which.Failures;
        await Verify(failures);
    }

    private class Options
    {
        public string? String { get; set; }
        public int Int { get; set; }
        public bool Bool { get; set; }
        public double Double { get; set; }

        private class Validator : AbstractValidator<Options>
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

    public Task InitializeAsync()
    {
        var conventionContextBuilder = ConventionContextBuilder.Create()
                                                               .ForTesting(DependencyContext.Load(GetType().Assembly)!, LoggerFactory)
                                                               .Set(
                                                                    new FoundationOptions
                                                                    {
                                                                        RegisterValidationOptionsAsHealthChecks = false
                                                                    }
                                                                )
                                                               .WithLogger(Logger);

        var context = ConventionContext.From(conventionContextBuilder);
        Populate(new ServiceCollection().ApplyConventions(context));

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
#endif
