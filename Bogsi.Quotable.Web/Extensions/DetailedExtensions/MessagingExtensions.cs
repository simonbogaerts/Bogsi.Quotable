// -----------------------------------------------------------------------
// <copyright file="MessagingExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Bogsi.Quotable.Application;
using Bogsi.Quotable.Application.Sagas;

using MassTransit;

/// <summary>
/// Extensions regarding message queue.
/// </summary>
internal static class MessagingExtensions
{
    /// <summary>
    /// Configure RabbitMQ and MassTransit.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureMessageQueue(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(massTransitConfig =>
        {
            massTransitConfig.SetKebabCaseEndpointNameFormatter();

            massTransitConfig.AddAndConfigureSagasAndConsumers();

            massTransitConfig.UsingRabbitMq((context, rabbitMqConfig) =>
            {
                var host = builder.Configuration["MessageBroker:Host"];
                var username = builder.Configuration["MessageBroker:Username"];
                var password = builder.Configuration["MessageBroker:Password"];

                ArgumentNullException.ThrowIfNullOrWhiteSpace(nameof(host));
                ArgumentNullException.ThrowIfNullOrWhiteSpace(nameof(username));
                ArgumentNullException.ThrowIfNullOrWhiteSpace(nameof(password));

                rabbitMqConfig.Host(new Uri(host!), host =>
                {
                    host.Username(username!);
                    host.Password(password!);
                });

                rabbitMqConfig.UseInMemoryOutbox(context);

                rabbitMqConfig.ConfigureEndpoints(context);
            });
        });
    }

    /// <summary>
    /// Add the sagas and consumers to the mass transit configurator.
    /// </summary>
    /// <param name="config">The configurator.</param>
    private static void AddAndConfigureSagasAndConsumers(this IBusRegistrationConfigurator config)
    {
        config.AddConsumers(typeof(IApplicationMarker).Assembly);

        config.AddSagaStateMachines(typeof(IApplicationMarker).Assembly);

        config.SetInMemorySagaRepositoryProvider();
    }
}
