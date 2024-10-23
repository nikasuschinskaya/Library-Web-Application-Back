using AutoMapper;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Presentation.Requests;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetUserByIdAsync(userId, cancellationToken);
        var response = _mapper.Map<User, UserResponse>(user);
        return user is not null ? Ok(response) : BadRequest();
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<UserResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetAllUsersAsync(cancellationToken);
        var response = _mapper.Map<IEnumerable<UserResponse>>(users);
        return Ok(response);
    }

    [HttpGet("by-email")]
    [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetByEmailAsync(email, cancellationToken);
        var response = _mapper.Map<User, UserResponse>(user);
        return user is not null ? Ok(response) : BadRequest("User not found.");
    }

    [HttpPut("update/{userId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetUserByIdAsync(userId, cancellationToken);

        if (user == null) return BadRequest("User not found.");

        _mapper.Map(request, user);
        await _userService.Update(user, cancellationToken);
        return NoContent();
    }

}
