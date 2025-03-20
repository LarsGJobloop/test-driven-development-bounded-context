using DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace Tests;

public class FeedbackServiceE2E(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    readonly HttpClient _httpClient = factory.CreateClient();

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

        var timeout = TimeSpan.FromSeconds(1);
        using var cts = new CancellationTokenSource(timeout);

        // Act
        var successResponse = await PostJson<FeedbackCreateRequest, CreatedResponse>(endpointPath, userFeedbackRequest, cts.Token);
        var feedbackList = await GetJson<List<Feedback>>(endpointPath, cts.Token);
        var retrievedFeedback = feedbackList.FindAll(feedback => feedback.Id == successResponse.Id);

        // Assert
        Assert.NotNull(retrievedFeedback);
        Assert.Single(retrievedFeedback);
        Assert.Equivalent(userFeedbackRequest, retrievedFeedback[0]);
    }

    private async Task<SuccessType> PostJson<PostType, SuccessType>(string endpointPath, PostType jsonObject, CancellationToken token)
    {
        var jsonString = new StringContent(
            JsonSerializer.Serialize(jsonObject),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(endpointPath, jsonString, token);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync(token);
        var deserialized = JsonSerializer.Deserialize<SuccessType>(
            responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        Assert.NotNull(deserialized);

        return deserialized;
    }

    private async Task<ReturnType> GetJson<ReturnType>(string endpointPath, CancellationToken token)
    {
        var getResponse = await _httpClient.GetAsync(endpointPath, token);
        getResponse.EnsureSuccessStatusCode();

        var responseContent = await getResponse.Content.ReadAsStringAsync(token);
        var feedbackList = JsonSerializer.Deserialize<ReturnType>(
            responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        Assert.NotNull(feedbackList);

        return feedbackList;
    }
}
