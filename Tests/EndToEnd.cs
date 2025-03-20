using DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using System.Text.Json;

namespace Tests;

public class FeedbackServiceE2E : IClassFixture<WebApplicationFactory<Program>>
{
    readonly WebApplicationFactory<Program> _factory;
    public FeedbackServiceE2E(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task PostedFeedbackShouldBeRetrievable()
    {
        // Arrange
        var userFeedback = new Feedback
        {
            ProductRef = 0,
            Comment = "This product is Great!",
            Rating = 5,
        };
        var requestContent = new StringContent(
            JsonSerializer.Serialize(userFeedback),
            Encoding.UTF8,
            "application/json"
        );

        var httpClient = _factory.CreateClient();

        // Act
        var postResponse = await httpClient.PostAsync("/feeedback", requestContent);
        var getResponse = await httpClient.GetAsync("/feedback");

        // Assert
        Assert.Equivalent(postResponse.Content, getResponse.Content);
    }
}
