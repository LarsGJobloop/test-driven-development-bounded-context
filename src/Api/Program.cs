using DTO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Feedback> feedbackList = [];

app.MapGet("/feedback", () =>
{
  return feedbackList;
});

app.MapPost("/feedback", (Feedback userFeedback) =>
{
  feedbackList.Add(userFeedback);
});

app.Run();

// Required by the API test fixture
public partial class Program { }