using AutoMapper;
using Library.Domain.Interfaces.Auth;
using Library.Domain.Models;
using Library.Presentation.Requests;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthenticationController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }


    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthTokensResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var authTokens = await _authService.RegisterAsync(request.Name, request.Email, request.Password, cancellationToken);
        var response = _mapper.Map<AuthTokens, AuthTokensResponse>(authTokens);
        return authTokens is not null ? Ok(response) : BadRequest();
    }


    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthTokensResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var authTokens = await _authService.LoginAsync(request.Email, request.Password, cancellationToken);
        var response = _mapper.Map<AuthTokens, AuthTokensResponse>(authTokens);
        return authTokens is not null ? Ok(response) : Unauthorized();
    }


    [HttpPost("refresh-access-token")]
    [ProducesResponseType(typeof(AuthTokensResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RefreshToken([FromBody] AuthTokensRequest request, CancellationToken cancellationToken)
    {
        var authTokens = await _authService.RefreshAccessTokenAsync(request.AccessToken, request.RefreshToken, cancellationToken);
        var response = _mapper.Map<AuthTokens, AuthTokensResponse>(authTokens);
        return authTokens is not null ? Ok(response) : BadRequest();
    }
}