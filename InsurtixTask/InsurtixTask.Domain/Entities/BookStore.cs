using System.Xml.Serialization;

namespace InsurtixTask.Domain.Entities;

[XmlRoot("bookstore")]
public class BookStore
{
    [XmlElement("book")]
    public List<Book> Books { get; set; } = [];
}
