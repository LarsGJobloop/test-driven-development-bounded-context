var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/feedback", () => "Feedback Returned!");
app.MapPost("/feedback", () => "Feedback Recieved!");

app.Run();

// Required by the API test fixture
public partial class Program { }