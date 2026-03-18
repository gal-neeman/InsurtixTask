using InsurtixTask.Domain.Entities;

namespace InsurtixTask.Application.Interfaces;

public interface IBookDao
{
    public Task<BookStore> GetAllBooksAsync();
    public Task SaveAllAsync(BookStore books);
}
