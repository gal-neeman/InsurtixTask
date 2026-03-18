using InsurtixTask.API.RequestObjects;
using Microsoft.AspNetCore.Mvc;

namespace InsurtixTask.API.Controllers;

[ApiController]
public class BooksController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        return Ok();
    }

    [HttpGet("{isbn}")]
    public async Task<IActionResult> GetBookByIsbnAsync()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddBooksAsync(BookRequest bookRequest)
    {

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
