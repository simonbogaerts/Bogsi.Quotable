using Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteByIdHandler;

namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes.GetQuotes;

public class GetQuoteByIdHandlerTests : TestBase<IGetQuoteByIdHandler>
{
    #region Test Setup

    private IReadonlyRepository<Quote> _repository = null!;
    private IMapper _mapper = null!;
    private CancellationToken _cancellationToken;

    protected override IGetQuoteByIdHandler Construct()
    {
        _mapper = ConfigureMapper();
        _repository = Substitute.For<IReadonlyRepository<Quote>>();
        _cancellationToken = new CancellationToken();

        GetQuoteByIdHandler sut = new(
            _repository,
            _mapper);

        return sut;
    }

    #endregion

    [Fact]
    public async Task GivenGetQuoteByIdHandler_WhenPublicIdIsMatched_ThenReturnQuoteAsResponseModel()
    {
        // GIVEN
        var publicId = Guid.NewGuid();
        string value = "VALUE";

        GetQuoteByIdHandlerRequest request = new()
        {
            PublicId = publicId
        };

        Quote quote = new() 
        { 
            PublicId = publicId,
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Value = value
        };

        _repository.GetByIdAsync(Arg.Is<Guid>(publicId), Arg.Any<CancellationToken>()).Returns(quote);

        // WHEN 
        var result = await Sut.HandleAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull();
        result.Quote.Should().NotBeNull();
        result.Quote!.PublicId.Should().Be(publicId);
        result.Quote!.Value.Should().Be(value);
    }

    [Fact]
    public async Task GivenGetQuoteByIdHandler_WhenPublicIdDoesNotMatchAny_ThenReturnNullInResponseModel()
    {
        // GIVEN
        GetQuoteByIdHandlerRequest request = new()
        {
            PublicId = Guid.NewGuid()
        };

        // WHEN 
        var result = await Sut.HandleAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull();
        result.Quote.Should().BeNull();
    }
}
