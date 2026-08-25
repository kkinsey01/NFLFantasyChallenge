using NFLFantasyChallenge.API.Services.Interfaces;
using NFLFantasyChallenge.Models;
using Resend;

namespace NFLFantasyChallenge.API.Services;

public class EmailService : IEmailService
{
    private readonly IResend _resend;

    public EmailService(IResend resend)
    {
        _resend = resend;
    }

    public async Task SendEmail(string message)
    {
        var email = GetBaseEmailMessage("Test", "kylerkinsey01@gmail.com");
        email.TextBody = message;

        await _resend.EmailSendAsync(email);
    }

    public async Task SendNewRegistrationEmail(string username, DateTime requestedTime)
    {
        var adminEmails = new[] { "kylerkinsey01@gmail.com", "michaeljpage44@gmail.com" };

        var message =
                "There is a new user registration for the fantasy challenge website!" +
                Environment.NewLine +
                $"Username: {username}" +
                Environment.NewLine +
                $"Submitted: {requestedTime.ToString("MM/dd/yyyy hh:mm:ss tt")}" +
                Environment.NewLine +
                $"Go to the admin page to review.";

        foreach (var address in adminEmails)
        {
            var email = GetBaseEmailMessage("New Fantasy User Registration", address);            
            email.TextBody = message;

            await _resend.EmailSendAsync(email);
        }        
    }

    public async Task SendApprovedRegistrationMessage(User user)
    {        
        if (string.IsNullOrEmpty(user.Email))
        {
            return;
        }

        var lines = new List<string>()
        {
            $"Hello {user.FullName},",
            $"Welcome to the NFL Fantasy Football Playoff Challenge Website!",
            $"You can now login with your username and password. Be sure to set your lineup before wild card weekend.",
            $"Scores will be updated weekly and you can keep updated on your standing within the league on the leaderboard tab.",
            $"Best of on your playoff run!"
        };

        var message = string.Join(Environment.NewLine, lines);

        var email = GetBaseEmailMessage("Welcome to the NFL Fantasy Football Playoff Challenge Website", user.Email);
        email.TextBody = message;

        await _resend.EmailSendAsync(email);
    }

    private EmailMessage GetBaseEmailMessage(string subject, string toAddress)
    {
        var email = new EmailMessage();
        email.From = "onboarding@resend.dev";
        email.To = toAddress;
        email.Subject = subject;
        return email;
    }
}
