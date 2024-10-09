// -----------------------------------------------------------------------
// <copyright file="BuilderBase.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Builders;

/// <summary>
/// Base class for builder pattern.
/// </summary>
/// <typeparam name="T">Builder can be of any type.</typeparam>
public abstract class BuilderBase<T>
{
    /// <summary>
    /// Gets or sets instance of builder.
    /// </summary>
    required public T Instance { get; set; }

    /// <summary>
    /// Build the instance and return it.
    /// </summary>
    /// <returns>The configured instance.</returns>
    public T Build()
    {
        return Instance;
    }
}
