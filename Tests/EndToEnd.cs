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
        var endpointPath = "/feedback";
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

        // A server which never responds is no good
        var timeout = TimeSpan.FromSeconds(1);
        using var cts = new CancellationTokenSource(timeout);

        // Act
        var postResponse = await httpClient.PostAsync(endpointPath, requestContent, cts.Token);
        postResponse.EnsureSuccessStatusCode(); // If we can't post feedback it does not work
        var getResponse = await httpClient.GetAsync(endpointPath, cts.Token);
        getResponse.EnsureSuccessStatusCode();

        var responseContent = await getResponse.Content.ReadAsStringAsync();
        var feedbackList = JsonSerializer.Deserialize<List<Feedback>>(
            responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        // Assert
        Assert.NotNull(feedbackList);
        Assert.Single(feedbackList);
        Assert.Equivalent(userFeedback, feedbackList[0]);
    }
}
