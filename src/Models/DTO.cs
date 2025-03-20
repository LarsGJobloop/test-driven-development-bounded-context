// DTOs or Domain Transfer Objects
// are the agreements we have with those they lies outside our little corner of the world
// For a Web API this might be the messages sent by a frontend or another system
// along with the response we send back.
//
// NOTE! Since these contains external actors, be weary of changing them,
// also be weary of how much you provide, it's a maintenance burden
// and will limit your capability to adapt to changing requirements.

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
