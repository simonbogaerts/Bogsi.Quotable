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
        _cancellationToken = new CancellationToken();

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
        GetQuotesHandlerRequest request = new();

        List<Quote> quotes =
            [
                new (){ PublicId = Guid.NewGuid(), Created = DateTime.Now, Updated = DateTime.Now, Value = "VALUE" },
                new (){ PublicId = Guid.NewGuid(), Created = DateTime.Now, Updated = DateTime.Now, Value = "VALUE" }
            ];

        _repository.GetAsync(Arg.Any<CancellationToken>()).Returns(quotes);

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
