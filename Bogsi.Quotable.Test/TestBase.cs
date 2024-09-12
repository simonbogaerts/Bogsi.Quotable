using Microsoft.Extensions.Configuration;

namespace Bogsi.Quotable.Test;

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
}