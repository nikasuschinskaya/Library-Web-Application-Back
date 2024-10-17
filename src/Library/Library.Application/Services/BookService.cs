using Library.Application.Interfaces;
using Library.Domain.Entities;

namespace Library.Application.Services
{
    public class BookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddBookAsync(Book book)
        {
            _unitOfWork.Repository<Book>().Create(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _unitOfWork.Repository<Book>().Update(book);
            await _unitOfWork.CompleteAsync();
        }
    }
}
