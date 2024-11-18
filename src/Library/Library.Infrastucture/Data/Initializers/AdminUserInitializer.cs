using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Exceptions;
using Library.Domain.Extentions;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Auth;
using Library.Domain.Interfaces.Repositories;
using Library.Domain.Specifications;
using Library.Domain.Specifications.Roles;
using Library.Infrastucture.Data.Initializers.Base;

namespace Library.Infrastucture.Data.Initializers;

public class AdminUserInitializer : BaseInitializer<User>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;

    public AdminUserInitializer(IRepository<User> userRepository,
                                IRepository<Role> roleRepository,
                                IRepository<RefreshToken> refreshTokenRepository,
                                IUnitOfWork unitOfWork,
                                IPasswordHasher passwordHasher)
        : base(userRepository, unitOfWork)
    {
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task InitializeAdminAsync(CancellationToken cancellationToken = default)
    {
        var spec = new RoleByNameSpecification(Roles.Admin.StringValue());
        var adminRole = await _roleRepository.GetBySpecAsync(spec, cancellationToken)
            ?? throw new EntityNotFoundException("Admin role not found.");

        var adminUser = new User(
            name: "nika_susch2003",
            email: "nikaAdmin@gmail.com",
            password: _passwordHasher.HashPassword("Nika2003!"),
            role: adminRole
        );

        await InitializeAsync([adminUser], cancellationToken);

        var refreshTokenExists = (await _refreshTokenRepository.ListAsync(new EmptySpecification<RefreshToken>(), cancellationToken)).Any();
        if (!refreshTokenExists)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = adminUser.Id,
                ExpiryDate = DateTime.UtcNow.AddDays(7)
            };

            _refreshTokenRepository.Create(refreshToken);
        }
    }
}
