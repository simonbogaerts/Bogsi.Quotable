using System.Net.Http.Json;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Test.Builders.Entities;
using Bogsi.Quotable.Test.Builders.Requests;
using Bogsi.Quotable.Test.Integration.Utilities;

using FluentAssertions;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Bogsi.Quotable.Test.Integration.Endpoints.Quotes;

public class UpdateQuoteEndpointTests : TestBase
{
    #region Test Setup

    public UpdateQuoteEndpointTests(IntegrationTestWebApplicationBuilderFactory factory) : base(factory) { }

    #endregion

    [Fact]
    public async Task GivenUpdateQuoteEndpoint_WhenValidRequestIsSend_ThenExistingItemIsUpdated()
    {
        // GIVEN 
        Guid id = Guid.NewGuid();

        UpdateQuoteRequest request = new UpdateQuoteRequestBuilder().WithValue("UPDATED-VALUE").Build();

        QuoteEntity entity = new QuoteEntityBuilder().WithPublicId(id).Build();

        _quotableContext.Add(entity);
        await _quotableContext.SaveChangesAsync();

        // WHEN
        var response = await _client.PutAsJsonAsync($"api/v1/quotes/{id}", request);

        // THEN 
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent, "this endpoint returns 204 on success");

        await _quotableContext.Entry<QuoteEntity>(entity).ReloadAsync();
        var updated = await _quotableContext.Quotes.SingleOrDefaultAsync(x => x.PublicId == id);

        response.Should().NotBeNull("we should get a response form");
        updated.Should().NotBeNull("the updated item should be in the database");
        updated!.Value.Should().Be(request.Value, "the updated should be persisted");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task GivenUpdateQuoteEndpoint_WhenInvalidRequestIsSend_ThenBadRequestIsReturned(string? value)
    {
        // GIVEN 
        Guid id = Guid.NewGuid();

        UpdateQuoteRequest request = new UpdateQuoteRequestBuilder().WithValue(value!).Build();

        // WHEN
        var response = await _client.PutAsJsonAsync($"api/v1/quotes/{id}", request);

        // THEN 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest, "this endpoint returns 400 when validation has failed");
    }

    [Fact]
    public async Task GivenUpdateQuoteEndpoint_WhenRequestIsSendToEndpointWithIdAndItemDoesNotExists_ThenNotFoundIsReturned()
    {
        // GIVEN 
        Guid id = Guid.NewGuid();

        UpdateQuoteRequest request = new UpdateQuoteRequestBuilder().Build();

        // WHEN
        var response = await _client.PutAsJsonAsync($"api/v1/quotes/{id}", request);

        // THEN 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound, "this endpoint returns 404 on when item does not exist");
    }
}
