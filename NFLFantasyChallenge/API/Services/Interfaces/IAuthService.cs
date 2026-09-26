using NFLFantasyChallenge.API.DTOs.Auth;

namespace NFLFantasyChallenge.API.Services.Interfaces
{
    public interface IAuthService
    {
        public Task Signup(SignupDTO signupDTO);

        public Task<UserLoginModel> Login(LoginDTO loginDTO);
    }
}
