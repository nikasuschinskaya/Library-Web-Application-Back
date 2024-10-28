using Bogus;
using FluentAssertions;
using Library.Domain.Entities;
using Library.Infrastucture.Data;
using Library.Infrastucture.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Library.UnitTests;

[TestFixture]
public class RefreshTokenRepositoryTests : IDisposable
{
    private LibraryDbContext _context;
    private EntityFrameworkRepository<RefreshToken> _repository;
    private Faker<RefreshToken> _refreshTokenFaker;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryTestDb")
            .Options;

        _context = new LibraryDbContext(options);
        _repository = new EntityFrameworkRepository<RefreshToken>(_context);

        _refreshTokenFaker = new Faker<RefreshToken>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Token, f => f.Internet.Password())
            .RuleFor(r => r.UserId, f => Guid.NewGuid())
            .RuleFor(r => r.ExpiryDate, f => f.Date.Future())
            .RuleFor(r => r.IsRevoked, f => f.Random.Bool());
    }

    [Test]
    public async Task Create_ShouldAddRefreshToken()
    {
        // Arrange
        var refreshToken = _refreshTokenFaker.Generate();

        // Act
        _repository.Create(refreshToken);
        await _context.SaveChangesAsync();

        // Assert
        var result = await _repository.GetByIdAsync(refreshToken.Id);
        result.Should().NotBeNull();
        result.Token.Should().Be(refreshToken.Token);
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnRefreshToken_WhenExists()
    {
        // Arrange
        var refreshToken = _refreshTokenFaker.Generate();
        _repository.Create(refreshToken);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(refreshToken.Id);

        // Assert
        result.Should().NotBeNull();
        result.Token.Should().Be(refreshToken.Token);
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = await _repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public async Task GetAll_ShouldReturnAllRefreshTokens()
    {
        // Arrange
        var refreshToken1 = _refreshTokenFaker.Generate();
        var refreshToken2 = _refreshTokenFaker.Generate();

        _repository.Create(refreshToken1);
        _repository.Create(refreshToken2);
        await _context.SaveChangesAsync();

        // Act
        var result = _repository.GetAll().ToList();

        // Assert
        result.Should().HaveCount(2);
    }

    [Test]
    public async Task Delete_ShouldRemoveRefreshToken()
    {
        // Arrange
        var refreshToken = _refreshTokenFaker.Generate();
        _repository.Create(refreshToken);
        await _context.SaveChangesAsync();

        // Act
        _repository.Delete(refreshToken);
        await _context.SaveChangesAsync();

        // Assert
        var result = await _repository.GetByIdAsync(refreshToken.Id);
        result.Should().BeNull();
    }

    [Test]
    public async Task Update_ShouldModifyRefreshToken()
    {
        // Arrange
        var refreshToken = _refreshTokenFaker.Generate();
        _repository.Create(refreshToken);
        await _context.SaveChangesAsync();

        // Act
        refreshToken.Token = "updated-token";
        _repository.Update(refreshToken);
        await _context.SaveChangesAsync();

        // Assert
        var result = await _repository.GetByIdAsync(refreshToken.Id);
        result.Token.Should().Be("updated-token");
    }

    [TearDown]
    public void TearDown()
    {
        _context.RefreshTokens.RemoveRange(_context.RefreshTokens);
        _context.SaveChanges();

        _context.Dispose();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}