using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Extentions;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastucture.Data.Initializers.Base;

namespace Library.Infrastucture.Data.Initializers;

public class RoleInitializer : BaseInitializer<Role>
{
    public RoleInitializer(IRepository<Role> repository, IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }

    public async Task InitializeRolesAsync(CancellationToken cancellationToken = default)
    {
        var predefinedRoles = new List<Role>
        {
            new(Roles.Admin.StringValue()),
            new(Roles.User.StringValue())
        };

        await InitializeAsync(predefinedRoles, cancellationToken);
    }
}
