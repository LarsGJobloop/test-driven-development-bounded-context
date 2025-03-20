// Models are the internal classes that we use
// to support whatever usecase our application is intended for.
// Since we control everything here, these are possibly more ephemeral
// and can be molded to what is currently needed.

namespace Models;

public class Feedback
{
  public Guid Id { get; } = Guid.NewGuid();
  public int ProductRef { get; init; }
  public string? Comment { get; init; }
  public int Rating { get; init; }
}
