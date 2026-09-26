using NFLFantasyChallenge.Models;

namespace NFLFantasyChallenge.API.Services.Interfaces;

public interface IEmailService
{
    public Task SendEmail(string message);
    public Task SendNewRegistrationEmail(string username, DateTime requestedTime);
    public Task SendApprovedRegistrationMessage(User user);
}
