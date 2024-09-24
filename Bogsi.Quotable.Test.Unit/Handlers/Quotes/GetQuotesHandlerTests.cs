using Bogsi.Quotable.Application.Utilities;
using Bogsi.Quotable.Test.Builders.Models;
using Bogsi.Quotable.Test.Builders.Requests;

namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes;

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

        CursoredList<Quote> quotes =
            [
                new QuoteBuilder().Build(),
                new QuoteBuilder().Build()
            ];

        _repository.GetAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(quotes);

        // WHEN 
        var result = await Sut.HandleAsync(request, _cancellationToken);

        //THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeTrue("Result should be success");
        result.IsFailure.Should().BeFalse("Result should be success");
        result.Value.Should().NotBeNull("Result should contain success value");
        result.Value.Count().Should().Be(2, "Result should contain 2 items");
    }
}
