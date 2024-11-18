using FluentAssertions;
using Library.Domain.Interfaces.Auth;
using Library.Infrastucture.Auth;

namespace Library.UnitTests;

[TestFixture]
public class PasswordHasherTests
{
    private IPasswordHasher _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new PasswordHasher();
    }

    [Test]
    [TestCase("password", "password")]
    [TestCase("p4ssw0rd123", "p4ssw0rd123")]
    public void VerifyPassword_ShouldReturnTrue_IfSamePasswordsProvided(string passwordToHash, string passwordToVerify)
    {
        // Arrange
        var hashedPassword = _sut.HashPassword(passwordToHash);

        // Act
        var result = _sut.VerifyPassword(passwordToVerify, hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    [TestCase("password", "other_password")]
    [TestCase("p4ssw0rd123", "p4ssw0rd321")]
    public void VerifyPassword_ShouldReturnFalse_IfDifferentPasswordsProvided(string passwordToHash, string passwordToVerify)
    {
        // Arrange
        var hashedPassword = _sut.HashPassword(passwordToHash);

        // Act
        var result = _sut.VerifyPassword(passwordToVerify, hashedPassword);

        // Assert
        result.Should().BeFalse();
    }
}
