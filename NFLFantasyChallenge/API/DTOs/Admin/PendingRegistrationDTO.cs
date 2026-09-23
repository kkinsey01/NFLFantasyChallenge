namespace NFLFantasyChallenge.API.DTOs.Admin;

public class PendingRegistrationDTO
{
    public int PendingRegistrationId { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime CreationDate { get; set; }
    public string DisplayCreationDate
    {
        get
        {
            return CreationDate.ToString("MM/dd hh:mm tt");
        }
    }
}
