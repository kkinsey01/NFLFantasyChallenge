namespace NFLFantasyChallenge.API.DTOs.Auth
{
    public class SignupDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
