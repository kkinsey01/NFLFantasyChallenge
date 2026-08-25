using System.ComponentModel.DataAnnotations;

namespace NFLFantasyChallenge.Models;

public class PendingRegistration
{
    [Key]
    public int PendingRegistrationId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string? Email { get; set; }    
    public DateTime RegistrationTime { get; set; }
}
