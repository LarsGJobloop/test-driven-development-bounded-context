namespace DTO;

public class FeedbackCreateRequest
{
  public int ProductRef { get; init; }
  public string? Comment { get; init; }
  public int Rating { get; init; }
}

public class Feedback
{
  public Guid Id { get; } = Guid.NewGuid();
  public int ProductRef { get; init; }
  public string? Comment { get; init; }
  public int Rating { get; init; }
}
