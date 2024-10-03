using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Test.Builders.Entities;
using Bogsi.Quotable.Test.Integration.Utilities;

using FluentAssertions;

namespace Bogsi.Quotable.Test.Integration.Endpoints.Quotes;

public class DeleteQuoteEndpointTests : TestBase
{
    #region Test Setup

    public DeleteQuoteEndpointTests(IntegrationTestWebApplicationBuilderFactory factory) : base(factory) { }

    #endregion

    [Fact]
    public async Task GivenDeleteQuoteEndpoint_WhenDeleteIsSuccesfull_ThenNoContentIsReturned()
    {
        // GIVEN
        Guid publicId = Guid.NewGuid();

        QuoteEntity entity = new QuoteEntityBuilder().WithPublicId(publicId).Build();

        _quotableContext.Add(entity);
        await _quotableContext.SaveChangesAsync();

        // WHEN 
        var resultPreDelete = await _client.GetAsync($"api/v1/quotes/{publicId}");

        var response = await _client.DeleteAsync($"api/v1/quotes/{publicId}");

        // THEN
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent, "this endpoint returns 204 on success");

        resultPreDelete.Should().NotBeNull("enity is not deleted yet");

        var resultPostDelete = await _client.GetAsync($"api/v1/quotes/{publicId}");
        resultPostDelete.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound, "this endpoint returns 404 on when item does not exist");
    }

    [Fact]
    public async Task GivenDeleteQuoteEndpoint_WhenItemIsNotFound_ThenNotFoundIsReturned()
    {
        // GIVEN
        Guid publicId = Guid.NewGuid();

        // WHEN 
        var response = await _client.DeleteAsync($"api/v1/quotes/{publicId}");

        // THEN
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound, "this endpoint returns 404 on when item does not exist");
    }
}
