using Bogsi.Quotable.Infrastructure.Repositories;
using Bogsi.Quotable.Persistence;
using Bogsi.Quotable.Test.Builders.Entities;
using Bogsi.Quotable.Test.Builders.Requests;

using Quote = Bogsi.Quotable.Application.Models.Quote;

namespace Bogsi.Quotable.Test.Unit.Repositories.Quotes;

public class QuoteReposiGetAsyncParameterTeststoryGetAsyncParametersTests : TestBase<IReadonlyRepository<Quote>>
{
    #region Test Setup

    private QuotableContext _quotable = null!;
    private IMapper _mapper = null!;
    private CancellationToken _cancellationToken;

    protected override IRepository<Quote> Construct()
    {
        _quotable = ConfigureDatabase();
        _mapper = ConfigureMapper();
        _cancellationToken = new();

        QuoteRepository sut = new(
            _quotable,
            _mapper);

        return sut;
    }

    #endregion

    #region Pagination 

    [Fact]
    public async Task GivenGetAsyncPaginationParameters_WhenCursorIsDefaultAndSizeIsFive_ThenReturnCorrectCollectionAndCursorInfo()
    {
        // GIVEN
        int cursor = Cursor.Default;
        int requestSize = 5;
        int outOfRequestRange = 6;

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().WithId(cursor).Build(),
            new QuoteEntityBuilder().WithId(2).Build(),
            new QuoteEntityBuilder().WithId(3).Build(),
            new QuoteEntityBuilder().WithId(4).Build(),
            new QuoteEntityBuilder().WithId(5).Build(),
            new QuoteEntityBuilder().WithId(outOfRequestRange).Build(),
            new QuoteEntityBuilder().WithId(7).Build(),
            new QuoteEntityBuilder().WithId(8).Build(),
            new QuoteEntityBuilder().WithId(9).Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        GetQuotesHandlerRequest request = new GetQuotesHandlerRequestBuilder().WithCursor(cursor).WithSize(requestSize).Build();

        // WHEN 
        var result = await Sut.GetAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("result object should never be null");
        result.IsSuccess.Should().BeTrue("response should be succesfull");
        result.IsFailure.Should().BeFalse("response should be succesfull");
        result.Value.Data.Should().NotBeNull("a response returns an initialized list");
        result.Value.Data.Count.Should().Be(request.Size, $"{requestSize} items were requested");
        result.Value.Cursor.Should().Be(outOfRequestRange, $"the id of the next item is {outOfRequestRange}");
        result.Value.Size.Should().Be(request.Size, $"{requestSize} items were requested");
        result.Value.Total.Should().Be(quoteEntities.Count, $"there are {quoteEntities.Count} items in the database");
        result.Value.HasNext.Should().BeTrue($"there are {quoteEntities.Count} items and only {requestSize} were requested");
    }

    [Fact]
    public async Task GivenGetAsyncPaginationParameters_WhenCursorIsThreeAndSizeIsFive_ThenReturnCorrectCollectionAndCursorInfo()
    {
        // GIVEN
        int cursor = 3;
        int requestSize = 5;
        int outOfRequestRange = 8;

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().WithId(1).Build(),
            new QuoteEntityBuilder().WithId(2).Build(),
            new QuoteEntityBuilder().WithId(cursor).Build(),
            new QuoteEntityBuilder().WithId(4).Build(),
            new QuoteEntityBuilder().WithId(5).Build(),
            new QuoteEntityBuilder().WithId(6).Build(),
            new QuoteEntityBuilder().WithId(7).Build(),
            new QuoteEntityBuilder().WithId(outOfRequestRange).Build(),
            new QuoteEntityBuilder().WithId(9).Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        GetQuotesHandlerRequest request = new GetQuotesHandlerRequestBuilder().WithCursor(cursor).WithSize(requestSize).Build();

        // WHEN 
        var result = await Sut.GetAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("result object should never be null");
        result.IsSuccess.Should().BeTrue("response should be succesfull");
        result.IsFailure.Should().BeFalse("response should be succesfull");
        result.Value.Data.Should().NotBeNull("a response returns an initialized list");
        result.Value.Data.Count.Should().Be(request.Size, $"{requestSize} items were requested");
        result.Value.Cursor.Should().Be(outOfRequestRange, $"the id of the next item is {outOfRequestRange}");
        result.Value.Size.Should().Be(request.Size, $"{requestSize} items were requested");
        result.Value.Total.Should().Be(quoteEntities.Count, $"there are {quoteEntities.Count} items in the database");
        result.Value.HasNext.Should().BeTrue($"there are {quoteEntities.Count} items and only {requestSize} were requested");
    }

