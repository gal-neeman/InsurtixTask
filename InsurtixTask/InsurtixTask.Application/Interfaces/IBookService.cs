using InsurtixTask.Application.DTOs;
using InsurtixTask.Application.RequestObjects;

namespace InsurtixTask.Application.Interfaces;

public interface IBookService
{
    public Task<List<BookDTO>> GetAllBooksAsync();
    public Task<BookDTO> GetBookByIsbnAsync(string isbn);
    public Task UpdateBookByIsbnAsync(BookRequest book);
    public Task AddBookAsync(BookRequest book);
    public Task DeleteBookByIsbnAsync(string isbn);
}
