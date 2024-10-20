using Library.Application.Services.Base;
using Library.Presentation.Requests;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        try
        {
            var tokenResponse = await _authService.RefreshAccessTokenAsync(refreshToken, cancellationToken);
            return Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}



//    [HttpPost("register")]
//    public IActionResult Register(RegisterRequest request)
//    {
//        var authResult = _authenticationService.RegisterAsync(
//            request.Username,
//            request.Email,
//            request.Password);

//        var response = new AuthenticationResponse(
//            authResult.Result.Id,
//            authResult.Result.Username,
//            authResult.Result.Email,
//            authResult.Result.Password,
//            authResult.Result.Token
//            );

//        return Ok(response);
//    }

//    [HttpPost("login")]
//    public IActionResult Login(LoginRequest request)
//    {
//        var authResult = _authenticationService.LoginAsync(
//            request.Email,
//            request.Password);

//        var response = new AuthenticationResponse(
//            authResult.Result.Id,
//            authResult.Result.Username,
//            authResult.Result.Email,
//            authResult.Result.Password,
//            authResult.Result.Token
//            );

//        return Ok(response);
//    }
//}
