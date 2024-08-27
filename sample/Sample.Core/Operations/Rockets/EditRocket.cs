﻿using FluentValidation;
using MediatR;
using Riok.Mapperly.Abstractions;
using Rocket.Surgery.LaunchPad.Foundation;
using Rocket.Surgery.LaunchPad.Mapping.Profiles;
using Sample.Core.Domain;
using Sample.Core.Models;

namespace Sample.Core.Operations.Rockets;

[PublicAPI, Mapper]
[UseStaticMapper(typeof(NodaTimeMapper))]
[UseStaticMapper(typeof(ModelMapper))]
[UseStaticMapper(typeof(StandardMapper))]
public static partial class EditRocket
{
    /// <summary>
    ///     The edit operation to update a rocket
    /// </summary>
    public record Request : IRequest<RocketModel>
    {
        /// <summary>
        ///     The rocket id
        /// </summary>
        public RocketId Id { get; init; }

        /// <summary>
        ///     The serial number of the rocket
        /// </summary>
        public string SerialNumber { get; set; } = null!; // TODO: Make generator that can be used to create a writable view model

        /// <summary>
        ///     The type of the rocket
        /// </summary>
        public RocketType Type { get; set; } // TODO: Make generator that can be used to create a writable view model
    }

    public partial record PatchRequest : IRequest<RocketModel>, IPropertyTracking<Request>
    {
        /// <summary>
        ///     The rocket id
        /// </summary>
        public RocketId Id { get; init; }
    }

    private class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty()
               .NotNull();

            RuleFor(x => x.Type)
               .NotNull()
               .IsInEnum();

            RuleFor(x => x.SerialNumber)
               .NotNull()
               .MinimumLength(10)
               .MaximumLength(30);
        }
    }

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    private static partial ReadyRocket Map(Request request);
    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    public static partial Request MapRequest(ReadyRocket model);
    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    [MapProperty(nameof(@RocketModel.Sn), nameof(@Request.SerialNumber))]
    public static partial Request MapRequest(RocketModel model);
    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    private static partial void Map(Request request, ReadyRocket record);

    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    private static Request Map(PatchRequest request, ReadyRocket rocket) => request.ApplyChanges(MapRequest(rocket));

    private class RequestHandler(RocketDbContext dbContext, IMediator mediator)
        : PatchRequestHandler<Request, PatchRequest, RocketModel>(mediator), IRequestHandler<Request, RocketModel>
    {
        private async Task<ReadyRocket> GetRocket(RocketId id, CancellationToken cancellationToken)
        {
            return await dbContext
                        .Rockets.FindAsync([id,], cancellationToken)
                        .ConfigureAwait(false)
             ?? throw new NotFoundException();
        }

        protected override async Task<Request> GetRequest(PatchRequest patchRequest, CancellationToken cancellationToken)
            => Map(patchRequest, await GetRocket(patchRequest.Id, cancellationToken));

        public async Task<RocketModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var rocket = await GetRocket(request.Id, cancellationToken);
            if (rocket == null) throw new NotFoundException();

            Map(request, rocket);
            dbContext.Update(rocket);
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return ModelMapper.Map(rocket);
        }
    }
}
