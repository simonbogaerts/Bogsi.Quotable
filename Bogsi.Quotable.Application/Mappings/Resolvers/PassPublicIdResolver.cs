// -----------------------------------------------------------------------
// <copyright file="PassPublicIdResolver.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application.Mappings.Resolvers;

using AutoMapper;

/// <summary>
/// Resolver to pass a public id (Guid) from options while mapping.
/// </summary>
public sealed class PassPublicIdResolver : IValueResolver<object, object, Guid>
{
    /// <summary>
    /// The method called by the resolver.
    /// </summary>
    /// <param name="source">Source you want to map from.</param>
    /// <param name="destination">Destination you want to map to.</param>
    /// <param name="destMember">Member you want to map.</param>
    /// <param name="context">Context of the mapping.</param>
    /// <returns>A new public id (Guid).</returns>
    public Guid Resolve(object source, object destination, Guid destMember, ResolutionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var publicId = Guid.Parse(context.Items["Id"].ToString()!);

        return publicId;
    }
}
