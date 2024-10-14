namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes;

using Bogsi.Quotable.Application.Utilities;
using Bogsi.Quotable.Test.Builders.Models;
using Bogsi.Quotable.Test.Builders.Requests;

using Quote = Application.Models.Quote;

public class GetQuotesHandlerTests : TestBase<IGetQuotesHandler>
{
    #region Test Setup

    private IReadonlyRepository<Quote> _repository = null!;
    private IMapper _mapper = null!;
    private CancellationToken _cancellationToken;

    protected override IGetQuotesHandler Construct()
    {
        _mapper = ConfigureMapper();
        _repository = Substitute.For<IReadonlyRepository<Quote>>();
        _cancellationToken = new();

        GetQuotesHandler sut = new(
            _repository,
            _mapper);

        return sut;
    }

    #endregion

    [Fact]
    public async Task GivenGetQuotesHandler_WhenParametersDontMatter_ReturnAllQuotesAsResponseModel()
    {
        // GIVEN
        GetQuotesHandlerRequest request = new GetQuotesHandlerRequestBuilder().Build();
        List<Quote> data =
            [
                new QuoteBuilder().Build(),
                new QuoteBuilder().Build()
            ];

        CursorResponse<Quote> quotes = new() 
        { 
            Data = data 
        };

        _repository.GetAsync(Arg.Any<GetQuotesHandlerRequest>(), Arg.Any<CancellationToken>()).Returns(quotes);

        // WHEN 
        var result = await Sut.HandleAsync(request, _cancellationToken);

        //THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeTrue("Result should be success");
        result.IsFailure.Should().BeFalse("Result should be success");
        result.Value.Should().NotBeNull("Result should contain success value");
        result.Value.Data.Count.Should().Be(2, "Result should contain 2 items");
    }
}
