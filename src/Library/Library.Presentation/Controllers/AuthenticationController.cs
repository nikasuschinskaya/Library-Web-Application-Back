using Library.Application.Services.Base;
using Library.Presentation.Requests;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthenticationController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.RegisterAsync(
            request.Username,
            request.Email,
            request.Password);

        var response = new AuthenticationResponse(
            authResult.Result.Id,
            authResult.Result.Username,
            authResult.Result.Email,
            authResult.Result.Password,
            authResult.Result.Token
            );

        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.LoginAsync(
            request.Email,
            request.Password);

        var response = new AuthenticationResponse(
            authResult.Result.Id,
            authResult.Result.Username,
            authResult.Result.Email,
            authResult.Result.Password,
            authResult.Result.Token
            );

        return Ok(response);
    }
}
