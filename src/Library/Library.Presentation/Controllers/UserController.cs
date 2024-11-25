using AutoMapper;
using Library.Application.Interfaces.UseCases.Users;
using Library.Domain.Entities;
using Library.Presentation.Requests;
using Library.Presentation.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IGetUserByIdUseCase _getUserByIdUseCase;
    private readonly IGetUserByEmailUseCase _getUserByEmailUseCase;
    private readonly IGetAllUsersUseCase _getAllUsersUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IMapper _mapper;

    public UserController(IGetUserByIdUseCase getUserByIdUseCase,
                          IGetUserByEmailUseCase getUserByEmailUseCase,
                          IGetAllUsersUseCase getAllUsersUseCase,
                          IUpdateUserUseCase updateUserUseCase,
                          IMapper mapper)
    {
        _getUserByIdUseCase = getUserByIdUseCase;
        _getUserByEmailUseCase = getUserByEmailUseCase;
        _getAllUsersUseCase = getAllUsersUseCase;
        _updateUserUseCase = updateUserUseCase;
        _mapper = mapper;
    }


    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _getUserByIdUseCase.ExecuteAsync(userId, cancellationToken);
        var response = _mapper.Map<User, UserResponse>(user);
        return Ok(response);
    }


    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<UserResponse>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = default)
    {
        var users = await _getAllUsersUseCase.ExecuteAsync(cancellationToken);
        var response = _mapper.Map<IEnumerable<UserResponse>>(users);
        return Ok(response);
    }


    [HttpGet("by-email")]
    [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email, CancellationToken cancellationToken = default)
    {
        var user = await _getUserByEmailUseCase.ExecuteAsync(email, cancellationToken);
        var response = _mapper.Map<User, UserResponse>(user);
        return Ok(response);
    }


    [HttpPut("update/{userId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserRequest request, CancellationToken cancellationToken = default)
    {
        var updatedUser = _mapper.Map<User>(request);
        await _updateUserUseCase.ExecuteAsync(userId, updatedUser, cancellationToken);
        return NoContent();
    }
}