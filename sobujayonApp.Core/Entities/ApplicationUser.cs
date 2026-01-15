namespace sobujayonApp.Core.Entities;

/// <summary>
/// Define the ApplicationUser class which acts as entity model class to store user details in data store
/// </summary>
public class ApplicationUser
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? Name { get; set; }
    public DateTime? Dob { get; set; }
    public string? Address { get; set; }
    public string? Gender { get; set; }
    public string Password { get; set; }
}