    [Fact]
    public async Task GivenGetAsyncPaginationParameters_WhenSizeIsMoreThenRemainingItems_ThenReturnCorrectCollectionAndCursorInfo()
    {
        // GIVEN
        int cursor = 3;
        int requestSize = 5;
        int finalId = 6;

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().WithId(1).Build(),
            new QuoteEntityBuilder().WithId(2).Build(),
            new QuoteEntityBuilder().WithId(cursor).Build(),
            new QuoteEntityBuilder().WithId(4).Build(),
            new QuoteEntityBuilder().WithId(5).Build(),
            new QuoteEntityBuilder().WithId(finalId).Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        GetQuotesHandlerRequest request = new GetQuotesHandlerRequestBuilder().WithCursor(cursor).WithSize(requestSize).Build();

        // WHEN 
        var result = await Sut.GetAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("result object should never be null");
        result.IsSuccess.Should().BeTrue("response should be succesfull");
        result.IsFailure.Should().BeFalse("response should be succesfull");
        result.Value.Data.Should().NotBeNull("a response returns an initialized list");
        result.Value.Data.Count.Should().Be(4, $"only 4 items remaining starting from provided cursor.");
        result.Value.Cursor.Should().Be(finalId, $"the id of the final item is {finalId}");
        result.Value.Size.Should().Be(request.Size, $"{requestSize} items were requested");
        result.Value.Total.Should().Be(quoteEntities.Count, $"there are {quoteEntities.Count} items in the database");
        result.Value.HasNext.Should().BeFalse("there are no more items left");
    }

    #endregion

    #region Searching

    [Fact]
    public async Task GivenGetAsyncSearching_WhenItemsFullyOrPartiallyMatchSearchQuery_ThenReturnCorrectCollectionAndCursorInfo()
    {
        // GIVEN
        string partialMatch = "KEYWORD";
        string fullMatch = "KEY";
        string shouldBeIgnored = "IGNORED";

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build(),
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build(),
            new QuoteEntityBuilder().WithValue(fullMatch).Build(),
            new QuoteEntityBuilder().WithValue(partialMatch).Build(),
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        GetQuotesHandlerRequest request = new GetQuotesHandlerRequestBuilder().WithSearchQuery(fullMatch).Build();

        // WHEN 
        var result = await Sut.GetAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("result object should never be null");
        result.IsSuccess.Should().BeTrue("response should be succesfull");
        result.IsFailure.Should().BeFalse("response should be succesfull");
        result.Value.Data.Should().NotBeNull("a response returns an initialized list");
        result.Value.Data.Count.Should().Be(2, $"only 2 items match the searchQuery");
        result.Value.Total.Should().Be(2, $"there are 2 items total that match the searchQuery");
    }

    [Fact]
    public async Task GivenGetAsyncSearching_WhenValueMatchesButCasingIsDifferent_ThenReturnCorrectCollectionAndCursorInfo()
    {
        // GIVEN
        string partialMatch = "KEYWORD";
        string fullMatch = "KEY";
        string shouldBeIgnored = "IGNORED";

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build(),
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build(),
            new QuoteEntityBuilder().WithValue(fullMatch.ToLower()).Build(),
            new QuoteEntityBuilder().WithValue(partialMatch.ToLower()).Build(),
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        GetQuotesHandlerRequest request = new GetQuotesHandlerRequestBuilder().WithSearchQuery(fullMatch).Build();

        // WHEN 
        var result = await Sut.GetAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("result object should never be null");
        result.IsSuccess.Should().BeTrue("response should be succesfull");
        result.IsFailure.Should().BeFalse("response should be succesfull");
        result.Value.Data.Should().NotBeNull("a response returns an initialized list");
        result.Value.Data.Count.Should().Be(2, $"only 2 items match the searchQuery");
        result.Value.Total.Should().Be(2, $"there are 2 items total that match the searchQuery");
    }

    [Fact]
    public async Task GivenGetAsyncSearching_WhenValueMatchesNone_ThenReturnCorrectCollectionAndCursorInfo()
    {
        // GIVEN
        string keyword = "KEYWORD";
        string shouldBeIgnored = "IGNORED";

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build(),
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build(),
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build(),
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build(),
            new QuoteEntityBuilder().WithValue(shouldBeIgnored).Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        GetQuotesHandlerRequest request = new GetQuotesHandlerRequestBuilder().WithSearchQuery(keyword).Build();

        // WHEN 
        var result = await Sut.GetAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("result object should never be null");
        result.IsSuccess.Should().BeTrue("response should be succesfull");
        result.IsFailure.Should().BeFalse("response should be succesfull");
        result.Value.Data.Should().NotBeNull("a response returns an initialized list");
        result.Value.Data.Count.Should().Be(0, $"no items match the searchQuery");
        result.Value.Total.Should().Be(0, $"there are 0 items total that match the searchQuery");
    }

    #endregion

    #region Filtering

    // add tests here after model is expended.

    #endregion
}