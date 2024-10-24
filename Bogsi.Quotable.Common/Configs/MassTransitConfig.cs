// -----------------------------------------------------------------------
// <copyright file="MassTransitConfig.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Common.Configs;

/// <summary>
/// Contains all the info for MassTransit and RabbitMQ.
/// </summary>
public sealed record MassTransitConfig
{
    /// <summary>
    /// Gets the address of the message queue.
    /// </summary>
    required public string Address { get; init; }

    /// <summary>
    /// Gets the port of the message queue.
    /// </summary>
    required public int Port { get; init; }

    /// <summary>
    /// Gets the username of the message queue.
    /// </summary>
    required public string UserName { get; init; }

    /// <summary>
    /// Gets the password of the message queue.
    /// </summary>
    required public string Password { get; init; }

    /// <summary>
    /// Gets the host address of the message queue.
    /// </summary>
    public string Host => $"amqp://{Address}:{Port}";

    /// <summary>
    /// Gets the connectionstring of the message queue.
    /// </summary>
    public string ConnectionString => $"amqp://{UserName}:{Password}@{Address}:{Port}";
}
