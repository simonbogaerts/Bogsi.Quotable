namespace Bogsi.Quotable.Test.Builders;

public abstract class BuilderBase<T>
{
    public required T Instance { get; set; }

    public T Build()
    {
        return Instance;
    }
}