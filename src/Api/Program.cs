using DTO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/feedback", () => new List<Feedback>
{
  new() {
    ProductRef = 1,
    Comment = "This product is Great!",
    Rating = 5,
  }
});

app.MapPost("/feedback", () => "Feedback Recieved!");

app.Run();

// Required by the API test fixture
public partial class Program { }