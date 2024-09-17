namespace Bogsi.Quotable.Application;

public sealed record Constants
{
    public sealed record PageNumber
    {
        public const int Minimum = 1;
        public const int Default = 1;
    }

    public sealed record PageSize
    {
        public const int Minimum = 1;
        public const int Maximum = 20;
        public const int Default = 10;
    }

    public sealed record QuoteProperties
    {
        public sealed record Value
        {
            public const int MinimumLength = 5;
            public const int MaximumLength = 1255;
        }
    }
}
