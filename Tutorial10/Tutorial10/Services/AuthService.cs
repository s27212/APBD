using System.Security.Claims;
using MedApp.Context;
using Microsoft.IdentityModel.Tokens;
using Tutorial10.Exceptions;
using Tutorial10.Properties.Contracts;
using Tutorial10.Properties.Helpers;
using Tutorial10.Properties.Models;

namespace Tutorial10.Properties.Services;

public class AuthService : IAuthService
{

    private readonly MedDbContext _dbContext;

    private readonly SecurityHelpers _securityHelpers;

    public AuthService(MedDbContext dbContext, SecurityHelpers securityHelpers)
    {
        _dbContext = dbContext;
        _securityHelpers = securityHelpers;
    }

    public void RegisterUser(RegisterUserRequest model)
    {
        if (_dbContext.Users.Any(e => e.Username == model.Username))
        {
            throw new InvalidRequestDataException("This username is already taken");
        }
        
        var (hashedPassword, salt) = _securityHelpers.GetHashedPasswordAndSalt(model.Password);

        var user = new AppUser
        {
            Username = model.Username,
            Password = hashedPassword,
            Salt = salt
        };

        _dbContext.Add(user);
        _dbContext.SaveChanges();
    }

    public (string accessToken, string refreshToken) LoginUser(LoginUserRequest model)
    {
        var user = _dbContext.Users.FirstOrDefault(e => e.Username == model.Username);
        if (user == null) throw new InvalidRequestDataException("Username is incorrect.");
        
        var hashedPassword = _securityHelpers.GetHashedPasswordWithSalt(model.Password, user.Salt);
        if (hashedPassword != user.Password) throw new InvalidRequestDataException("Password is incorrect.");

        var (accessToken, refreshToken) = GenerateTokensForUser(user);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(30);
        _dbContext.SaveChanges();

        return (accessToken, refreshToken);
    }

    public (string accessToken, string refreshToken) RefreshToken(string refreshToken)
    {
        var user = _dbContext.Users.FirstOrDefault(e => e.RefreshToken == refreshToken);
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token.");
        }
        
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }

        var (accessToken, newRefreshToken) = GenerateTokensForUser(user);
        user.RefreshToken = refreshToken;
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(30);
        _dbContext.SaveChanges();

        return (accessToken, newRefreshToken);
    }

    private (string, string) GenerateTokensForUser(AppUser user)
    {
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.IdUser)),
            new Claim(ClaimTypes.UserData, user.Username),
            new Claim(ClaimTypes.Role, "User")
        };
        
        var accessToken = _securityHelpers.GenerateAccessToken(userClaims);
        var refreshToken = _securityHelpers.GenerateRefreshToken();
        return (accessToken, refreshToken);
    }
}