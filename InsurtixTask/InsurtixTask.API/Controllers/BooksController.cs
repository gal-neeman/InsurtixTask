using InsurtixTask.Application.Interfaces;
using InsurtixTask.Application.RequestObjects;
using Microsoft.AspNetCore.Mvc;

namespace InsurtixTask.API.Controllers;

[ApiController]
public class BooksController : BaseApiController
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();

        return Ok(books);
    }

    [HttpGet("{isbn}")]
    public async Task<IActionResult> GetBookByIsbnAsync()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddBooksAsync(BookRequest bookRequest)
    {
        await _bookService.AddBookAsync(bookRequest);

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBookAsync(BookRequest bookRequest)
    {

        return NoContent();
    }

    [HttpDelete("{isbn}")]
    public async Task<IActionResult> DeleteBookAsync([FromRoute] string isbn)
    {
        return NoContent();
    }
}
