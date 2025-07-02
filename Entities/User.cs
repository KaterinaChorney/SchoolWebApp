using Microsoft.AspNetCore.Identity;
using SchoolWebApplication.Entities;

public class User : IdentityUser
{
    public string? DisplayName { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}