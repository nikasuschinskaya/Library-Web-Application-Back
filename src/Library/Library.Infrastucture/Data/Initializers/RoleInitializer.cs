using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Enums.Extentions;
using Library.Infrastucture.Data.Initializers.Base;

namespace Library.Infrastucture.Data.Initializers;

public class RoleInitializer : BaseInitializer<Role>
{
    public RoleInitializer() : base(
    [
        new Role(Roles.Admin.StringValue()),
        new Role(Roles.User.StringValue())
    ])
    {
    }

}
