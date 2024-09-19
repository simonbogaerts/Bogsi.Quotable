namespace Bogsi.Quotable.Application.Errors;

public sealed record QuotableError(string Code, string Description);

public sealed record QuotableErrors
{
    public static QuotableError NotFound => new (
        nameof(NotFound),
        "Nothing found for provided id");

    public static QuotableError InternalError => new(
        nameof(InternalError),
        "Something went wrong");
}
