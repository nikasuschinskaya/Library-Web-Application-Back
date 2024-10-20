using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Application.Services.Base;
using Library.Domain.Entities;

namespace Library.Application.Services;

public class AuthService : IAuthService
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IJwtTokenService jwtTokenService,
                       IUserService userService,
                       IUnitOfWork unitOfWork,
                       IPasswordHasher passwordHasher)
    {
        _jwtTokenService = jwtTokenService;
        _userService = userService;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userService.GetByEmailAsync(email, cancellationToken);

        if (existingUser != null) throw new Exception("User already exists.");

        var user = new User
        {
            Name = name,
            Email = email,
            Password = _passwordHasher.HashPassword(password)
        };

        _unitOfWork.Repository<User>().Create(user);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return user;
    }

    public async Task<TokenResponse> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetByEmailAsync(email, cancellationToken);
        if (user == null || _passwordHasher.VerifyPassword(user.Password, password) == false)
        {
            throw new Exception("Invalid credentials.");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString());
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7)
        };

        await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return new TokenResponse(accessToken, refreshToken.Token);
    }

    public async Task<TokenResponse> RefreshAccessTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var storedToken = await _unitOfWork.RefreshTokenRepository.GetByTokenAsync(refreshToken, cancellationToken);
        if (storedToken == null || storedToken.ExpiryDate <= DateTime.UtcNow || storedToken.IsRevoked)
        {
            throw new Exception("Invalid refresh token.");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(storedToken.UserId.ToString());
        return new TokenResponse(accessToken, refreshToken);
    }
}

