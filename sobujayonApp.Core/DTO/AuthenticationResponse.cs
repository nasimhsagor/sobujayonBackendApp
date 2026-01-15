namespace sobujayonApp.Core.DTO;

public record AuthenticationResponse(
  Guid Id,
  string? Email,
  string? Name,
  string? Gender,
  string? Token,
  bool Success
  )
{
  //Parameterless constructor
  public AuthenticationResponse() : this(default, default, default, default, default, default)
  {
  }
}
