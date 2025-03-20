using DTO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Feedback> feedbackList = [];

app.MapGet("/feedback", () =>
{
  return feedbackList;
});

app.MapPost("/feedback", (FeedbackCreateRequest userFeedbackRequest) =>
{
  var newFeedback = new Feedback
  {
    Comment = userFeedbackRequest.Comment,
    ProductRef = userFeedbackRequest.ProductRef,
    Rating = userFeedbackRequest.Rating,
  };
  feedbackList.Add(newFeedback);

  return Results.Ok(new CreatedResponse { Id = newFeedback.Id });
});

app.Run();

// Required by the API test fixture
public partial class Program { }