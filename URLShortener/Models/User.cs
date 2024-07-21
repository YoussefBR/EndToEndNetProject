using System.ComponentModel.DataAnnotations;

using URLShortener.Helper;

namespace URLShortener.Models;

public class User
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordSalt { get; set; }
    public List<string> Roles { get; set; } = [];
}

public class UserToID
{

    public UserToID(string email, int userId)
    {
        Email = email;
        UserId = userId;
    }

    [Key]
    [StringLength(200)]
    public string Email { get; set; }

    [Required]
    public int UserId { get; set; }
    
}