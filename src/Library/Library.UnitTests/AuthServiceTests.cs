using FluentAssertions;
using Library.Application.Interfaces.Services;
using Library.Application.Services;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Library.UnitTests;

[TestFixture]
public class AuthServiceTests
{
    private Mock<IJwtTokenService> _jwtTokenServiceMock;
    private Mock<IUserService> _userServiceMock;
    private LibraryDbContext _context;
    private AuthService _authService;
    private readonly string _testEmail = "test@example.com";
    private readonly string _testPassword = "Password123!";

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new LibraryDbContext(options);
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _userServiceMock = new Mock<IUserService>();

        _authService = new AuthService(
            _jwtTokenServiceMock.Object,
            _userServiceMock.Object,
            new UnitOfWork(_context),
            new PasswordHasher()
        );
    }

    [Test]
    public async Task RegisterAsync_ShouldCreateUserAndReturnTokens()
    {
        // Arrange
        var userName = "Test User";

        var userRole = new Role { Name = nameof(Roles.User) };
        await _context.Roles.AddAsync(userRole);
        await _context.SaveChangesAsync();

        var user = new User(userName, _testEmail, new PasswordHasher().HashPassword(_testPassword), userRole);

        _userServiceMock.Setup(x => x.GetByEmailAsync(_testEmail, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);

        _jwtTokenServiceMock.Setup(x => x.GenerateAccessToken(It.IsAny<string>()))
            .Returns("access_token");

        // Act
        var result = await _authService.RegisterAsync(userName, _testEmail, _testPassword);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().Be("access_token");
        result.RefreshToken.Should().NotBeNullOrEmpty();

        var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == _testEmail);
        createdUser.Should().NotBeNull();
        createdUser.Email.Should().Be(_testEmail);
        createdUser.Role.Should().NotBeNull(); 
        createdUser.Role.Name.Should().Be(nameof(Roles.User)); 
    }


    [Test]
    public async Task LoginAsync_ShouldReturnTokens_WhenCredentialsAreValid()
    {
        // Arrange
        var userRole = new Role { Name = nameof(Roles.User) }; 
        var user = new User("Test User", _testEmail, new PasswordHasher().HashPassword(_testPassword), userRole);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        _userServiceMock.Setup(x => x.GetByEmailAsync(_testEmail, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        _jwtTokenServiceMock.Setup(x => x.GenerateAccessToken(user.Id.ToString())).Returns("access_token");

        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            UserId = user.Id,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        // Act
        var result = await _authService.LoginAsync(_testEmail, _testPassword);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().Be("access_token");
        result.RefreshToken.Should().Be(refreshToken.Token);
    }

    [Test]
    public async Task RefreshAccessTokenAsync_ShouldReturnNewAccessToken_WhenTokenIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userRole = new Role { Name = nameof(Roles.User) }; 
        var user = new User("Test User", _testEmail, new PasswordHasher().HashPassword(_testPassword), userRole) { Id = userId };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var refreshToken = new RefreshToken
        {
            Token = "valid_refresh_token",
            UserId = userId,
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        _jwtTokenServiceMock.Setup(x => x.ValidateToken("valid_access_token")).Returns(true);
        _jwtTokenServiceMock.Setup(x => x.GenerateAccessToken(userId.ToString())).Returns("new_access_token");

        // Act
        var result = await _authService.RefreshAccessTokenAsync("valid_access_token", "valid_refresh_token");

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().Be("new_access_token");
    }

    [Test]
    public async Task RevokeRefreshTokenAsync_ShouldRevokeToken_WhenTokenIsValid()
    {
        // Arrange
        var refreshToken = new RefreshToken
        {
            Token = "valid_refresh_token",
            UserId = Guid.NewGuid(),
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        // Act
        await _authService.RevokeRefreshTokenAsync("valid_refresh_token");

        // Assert
        var revokedToken = await _context.RefreshTokens.FindAsync(refreshToken.Id);
        revokedToken.Should().NotBeNull();
        revokedToken.IsRevoked.Should().BeTrue();
    }

    [Test]
    public async Task RevokeRefreshTokenAsync_ShouldThrowException_WhenTokenIsAlreadyRevoked()
    {
        // Arrange
        var refreshToken = new RefreshToken
        {
            Token = "valid_refresh_token",
            UserId = Guid.NewGuid(),
            ExpiryDate = DateTime.UtcNow.AddDays(7),
            IsRevoked = true
        };
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        // Act
        Func<Task> act = async () => await _authService.RevokeRefreshTokenAsync("valid_refresh_token");

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Token has already been revoked.");
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}