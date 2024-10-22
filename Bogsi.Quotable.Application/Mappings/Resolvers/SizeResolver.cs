// -----------------------------------------------------------------------
// <copyright file="SizeResolver.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes;

/// <summary>
/// Resolver to validate and set size.
/// </summary>
internal sealed class SizeResolver : IValueResolver<GetQuotesParameters, GetQuotesQuery, int>
{
    /// <summary>
    /// The method called by the resolver.
    /// </summary>
    /// <param name="source">Source you want to map from.</param>
    /// <param name="destination">Destination you want to map to.</param>
    /// <param name="destMember">Member you want to map.</param>
    /// <param name="context">Context of the mapping.</param>
    /// <returns>A validated or default size.</returns>
    public int Resolve(
        GetQuotesParameters source,
        GetQuotesQuery destination,
        int destMember,
        ResolutionContext context)
    {
        if (source.Size == null || source.Size < Constants.Size.Minimum)
        {
            return Constants.Size.Default;
        }

        if (source.Size > Constants.Size.Maximum)
        {
            return Constants.Size.Maximum;
        }

        return source.Size!.Value;
    }
}
