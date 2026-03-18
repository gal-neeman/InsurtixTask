namespace InsurtixTask.API.RequestObjects;

public class BookRequest
{
    public string Category { get; set; } = String.Empty;
    public string Isbn { get; set; } = String.Empty;
    public Title Title { get; set; } = new();
    public List<string> Author { get; set; } = [];
    public int Year { get; set; }
    public decimal Money { get; set; }
}

public class Title
{
    public string TitleName { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}