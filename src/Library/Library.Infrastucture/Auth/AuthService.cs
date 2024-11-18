using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Auth;
using Library.Domain.Models;
using Library.Domain.Specifications.RefreshTokens;
using Library.Domain.Specifications.Roles;
using Library.Domain.Specifications.Users;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastucture.Auth;

public class AuthService : IAuthService
{
    private readonly IJwtTokenService _jwtTokenService;
    //private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IJwtTokenService jwtTokenService,
                       //IUserService userService,
                       IUnitOfWork unitOfWork,
                       IPasswordHasher passwordHasher)
    {
        _jwtTokenService = jwtTokenService;
        //_userService = userService;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthTokens> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default)
    {
        var userSpec = new UserByEmailSpecification(email);
        var existingUser = await _unitOfWork.Users.GetBySpecAsync(userSpec, cancellationToken);

        if (existingUser != null) 
            throw new AlreadyExistsException($"User with {email} already exists.");

        //var userRole = await _unitOfWork.Roles.GetAll()
        //    .Where(r => r.Name == nameof(Roles.User))
        //    .FirstOrDefaultAsync(cancellationToken)
        //    ?? throw new EntityNotFoundException("User role not found");
        var roleSpec = new RoleByNameSpecification(nameof(Roles.User));
        var userRole = await _unitOfWork.Roles.GetBySpecAsync(roleSpec, cancellationToken)
            ?? throw new EntityNotFoundException("User role not found");

        var user = new User(name, email, _passwordHasher.HashPassword(password), userRole);
        _unitOfWork.Users.Create(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var accessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString());
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7)
        };

        _unitOfWork.RefreshTokens.Create(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthTokens(accessToken, refreshToken.Token);
    }

    public async Task<AuthTokens> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var userSpec = new UserByEmailSpecification(email);
        var user = await _unitOfWork.Users.GetBySpecAsync(userSpec, cancellationToken);

        if (user == null || _passwordHasher.VerifyPassword(password, user.Password) == false)
        {
            throw new Exception("Invalid credentials.");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString());

        //var refreshToken = await _unitOfWork.Repository<RefreshToken>().GetAll()
        //    .FirstOrDefaultAsync(rt => rt.UserId == user.Id && rt.ExpiryDate > DateTime.UtcNow && !rt.IsRevoked, cancellationToken);

        var tokenSpec = new ActiveRefreshTokenSpecification(user.Id);
        var refreshToken = await _unitOfWork.RefreshTokens.GetBySpecAsync(tokenSpec, cancellationToken);

        if (refreshToken == null)
        {
            refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            _unitOfWork.RefreshTokens.Create(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return new AuthTokens(accessToken, refreshToken.Token);
    }

    public async Task<AuthTokens> RefreshAccessTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
    {
        if (!_jwtTokenService.ValidateToken(accessToken))
        {
            throw new Exception("Invalid or expired access token.");
        }

        var tokenSpec = new RefreshTokenByValueSpecification(refreshToken);
        var storedToken = await _unitOfWork.RefreshTokens.GetBySpecAsync(tokenSpec, cancellationToken);
        //var storedToken = await _unitOfWork.Repository<RefreshToken>().GetAll()
        //    .FirstOrDefaultAsync(t => t.Token == refreshToken, cancellationToken);

        if (storedToken == null || storedToken.ExpiryDate <= DateTime.UtcNow || storedToken.IsRevoked)
            throw new Exception("Invalid refresh token.");

        var newAccessToken = _jwtTokenService.GenerateAccessToken(storedToken.UserId.ToString());

        return new AuthTokens(newAccessToken, refreshToken);
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        //var storedToken = await _unitOfWork.Repository<RefreshToken>().GetAll()
        //    .FirstOrDefaultAsync(t => t.Token == refreshToken, cancellationToken)
        //    ?? throw new Exception("Invalid refresh token.");
        var tokenSpec = new RefreshTokenByValueSpecification(refreshToken);
        var storedToken = await _unitOfWork.RefreshTokens.GetBySpecAsync(tokenSpec, cancellationToken)
            ?? throw new EntityNotFoundException("Refresh token is not found.");

        if (storedToken.IsRevoked)
            throw new Exception("Token has already been revoked.");

        storedToken.IsRevoked = true;

        _unitOfWork.RefreshTokens.Update(storedToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}