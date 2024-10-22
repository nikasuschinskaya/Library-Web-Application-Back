using Library.Application.Interfaces.Common;
using Library.Domain.Entities;

namespace Library.Infrastucture.Data.Initializers
{
    public class DbContextInitializer
    {
        private readonly IInitializer<Role> _roleInitializer;

        public DbContextInitializer(IInitializer<Role> roleInitializer)
        {
            _roleInitializer = roleInitializer;
        }

        public void Initialize(IUnitOfWork unitOfWork)
        {
            _roleInitializer.Initialize(unitOfWork);
        }
    }
}
