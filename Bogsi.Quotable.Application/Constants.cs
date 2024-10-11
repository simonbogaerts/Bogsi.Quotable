// -----------------------------------------------------------------------
// <copyright file="Constants.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Application;

/// <summary>
/// Constants used in the Application Layer.
/// </summary>
public sealed record Constants
{
    /// <summary>
    /// Static values regarding cursor.
    /// </summary>
    public sealed record Cursor
    {
        /// <summary>
        /// Gets none or empty value.
        /// </summary>
        public const int None = 0;

        /// <summary>
        /// Gets minimum value.
        /// </summary>
        public const int Minimum = 1;

        /// <summary>
        /// Gets default value.
        /// </summary>
        public const int Default = 1;

        /// <summary>
        /// Gets offset (required for calculating new cursor).
        /// </summary>
        public const int Offset = 1;
    }

    /// <summary>
    /// Static values regarding size.
    /// </summary>
    public sealed record Size
    {
        /// <summary>
        /// Gets minimum value.
        /// </summary>
        public const int Minimum = 1;

        /// <summary>
        /// Gets maximum value.
        /// </summary>
        public const int Maximum = 20;

        /// <summary>
        /// Gets default value.
        /// </summary>
        public const int Default = 10;
    }

    /// <summary>
    /// Static values used in Quote models.
    /// </summary>
    public sealed record Quote
    {
        /// <summary>
        /// Static values regarding specific properties.
        /// </summary>
        public sealed record Properties
        {
            /// <summary>
            /// Static values regarding the Value property of the Quote model.
            /// </summary>
            public sealed record Value
            {
                /// <summary>
                /// Gets minimum length value.
                /// </summary>
                public const int MinimumLength = 5;

                /// <summary>
                /// Gets maximum lentgh value.
                /// </summary>
                public const int MaximumLength = 1255;
            }
        }
    }
}
