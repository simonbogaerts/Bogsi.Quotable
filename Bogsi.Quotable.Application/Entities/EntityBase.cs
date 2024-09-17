namespace Bogsi.Quotable.Application.Entities;

public interface IAuditableEntity 
{
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}

public abstract record EntityBase : IAuditableEntity
{
    public int Id { get; init; }
    public required Guid PublicId { get; init; }
    public DateTime Created { get; init; }
    public DateTime Updated { get; init; }
}
