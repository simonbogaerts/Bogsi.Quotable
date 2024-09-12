namespace Bogsi.Quotable.Application.Models;

public sealed record Quote : ModelBase
{
    public required string Value { get; init; }
}
