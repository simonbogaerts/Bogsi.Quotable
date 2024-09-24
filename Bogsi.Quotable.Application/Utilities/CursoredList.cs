namespace Bogsi.Quotable.Application.Utilities;

public record CursorResponse<T> 
{
    public int Cursor { get; init; }
    public int Size { get; init; }
    public int Total { get; init; }
    public bool HasNext => Cursor < Total;
    public required T Data { get; init; }
}
