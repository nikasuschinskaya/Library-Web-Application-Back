using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Infrastucture.Data.Initializers.Base;

namespace Library.Infrastucture.Data.Initializers
{
    public class RoleInitializer : BaseInitializer<Role>
    {
        public RoleInitializer() : base(
        [
            new Role(nameof(Roles.Admin)),
            new Role(nameof(Roles.User))
        ])
        {
        }

    }
}
