using Tutorial10.Properties.Contracts;

namespace Tutorial10.Properties.Services;

public interface IAuthService
{
    public void RegisterUser(RegisterUserRequest model);
    (string accessToken, string refreshToken) LoginUser(LoginUserRequest model);
}