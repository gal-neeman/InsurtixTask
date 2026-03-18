using AutoMapper;
using InsurtixTask.Application.DTOs;
using InsurtixTask.Application.Interfaces;
using InsurtixTask.Application.RequestObjects;
using InsurtixTask.Domain.Entities;
using InsurtixTask.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace InsurtixTask.Application.Services;

public class BookService : IBookService
{
    private readonly IBookDao _bookDao;
    private readonly IMapper _mapper;
    private readonly ILogger<BookService> _logger;

    public BookService(IBookDao bookDao, IMapper mapper, ILogger<BookService> logger)
    {
        _bookDao = bookDao;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task AddBookAsync(BookRequest bookRequest)
    {
        _logger.LogInformation($"Trying to add book with ISBN: {bookRequest.Isbn}");
        var bookStore = await _bookDao.GetAllBooksAsync();

        if (bookStore.Books.FirstOrDefault(b => b.Isbn == bookRequest.Isbn) != null)
            throw new BookAlreadyExistsException(bookRequest.Isbn);

        var book = _mapper.Map<BookRequest, Book>(bookRequest);
        bookStore.Books.Add(book);

        await _bookDao.SaveAllAsync(bookStore);
        _logger.LogInformation($"Book with ISBN: {bookRequest.Isbn} added successfully");
    }

    public async Task DeleteBookByIsbnAsync(string isbn)
    {
        _logger.LogInformation($"Trying to delete book with ISBN: {isbn}");
        var books = await _bookDao.GetAllBooksAsync();
        var bookToDelete = books.Books.FirstOrDefault(b => b.Isbn == isbn);

        if (bookToDelete == null)
            throw new BookNotFoundException(isbn);

        books.Books = books.Books.Where(b => b.Isbn != isbn).ToList();

        await _bookDao.SaveAllAsync(books);
        _logger.LogInformation($"Book with ISBN: {isbn} deleted successfully");
    }

    public async Task<List<BookDTO>> GetAllBooksAsync()
    {
        _logger.LogInformation("Retrieving all books");
        var bookStore = await _bookDao.GetAllBooksAsync();
        var books = _mapper.Map<List<Book>, List<BookDTO>>(bookStore.Books);

        return books;
    }

    public async Task<BookDTO> GetBookByIsbnAsync(string isbn)
    {
        _logger.LogInformation($"Retrieving book with ISBN: {isbn}");
        var bookStore = await _bookDao.GetAllBooksAsync();
        var book = bookStore.Books.FirstOrDefault(b => b.Isbn == isbn);

        if (book == null)
            throw new BookNotFoundException(isbn);

        var bookDto = _mapper.Map<Book, BookDTO>(book);
        return bookDto;
    }

    public async Task UpdateBookByIsbnAsync(BookRequest book)
    {
        _logger.LogInformation($"Trying to update book with ISBN: {book.Isbn}");
        var books = await _bookDao.GetAllBooksAsync();
        var bookToUpdate = books.Books.FirstOrDefault(b => b.Isbn == book.Isbn);

        if (bookToUpdate == null)
            throw new BookNotFoundException(book.Isbn);

        _mapper.Map(book, bookToUpdate);

        await _bookDao.SaveAllAsync(books);
        _logger.LogInformation($"Book with ISBN: {book.Isbn} updated successfully");
    }
}
