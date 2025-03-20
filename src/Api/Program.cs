var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Models.Feedback> feedbackList = [];

app.MapGet("/feedback", () =>
{
  return feedbackList;
});

app.MapPost("/feedback", (DTO.FeedbackCreateRequest userFeedbackRequest) =>
{
  var newFeedback = new Models.Feedback
  {
    Comment = userFeedbackRequest.Comment,
    ProductRef = userFeedbackRequest.ProductRef,
    Rating = userFeedbackRequest.Rating,
  };
  feedbackList.Add(newFeedback);

  return Results.Ok(new DTO.CreatedResponse { Id = newFeedback.Id });
});

app.Run();

// Required by the API test fixture
public partial class Program { }