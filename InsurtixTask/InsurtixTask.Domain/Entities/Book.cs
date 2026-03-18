using System.Xml.Serialization;

namespace InsurtixTask.Domain.Entities;

[XmlRoot("book")]
public class Book
{
    [XmlAttribute("category")]
    public string Category { get; set; } = String.Empty;
    [XmlElement("isbn")]
    public string Isbn { get; set; } = String.Empty;
    [XmlElement("title")]
    public Title Title { get; set; } = new();
    [XmlElement("author")]
    public string Author { get; set; } = String.Empty;
    [XmlElement("year")]
    public int Year { get; set; }
    [XmlElement("price")]
    public decimal Price { get; set; }
}

public class Title
{
    [XmlAttribute("lang")]
    public string Language { get; set; } = String.Empty;
    [XmlText]
    public string Value { get; set; } = String.Empty;
}