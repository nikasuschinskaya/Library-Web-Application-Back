using Library.Application.Interfaces.Common;
using Library.Application.Interfaces.Services;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Microsoft.EntityFrameworkCore;

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


    // с jwt токена доставать роль и проверять кажде действие
    // Каждый запрос проверять токен, если он жив, то пользователь активен + при каждом запросе доставать кто это и какие у него права 
    // Генерить токены при регистрации и возвращать только токен на реакт при авторизации и достем из него данные на юзере
    // При каждом запросе от ползвателя смотреть кто это
    // Любой мой запрос возвращает новый access токен,
    // заново входить, когда 15 мин прошло (ошибка) Forbidden 403

    public async Task<AuthTokens> RegisterAsync(string name, string email, string password, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userService.GetByEmailAsync(email, cancellationToken);

        if (existingUser != null) throw new Exception("User already exists.");

        var user = new User(name, email, _passwordHasher.HashPassword(password));
        _unitOfWork.Repository<User>().Create(user);
        await _unitOfWork.CompleteAsync(cancellationToken);

        var accessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString());
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7)
        };

        _unitOfWork.Repository<RefreshToken>().Create(refreshToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return new AuthTokens(accessToken, refreshToken.Token);
    }

    public async Task<AuthTokens> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        var user = await _userService.GetByEmailAsync(email, cancellationToken);
        if (user == null || _passwordHasher.VerifyPassword(user.Password, password) == false)
        {
            throw new Exception("Invalid credentials.");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user.Id.ToString());

        var refreshToken = await _unitOfWork.Repository<RefreshToken>().GetAll()
            .FirstOrDefaultAsync(rt => rt.UserId == user.Id && rt.ExpiryDate > DateTime.UtcNow && !rt.IsRevoked, cancellationToken);

        if (refreshToken == null)
        {
            refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            _unitOfWork.Repository<RefreshToken>().Create(refreshToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
        }

        return new AuthTokens(accessToken, refreshToken.Token);
    }

    public async Task<AuthTokens> RefreshAccessTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default)
    {
        if (!_jwtTokenService.ValidateToken(accessToken))
        {
            throw new Exception("Invalid or expired access token.");
        }

        var storedToken = await _unitOfWork.Repository<RefreshToken>().GetAll()
            .FirstOrDefaultAsync(t => t.Token == refreshToken, cancellationToken);

        if (storedToken == null || storedToken.ExpiryDate <= DateTime.UtcNow || storedToken.IsRevoked)
            throw new Exception("Invalid refresh token.");

        var newAccessToken = _jwtTokenService.GenerateAccessToken(storedToken.UserId.ToString());

        return new AuthTokens(newAccessToken, refreshToken);
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var storedToken = await _unitOfWork.Repository<RefreshToken>().GetAll()
            .FirstOrDefaultAsync(t => t.Token == refreshToken, cancellationToken)
            ?? throw new Exception("Invalid refresh token.");

        if (storedToken.IsRevoked)
            throw new Exception("Token has already been revoked.");

        storedToken.IsRevoked = true;

        _unitOfWork.Repository<RefreshToken>().Update(storedToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
    }
}

