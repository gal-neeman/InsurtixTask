using AutoMapper;
using InsurtixTask.Application.DTOs;
using InsurtixTask.Application.Interfaces;
using InsurtixTask.Application.RequestObjects;
using InsurtixTask.Domain.Entities;
using InsurtixTask.Domain.Exceptions;

namespace InsurtixTask.Application.Services;

public class BookService : IBookService
{
    private readonly IBookDao _bookDao;
    private readonly IMapper _mapper;

    public BookService(IBookDao bookDao, IMapper mapper)
    {
        _bookDao = bookDao;
        _mapper = mapper;
    }

    public async Task AddBookAsync(BookRequest bookRequest)
    {
        var bookStore = await _bookDao.GetAllBooksAsync();

        var book = _mapper.Map<BookRequest, Book>(bookRequest);
        bookStore.Books.Add(book);

        await _bookDao.SaveAllAsync(bookStore);
    }

    public async Task DeleteBookByIsbnAsync(string isbn)
    {
        var books = await _bookDao.GetAllBooksAsync();
        var bookToDelete = books.Books.FirstOrDefault(b => b.Isbn == isbn);

        if (bookToDelete == null)
            throw new BookNotFoundException(isbn);

        books.Books = books.Books.Where(b => b.Isbn != isbn).ToList();

        await _bookDao.SaveAllAsync(books);
    }

    public async Task<List<BookDTO>> GetAllBooksAsync()
    {
        var bookStore = await _bookDao.GetAllBooksAsync();
        var books = _mapper.Map<List<Book>, List<BookDTO>>(bookStore.Books);

        return books;
    }

    public async Task<BookDTO> GetBookByIsbnAsync(string isbn)
    {
        var bookStore = await _bookDao.GetAllBooksAsync();
        var book = bookStore.Books.FirstOrDefault(b => b.Isbn == isbn);

        if (book == null)
            throw new BookNotFoundException(isbn);

        var bookDto = _mapper.Map<Book, BookDTO>(book);
        return bookDto;
    }

    public Task UpdateBookByIsbnAsync(BookRequest book)
    {
        throw new NotImplementedException();
    }
}
