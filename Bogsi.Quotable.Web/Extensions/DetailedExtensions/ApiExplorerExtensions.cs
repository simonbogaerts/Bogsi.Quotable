// -----------------------------------------------------------------------
// <copyright file="ApiExplorerExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Asp.Versioning;

/// <summary>
/// Extensions regarding api explorer and versioning.
/// </summary>
internal static class ApiExplorerExtensions
{
    /// <summary>
    /// Configure and api explorer and add versioning..
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddApiExplorerWithVersioning(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
    }
}
