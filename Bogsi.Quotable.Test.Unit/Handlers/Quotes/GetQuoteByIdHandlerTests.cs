using Bogsi.Quotable.Application.Errors;

namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes;

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

        GetQuoteByIdHandlerRequest request = new()
        {
            PublicId = publicId
        };

        Quote model = new()
        {
            PublicId = publicId,
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Value = "VALUE-FOR-TEST"
        };

        _repository
            .GetByIdAsync(Arg.Is(publicId), Arg.Any<CancellationToken>())
            .Returns(model);

        // WHEN 
        var result = await Sut.HandleAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeTrue("Result should be success");
        result.IsFailure.Should().BeFalse("Result should be success");
        result.Value.Should().NotBeNull("Result should contain success value");
        result.Value.PublicId.Should().Be(publicId, "PublicId should match the request");
        result.Value.Value.Should().Be(model.Value, "Value should match the model");
    }

    [Fact]
    public async Task GivenGetQuoteByIdHandler_WhenPublicIdDoesNotMatchAny_ThenReturnFailureResult()
    {
        // GIVEN
        GetQuoteByIdHandlerRequest request = new()
        {
            PublicId = Guid.NewGuid()
        };

        _repository
            .GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(QuotableErrors.NotFound);

        // WHEN 
        var result = await Sut.HandleAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeFalse("Result should be failure");
        result.IsFailure.Should().BeTrue("Result should be failure");
    }
}
