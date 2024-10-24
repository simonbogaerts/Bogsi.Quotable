// -----------------------------------------------------------------------
// <copyright file="MessagingExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Modules.Extensions;

using Bogsi.Quotable.Application;
using Bogsi.Quotable.Common.Enums;
using Bogsi.Quotable.Persistence;

using MassTransit;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions regarding messaging.
/// </summary>
internal static class MessagingExtensions
{
    /// <summary>
    /// Configure RabbitMQ and MassTransit.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureMediatr(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
    }

    /// <summary>
    /// Configure RabbitMQ and MassTransit.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureMassTransit(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var config = builder.GetOrAddMassTransitConfig(ServiceCollectionOptions.Return);

        builder.Services.AddMassTransit(massTransitConfig =>
        {
            massTransitConfig.SetKebabCaseEndpointNameFormatter();

            massTransitConfig.AddAndConfigureConsumers();
            massTransitConfig.AddAndConfigureSagas();

            massTransitConfig.UsingRabbitMq((context, rabbitMqConfig) =>
            {
                rabbitMqConfig.Host(new Uri(config.Host), host =>
                {
                    host.Username(config.UserName);
                    host.Password(config.Password);
                });

                rabbitMqConfig.UseInMemoryOutbox(context);

                rabbitMqConfig.ConfigureEndpoints(context);
            });
        });
    }

    /// <summary>
    /// Add the consumers to the mass transit configurator.
    /// </summary>
    /// <param name="config">The configurator.</param>
    private static void AddAndConfigureConsumers(this IBusRegistrationConfigurator config)
    {
        config.AddConsumers(typeof(IApplicationMarker).Assembly);
    }

    /// <summary>
    /// Add the sagas to the mass transit configurator.
    /// </summary>
    /// <param name="config">The configurator.</param>
    private static void AddAndConfigureSagas(this IBusRegistrationConfigurator config)
    {
        config.AddSagaStateMachines(typeof(IApplicationMarker).Assembly);

        config.SetEntityFrameworkSagaRepositoryProvider(r =>
        {
            r.ExistingDbContext<SagaContext>();

            r.UsePostgres();

            r.ConcurrencyMode = ConcurrencyMode.Optimistic;
        });
    }
}
