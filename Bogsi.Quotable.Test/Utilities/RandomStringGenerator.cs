// -----------------------------------------------------------------------
// <copyright file="RandomStringGenerator.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Test.Utilities;

/// <summary>
/// Utility tool to generate a random string of a certain length.
/// </summary>
public sealed class RandomStringGenerator
{
    /// <summary>
    /// Generate a string of a random lentgh.
    /// </summary>
    /// <param name="length">Required length.</param>
    /// <returns>The  random string.</returns>
    public static string GenerateRandomString(int length)
    {
        return string.Join(string.Empty, Enumerable.Repeat(0, length).Select(n => (char)new Random().Next(32, 127)));
    }
}
