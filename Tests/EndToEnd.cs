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

    public static TheoryData<FeedbackCreateRequest> TestData =>
        [
            new() {Comment = "This product is Great!", ProductRef = 0, Rating = 5},
            new() {Comment = "Mehe", ProductRef = 10, Rating = 3},
            new() {Comment = "Mehe", ProductRef = 10, Rating = 3},
        ];

    [Theory]
    [MemberData(nameof(TestData))]
    public async Task PostedFeedbackShouldBeRetrievable(FeedbackCreateRequest userFeedbackRequest)
    {
        // Arrange
        var endpointPath = "/feedback";
        var requestContent = new StringContent(
            JsonSerializer.Serialize(userFeedbackRequest),
            Encoding.UTF8,
            "application/json"
        );

        var httpClient = _factory.CreateClient();

        var timeout = TimeSpan.FromSeconds(1);
        using var cts = new CancellationTokenSource(timeout);

        // Act
        var postResponse = await httpClient.PostAsync(endpointPath, requestContent, cts.Token);
        postResponse.EnsureSuccessStatusCode();
        var postResponseContent = await postResponse.Content.ReadAsStringAsync();
        var successResponse = JsonSerializer.Deserialize<CreatedResponse>(
            postResponseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        Assert.NotNull(successResponse);

        var getResponse = await httpClient.GetAsync(endpointPath, cts.Token);
        getResponse.EnsureSuccessStatusCode();

        var responseContent = await getResponse.Content.ReadAsStringAsync();
        var feedbackList = JsonSerializer.Deserialize<List<FeedbackExternal>>(
            responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        Assert.NotNull(feedbackList);

        var retrievedFeedback = feedbackList.FindAll(feedback => feedback.Id == successResponse.Id);

        // Assert
        Assert.NotNull(retrievedFeedback);
        Assert.Single(retrievedFeedback);
        Assert.Equivalent(userFeedbackRequest, retrievedFeedback[0]);
    }
}
