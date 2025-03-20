namespace DTO;

public class FeedbackCreateRequest
{
  public int ProductRef { get; init; }
  public string? Comment { get; init; }
  public int Rating { get; init; }
}

public class CreatedResponse
{
  public Guid Id { get; init; }
}

public class Feedback
{
  public Guid Id { get; init; }
  public int ProductRef { get; init; }
  public string? Comment { get; init; }
  public int Rating { get; init; }
}
