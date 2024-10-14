namespace Bogsi.Quotable.Test.Integration.Endpoints.Quotes;

using System.Net.Http.Json;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Test.Builders.Entities;
using Bogsi.Quotable.Test.Builders.Requests;
using Bogsi.Quotable.Test.Integration.Utilities;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class CreateQuoteEndpointTests : TestBase
{
    #region Test Setup

    public CreateQuoteEndpointTests(IntegrationTestWebApplicationBuilderFactory factory) : base(factory) { }

    #endregion

    [Fact]
    public async Task GivenCreateQuoteEndpoint_WhenValidRequstIsSend_ThenNewItemIsCreatedAndReturned()
    {
        // GIVEN 
        CreateQuoteRequest request = new CreateQuoteRequestBuilder().Build();

        // WHEN 
        var response = await _client.PostAsJsonAsync("api/v1/quotes", request);

        // THEN 
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created, "this endpoint returns 201 on success");

        var result = await response.Content.ReadFromJsonAsync<CreateQuoteResponse>();
        var created = await _quotableContext.Quotes.SingleOrDefaultAsync(x => x.PublicId == result!.PublicId);

        response.Should().NotBeNull("we should get a response form");
        result.Should().NotBeNull("the response should deserialize to the correct object");
        created.Should().NotBeNull("the created item should be in the database");

        result!.Value.Should().Be(created!.Value, "the same value should be returned");
        result!.Value.Should().Be(request!.Value, "the same value should be stored");
        result!.PublicId.Should().Be(created!.PublicId, "the same value should be returned");
        result!.Created.Should().Be(created!.Created, "the same value should be returned");
        result!.Updated.Should().Be(created!.Updated, "the same value should be returned");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task GivenCreateQuoteEndpoint_WhenInvalidRequestIsSend_ThenBadRequestIsReturned(string? value)
    {
        // GIVEN 
        CreateQuoteRequest request = new CreateQuoteRequestBuilder().WithValue(value!).Build();

        // WHEN
        var response = await _client.PostAsJsonAsync("api/v1/quotes", request);

        // THEN 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest, "this endpoint returns 400 when validation has failed");
    }

    [Fact]
    public async Task GivenCreateQuoteEndpoint_WhenRequestIsSendToEndpointWithIdAndItemExists_ThenConflictIsReturned()
    {
        // GIVEN 
        Guid publicId = Guid.NewGuid();

        CreateQuoteRequest request = new CreateQuoteRequestBuilder().Build();

        QuoteEntity entity = new QuoteEntityBuilder().WithPublicId(publicId).Build();

        _quotableContext.Add(entity);
        await _quotableContext.SaveChangesAsync();

        // WHEN
        var response = await _client.PostAsJsonAsync($"api/v1/quotes/{publicId}", request);

        // THEN 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Conflict, "this endpoint returns 409, create at route is not permitted");
    }

    [Fact]
    public async Task GivenCreateQuoteEndpoint_WhenRequestIsSendToEndpointWithIdAndItemDoesNotExists_ThenNotFoundIsReturned()
    {
        // GIVEN 
        Guid publicId = Guid.NewGuid();

        CreateQuoteRequest request = new CreateQuoteRequestBuilder().Build();

        // WHEN
        var response = await _client.PostAsJsonAsync($"api/v1/quotes/{publicId}", request);

        // THEN 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound, "this endpoint returns 404 on when item does not exist");
    }
}
