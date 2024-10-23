using Library.Application.Interfaces.Common;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastucture.Data.Initializers
{
    public class DbContextInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInitializer<Role> _roleInitializer;
        private readonly IInitializer<Author> _authorInitializer;
        private readonly IInitializer<Book> _bookInitializer;

        public DbContextInitializer(IUnitOfWork unitOfWork,
                                    IInitializer<Role> roleInitializer,
                                    IInitializer<Author> authorInitializer,
                                    IInitializer<Book> bookInitializer)
        {
            _unitOfWork = unitOfWork;
            _roleInitializer = roleInitializer;
            _authorInitializer = authorInitializer;
            _bookInitializer = bookInitializer;
        }

        public async Task InitializeAsync()
        {
            _roleInitializer.InitializeAsync(_unitOfWork);

            await _authorInitializer.InitializeAsync(_unitOfWork); 

            var authors = await _unitOfWork.Repository<Author>().GetAll().ToListAsync();

            if (!authors.Any())
            {
                throw new InvalidOperationException("Authors should be initialized before books.");
            }

            var bookInitializer = new BookInitializer(authors);
            await bookInitializer.InitializeAsync(_unitOfWork); 

        }


        //public void Initialize()
        //{
        //    _roleInitializer.Initialize(_unitOfWork);

        //    _authorInitializer.Initialize(_unitOfWork);

        //    _unitOfWork.CompleteAsync();

        //    var authors = _unitOfWork.Repository<Author>().GetAll().ToList();

        //    if (!authors.Any())
        //    {
        //        throw new InvalidOperationException("Authors should be initialized before books.");
        //    }

        //    var bookInitializer = new BookInitializer(authors);
        //    bookInitializer.Initialize(_unitOfWork);

        //    _unitOfWork.CompleteAsync();
        //}
    }
}
