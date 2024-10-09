// <copyright file="CursorResolver.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes;

/// <summary>
/// Resolver to validate and set cursor.
/// </summary>
internal sealed class CursorResolver : IValueResolver<GetQuotesParameters, GetQuotesHandlerRequest, int>
{
    /// <summary>
    /// The method called by the resolver.
    /// </summary>
    /// <param name="source">Source you want to map from.</param>
    /// <param name="destination">Destination you want to map to.</param>
    /// <param name="destMember">Member you want to map.</param>
    /// <param name="context">Context of the mapping.</param>
    /// <returns>A validated or default cursor.</returns>
    public int Resolve(
        GetQuotesParameters source,
        GetQuotesHandlerRequest destination,
        int destMember,
        ResolutionContext context)
    {
        if (source.Cursor == null || source.Cursor < Constants.Cursor.Minimum)
        {
            return Constants.Cursor.Default;
        }

        return source.Cursor!.Value;
    }
}
