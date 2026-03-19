using InsurtixTask.Application.DTOs;

namespace InsurtixTask.Application.Interfaces;

public interface IReportService
{
    public string GetHtmlReportFromBooks(IEnumerable<BookDTO> books);
    public string GetHtmlReportFromBook(BookDTO book);
}
