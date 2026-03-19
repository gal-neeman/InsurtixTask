namespace InsurtixTask.Application.RequestObjects;

public class BookRequest
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
    public string Value { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}