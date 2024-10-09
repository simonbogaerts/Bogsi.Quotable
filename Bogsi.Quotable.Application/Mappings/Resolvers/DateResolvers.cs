// <copyright file="DateResolvers.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

using AutoMapper;

using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Models;

/// <summary>
/// Resolver to maintain the destination's Created date.
/// </summary>
public sealed class CreatedDateResolver : IValueResolver<Quote, QuoteEntity, DateTime>
{
    /// <summary>
    /// The method called by the resolver.
    /// </summary>
    /// <param name="source">Source you want to map from.</param>
    /// <param name="destination">Destination you want to map to.</param>
    /// <param name="destMember">Member you want to map.</param>
    /// <param name="context">Context of the mapping.</param>
    /// <returns>Created date of destination.</returns>
    public DateTime Resolve(Quote source, QuoteEntity destination, DateTime destMember, ResolutionContext context)
    {
        ArgumentNullException.ThrowIfNull(destination);

        return destination.Created;
    }
}

/// <summary>
/// Resolver to maintain the destination's Updated date.
/// </summary>
public sealed class UpdatedDateResolver : IValueResolver<Quote, QuoteEntity, DateTime>
{
    /// <summary>
    /// The method called by the resolver.
    /// </summary>
    /// <param name="source">Source you want to map from.</param>
    /// <param name="destination">Destination you want to map to.</param>
    /// <param name="destMember">Member you want to map.</param>
    /// <param name="context">Context of the mapping.</param>
    /// <returns>Updated date of destination.</returns>
    public DateTime Resolve(Quote source, QuoteEntity destination, DateTime destMember, ResolutionContext context)
    {
        ArgumentNullException.ThrowIfNull(destination);

        return destination.Updated;
    }
}
