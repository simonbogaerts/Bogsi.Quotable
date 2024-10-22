// -----------------------------------------------------------------------
// <copyright file="MediatrExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Bogsi.Quotable.Application;

/// <summary>
/// Extensions regarding mediatr.
/// </summary>
internal static class MediatrExtensions
{
    /// <summary>
    /// Configure RabbitMQ and MassTransit.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureMediatr(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IApplicationMarker).Assembly));
    }
}
