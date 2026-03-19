using InsurtixTask.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsurtixTask.API.Controllers;

[ApiController]
public class ReportController : BaseApiController
{
    private readonly IBookService _bookService;
    private readonly IReportService _reportService;

    public ReportController(IBookService bookService, IReportService reportService)
    {
        _bookService = bookService;
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var books = await _bookService.GetAllBooksAsync();
        var html = _reportService.GetHtmlReportFromBooks(books);

        return Content(html, "text/html");
    }

    [HttpGet("{isbn}")]
    public async Task<IActionResult> GetBookByIsbnAsync([FromRoute] string isbn)
    {
        var book = await _bookService.GetBookByIsbnAsync(isbn);
        var html = _reportService.GetHtmlReportFromBook(book);

        return Content(html, "text/html");
    }
}
