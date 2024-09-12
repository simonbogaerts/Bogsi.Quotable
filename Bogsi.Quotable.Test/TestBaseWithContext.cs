using Bogsi.Quotable.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bogsi.Quotable.Test;

public abstract class TestBaseWithContext<T> : TestBase<T> where T : class
{
    protected static QuotableContext SetupQuotableDatabase()
    {
        var options = new DbContextOptionsBuilder<QuotableContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new(options);
    }
}
