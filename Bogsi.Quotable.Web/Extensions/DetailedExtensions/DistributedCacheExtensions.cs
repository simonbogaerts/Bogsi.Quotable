// -----------------------------------------------------------------------
// <copyright file="DistributedCacheExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

/// <summary>
/// Extensions regarding database connections.
/// </summary>
internal static class DistributedCacheExtensions
{
    /// <summary>
    /// Configure and add Valkey support.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddDistributedCache(this WebApplicationBuilder builder)
    {
        string? connectionstring = builder.Configuration.GetConnectionString(Constants.ConnectionStrings.Valkey);

        ArgumentNullException.ThrowIfNullOrWhiteSpace(connectionstring);

        builder.Services.AddStackExchangeRedisCache(options => options.Configuration = connectionstring);
    }
}
