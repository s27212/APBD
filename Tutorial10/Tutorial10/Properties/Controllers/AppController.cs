using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tutorial10.Properties.Contracts;
using Tutorial10.Properties.Services;

namespace Tutorial10.Properties.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppController : ControllerBase
{
    private readonly IAuthService _service;

    public AppController(IAuthService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest model)
    {
        _service.RegisterUser(model);
        
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] LoginUserRequest model)
    {
        var (accessToken, refreshToken) = _service.LoginUser(model);
        
        return Ok(new {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
    
    
}