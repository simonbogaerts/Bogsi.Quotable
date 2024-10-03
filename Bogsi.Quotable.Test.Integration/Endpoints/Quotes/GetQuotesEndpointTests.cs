using System.Net.Http.Json;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Test.Builders.Entities;
using Bogsi.Quotable.Test.Integration.Utilities;

using FluentAssertions;

namespace Bogsi.Quotable.Test.Integration.Endpoints.Quotes;

public class GetQuotesEndpointTests : TestBase
{
    #region MyRegion

    public GetQuotesEndpointTests(IntegrationTestWebApplicationBuilderFactory factory) : base(factory) { }

    #endregion

    [Fact]
    public async Task GivenGetQuotesEndpoint_WhenFetchingAllWithParameters_ThenProvideCursorResponseWithListOfResponseObjects()
    {
        // GIVEN
        var size = 5;
        var parameters = $"?cursor=1&size={size}";

        List<QuoteEntity> entities = [];

        for (int i = 0; i < 6; i++) 
        {
            QuoteEntity entity = new QuoteEntityBuilder().Build();

            entities.Add(entity);
        }

        _quotableContext.Quotes.AddRange(entities);
        await _quotableContext.SaveChangesAsync();

        var total = _quotableContext.Quotes.Count();

        // WHEN
        var response = await _client.GetAsync("api/v1/quotes" + parameters);

        // THEN
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK, "this endpoint returns 200 on success");

        var result = await response.Content.ReadFromJsonAsync<GetQuotesResponse>();

        response.Should().NotBeNull("we should get a response form");
        result.Should().NotBeNull("the response should deserialize to the correct object");
        result!.Cursor.Should().NotBe(Application.Constants.Cursor.None, "response contains the id of the next item");
        result!.Size.Should().Be(size, "response includes the request size in the response");
        result!.Total.Should().Be(total, "it should match the entire collection size");
        result!.HasNext.Should().BeTrue("there are more items to fetch");
        result.Data.Count.Should().Be(size, "the request asked for 5 items");
    }

    [Fact]
    public async Task GivenGetQuotesEndpoint_WhenThereAreNoItemsToReturn_ThenProvideEmptyCursorResponse()
    {
        // GIVEN
        var size = 5;
        var parameters = $"?cursor=1&size={size}";

        // WHEN
        var response = await _client.GetAsync("api/v1/quotes" + parameters);

        // THEN
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK, "this endpoint returns 200 on success");

        var result = await response.Content.ReadFromJsonAsync<GetQuotesResponse>();

        response.Should().NotBeNull("we should get a response form");
        result.Should().NotBeNull("the response should deserialize to the correct object");
        result!.Cursor.Should().Be(Application.Constants.Cursor.None, "there is no next item");
        result!.Size.Should().Be(size, "response includes the request size in the response");
        result!.Total.Should().Be(0, "there are no items in the database");
        result!.HasNext.Should().BeFalse("there is no additional items");
        result.Data.Count.Should().Be(0, "we have no items");
    }

    // return filtering
    [Fact]
    public async Task GivenGetQuotesEndpoint_WhenProvidingASearchQuery_ThenProvideCursorResponseWithItemsThatMatchQuery()
    {
        // GIVEN
        var size = 5;
        var parameters = $"?cursor=1&size={size}&searchquery=filter";

        List<QuoteEntity> entities = 
            [
                new QuoteEntityBuilder().WithValue("FILTER").Build(),
                new QuoteEntityBuilder().WithValue("DEFAULT-VALUE").Build(),
                new QuoteEntityBuilder().WithValue("DEFAULT-VALUE").Build(),
                new QuoteEntityBuilder().WithValue("FILTER").Build(),
                new QuoteEntityBuilder().WithValue("DEFAULT-VALUE").Build()
            ];

        _quotableContext.Quotes.AddRange(entities);
        await _quotableContext.SaveChangesAsync();

        var total = _quotableContext.Quotes.Count();

        // WHEN
        var response = await _client.GetAsync("api/v1/quotes" + parameters);

        // THEN
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK, "this endpoint returns 200 on success");

        var result = await response.Content.ReadFromJsonAsync<GetQuotesResponse>();

        response.Should().NotBeNull("we should get a response form");
        result.Should().NotBeNull("the response should deserialize to the correct object");
        result!.Cursor.Should().NotBe(Application.Constants.Cursor.None, "response contains the id of the next item");
        result!.Size.Should().Be(size, "response includes the request size in the response");
        result!.Total.Should().Be(2, "it should match the entire collection size that matches the search query");
        result!.HasNext.Should().BeFalse("there are no more items to fetch");
        result.Data.Count.Should().Be(2, "no other items match te filter");
    }

    // return searching upon impl the properties
}
