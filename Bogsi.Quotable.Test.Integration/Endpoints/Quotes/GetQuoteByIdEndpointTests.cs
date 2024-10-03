using System.Net.Http.Json;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Test.Builders.Entities;
using Bogsi.Quotable.Test.Integration.Utilities;

using FluentAssertions;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bogsi.Quotable.Test.Integration.Endpoints.Quotes;

public class GetQuoteByIdEndpointTests : TestBase
{
    #region Test Setup

    public GetQuoteByIdEndpointTests(IntegrationTestWebApplicationBuilderFactory factory) : base(factory) { }

    #endregion

    [Fact]
    public async Task GivenGetQuoteByIdEndpoint_WhenRequestIsMadeToExistingItem_ThenItemIsReturned()
    {
        // GIVEN
        Guid publicId = Guid.NewGuid();

        QuoteEntity entity = new QuoteEntityBuilder().WithPublicId(publicId).Build();

        _quotableContext.Add(entity);
        await _quotableContext.SaveChangesAsync();

        // WHEN 
        var response = await _client.GetAsync($"api/v1/quotes/{publicId}");

        // THEN
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK, "this endpoint returns 200 on success");

        var result = await response.Content.ReadFromJsonAsync<GetQuoteByIdResponse>();
        response.Should().NotBeNull("we should get a response form");
        result.Should().NotBeNull("the response should deserialize to the correct object");

        result!.Value.Should().Be(entity!.Value, "the same value should be returned");
        result!.PublicId.Should().Be(entity!.PublicId, "the same value should be returned");
        //result!.Created.Should().Be(entity!.Created, "the same value should be returned");
        //result!.Updated.Should().Be(entity!.Updated, "the same value should be returned");
    }

    [Fact]
    public async Task GivenGetQuoteByIdEndpoint_WhenRequestIsMadeToNonExistingItem_ThenNotFoundIsReturned()
    {
        // GIVEN
        Guid publicId = Guid.NewGuid();

        // WHEN 
        var response = await _client.GetAsync($"api/v1/quotes/{publicId}");

        // THEN
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound, "this endpoint returns 404 on when item does not exist");
    }
}
