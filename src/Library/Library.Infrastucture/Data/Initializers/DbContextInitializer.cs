﻿using Library.Application.Interfaces;
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

        public void Initialize(UnitOfWork unitOfWork)
        {
            _roleInitializer.Initialize(unitOfWork);
        }
    }
}