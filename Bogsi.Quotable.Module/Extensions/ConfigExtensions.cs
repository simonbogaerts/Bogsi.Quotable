// -----------------------------------------------------------------------
// <copyright file="ConfigExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Modules.Extensions;

using Bogsi.Quotable.Common.Configs;
using Bogsi.Quotable.Common.Enums;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Add and configure all the config singletons.
/// </summary>
public static class ConfigExtensions
{
    /// <summary>
    /// Configure the AuthConfig and add to service collection as singleton.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    /// <param name="options">Options when adding singleton to IserviceCollection.</param>
    /// <returns>AuthConfig based upon appsettings.</returns>
    public static AuthConfig GetOrAddAuthConfig(this WebApplicationBuilder builder, ServiceCollectionOptions options)
    {
        ArgumentNullException.ThrowIfNull(builder);

        AuthConfig config = builder.GetOrAddConfig<AuthConfig>(
            Common.Constants.AppSettingSections.Auth,
            options);

        return config;
    }

    /// <summary>
    /// Configure the QuotableDbConfig and add to service collection as singleton.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    /// <param name="options">Options when adding singleton to IserviceCollection.</param>
    /// <returns>QuotableDbConfig based upon appsettings.</returns>
    public static QuotableDbConfig GetOrAddQuotableDbConfig(this WebApplicationBuilder builder, ServiceCollectionOptions options)
    {
        ArgumentNullException.ThrowIfNull(builder);

        QuotableDbConfig config = builder.GetOrAddConfig<QuotableDbConfig>(
            Common.Constants.AppSettingSections.QuotableDb,
            options);

        return config;
    }

    /// <summary>
    /// Configure the MassTransitConfig and add to service collection as singleton.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    /// <param name="options">Options when adding singleton to IserviceCollection.</param>
    /// <returns>MassTransitConfig based upon appsettings.</returns>
    public static MassTransitConfig GetOrAddMassTransitConfig(this WebApplicationBuilder builder, ServiceCollectionOptions options)
    {
        ArgumentNullException.ThrowIfNull(builder);

        MassTransitConfig config = builder.GetOrAddConfig<MassTransitConfig>(
            Common.Constants.AppSettingSections.Messaging,
            options);

        return config;
    }

    /// <summary>
    /// Configure the ValkeyConfig and add to service collection as singleton.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    /// <param name="options">Options when adding singleton to IserviceCollection.</param>
    /// <returns>ValkeyConfig based upon appsettings.</returns>
    public static ValkeyConfig GetOrAddValkeyConfig(this WebApplicationBuilder builder, ServiceCollectionOptions options)
    {
        ArgumentNullException.ThrowIfNull(builder);

        ValkeyConfig config = builder.GetOrAddConfig<ValkeyConfig>(
            Common.Constants.AppSettingSections.Valkey,
            options);

        return config;
    }

    /// <summary>
    /// Configure all config singletons and add them to the service collection.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureConfigSingletons(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.GetOrAddAuthConfig(ServiceCollectionOptions.AddAndReturn);
        builder.GetOrAddQuotableDbConfig(ServiceCollectionOptions.AddAndReturn);
        builder.GetOrAddMassTransitConfig(ServiceCollectionOptions.AddAndReturn);
        builder.GetOrAddValkeyConfig(ServiceCollectionOptions.AddAndReturn);
    }

    /// <summary>
    /// Test.
    /// </summary>
    /// <typeparam name="T">this WebApplicationBuilder builder, ServiceCollectionOptions options..</typeparam>
    /// <param name="builder">egv'ztevae.</param>
    /// <param name="section">zgftva'vt.</param>
    /// <param name="options">a"f'a"vr.</param>
    /// <returns>f"arc  "rv.</returns>
    private static T GetOrAddConfig<T>(
        this WebApplicationBuilder builder,
        string section,
        ServiceCollectionOptions options)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(builder);

        T? config = builder
            .Configuration
            .GetSection(section)
            .Get<T>();

        ArgumentNullException.ThrowIfNull(config);

        if (options is ServiceCollectionOptions.AddAndReturn)
        {
            builder.Services.AddSingleton(config);
        }

        return config;
    }
}
