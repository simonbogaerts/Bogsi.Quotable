namespace Bogsi.Quotable.Application;

public sealed record Constants
{
    public sealed record Cursor
    {
        public const int None = 0;
        public const int Minimum = 1;
        public const int Default = 1;
        public const int Offset = 1;
    }

    public sealed record Size
    {
        public const int Minimum = 1;
        public const int Maximum = 20;
        public const int Default = 10;
    }

    public sealed record Quote
    {
        public sealed record Properties
        {
            public sealed record Value
            {
                public const int MinimumLength = 5;
                public const int MaximumLength = 1255;
            }
        }
    }
}
