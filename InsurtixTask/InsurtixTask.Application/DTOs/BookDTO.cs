namespace InsurtixTask.Application.DTOs;

public class BookDTO
{
    public string Category { get; set; } = String.Empty;
    public string Isbn { get; set; } = String.Empty;
    public Title Title { get; set; } = new();
    public List<string> Author { get; set; } = [];
    public int Year { get; set; }
    public decimal Price { get; set; }
}
public class Title
{
    public string Language { get; set; } = String.Empty;
    public string Value { get; set; } = String.Empty;
}
