// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Bogsi.Quotable.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureBuilder();

var application = builder.Build();

application.ConfigureWebApplication();

await application.RunAsync().ConfigureAwait(false);

/// <summary>
/// The partial class is required for integration testing (devcontainer setup).
/// </summary>
public partial class Program
{
}
