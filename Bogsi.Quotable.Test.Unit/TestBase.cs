namespace Bogsi.Quotable.Test.Unit;

using Bogsi.Quotable.Application;
using Bogsi.Quotable.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public abstract class TestBase<T> where T : class
{
    public TestBase()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsetting.json", true)
            .Build();

        Sut = Construct();
    }

    protected T Sut { get; set; }

    protected IConfiguration Configuration { get; init; }

    protected abstract T Construct();

    protected static IMapper ConfigureMapper()
    {
        var configuration = new MapperConfiguration(x => x.AddMaps(typeof(IApplicationMarker).Assembly));

        return configuration.CreateMapper();
    }

    protected static QuotableContext ConfigureDatabase()
    {
        var options = new DbContextOptionsBuilder<QuotableContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new(options);
    }
}
