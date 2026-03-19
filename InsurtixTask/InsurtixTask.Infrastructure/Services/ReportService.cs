using InsurtixTask.Application.DTOs;
using InsurtixTask.Application.Interfaces;
using System.Text;

namespace InsurtixTask.Infrastructure.Services;

public class ReportService : IReportService
{
    public string GetHtmlReportFromBook(BookDTO book)
    {
        var sb = new StringBuilder();

        var style = GetHtmlTableStyle();
        sb.Append("<html><head>");
        sb.Append(style);
        sb.Append("</head>");

        sb.Append("<body><table border='1'>");
        var rows = GetBookTableRows();
        sb.Append(rows);

        var column = GetTableColumFromBook(book);
        sb.Append(column);
        sb.Append("</table></body></html>");

        return sb.ToString();
    }

    public string GetHtmlReportFromBooks(IEnumerable<BookDTO> books)
    {
        var sb = new StringBuilder();

        var style = GetHtmlTableStyle();
        sb.Append("<html><head>");
        sb.Append(style);
        sb.Append("</head>");

        sb.Append("<body><table border='1'>");
        var rows = GetBookTableRows();
        sb.Append(rows);

        foreach (var book in books)
        {
            var column = GetTableColumFromBook(book);
            sb.Append(column);
        }

        sb.Append("</table></body></html>");
        return sb.ToString();
    }

    private string GetTableColumFromBook(BookDTO book)
    {
        var sb = new StringBuilder();
        var authors = string.Join(", ", book.Author);

        sb.Append("<tr>");
        sb.Append($"<td>{book.Title.Value}</td>");
        sb.Append($"<td>{authors}</td>");
        sb.Append($"<td>{book.Category}</td>");
        sb.Append($"<td>{book.Year}</td>");
        sb.Append($"<td>{book.Price}</td>");
        sb.Append("</tr>");

        return sb.ToString();
    }

    private string GetBookTableRows()
    {
        var sb = new StringBuilder();
        sb.Append("<tr><th>Title</th><th>Author</th><th>Category</th><th>Year</th><th>Price</th></tr>");

        return sb.ToString();
    }

    private string GetHtmlTableStyle()
    {
        var sb = new StringBuilder();

        sb.Append("<style>");
        sb.Append(@"
            table { 
                width: 100%; 
                border-collapse: collapse; 
                font-family: Arial, sans-serif;
            }
            th, td { 
                border: 1px solid black; 
                padding: 8px; 
                text-align: left; 
            }
            th { 
                background-color: #f2f2f2; 
            }
        ");
        sb.Append("</style>");

        return sb.ToString();
    }
}
