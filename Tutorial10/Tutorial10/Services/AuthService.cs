using System.Security.Claims;
using Microsoft.AspNetCore.Identity.Data;
using Tutorial10.Properties.Contracts;
using Tutorial10.Properties.DbContext;
using Tutorial10.Properties.Helpers;
using Tutorial10.Properties.Models;

namespace Tutorial10.Properties.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    private readonly AppDbContext _dbContext;

    private readonly SecurityHelpers _securityHelpers;

    public AuthService(IConfiguration configuration, AppDbContext dbContext, SecurityHelpers securityHelpers)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _securityHelpers = securityHelpers;
    }

    public void RegisterUser(RegisterUserRequest model)
    {
        if (_dbContext.Users.Any(e => e.Username == model.Username))
        {
            throw new Exception("This username is already taken");
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

        if (user == null) throw new Exception("Username is incorrect.");
        
        var hashedPassword = _securityHelpers.GetHashedPasswordWithSalt(model.Password, user.Salt);
        
        if (hashedPassword != user.Password) throw new Exception("Password is incorrect.");
        
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.IdUser)),
            new Claim(ClaimTypes.UserData, user.Username),
            new Claim(ClaimTypes.Role, "User")
        };
        
        var accessToken = _securityHelpers.GenerateAccessToken(userClaims);
        var refreshToken = _securityHelpers.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExp = DateTime.UtcNow.AddDays(30);
        _dbContext.SaveChanges();

        return (accessToken, refreshToken);
    }
    
}