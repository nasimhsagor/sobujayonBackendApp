namespace sobujayonApp.Core.DTO;

public record RegisterRequest(
  string? Email,
  string? Phone,
  string? Password,
  string? Name,
  DateTime? Dob,
  string? Address,
  GenderOptions Gender);
