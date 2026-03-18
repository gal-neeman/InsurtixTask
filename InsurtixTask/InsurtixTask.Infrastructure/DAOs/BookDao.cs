using InsurtixTask.Application.Interfaces;
using InsurtixTask.Domain.Entities;
using InsurtixTask.Domain.Exceptions;
using InsurtixTask.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace InsurtixTask.Infrastructure.DAOs;

public class BookDao : IBookDao
{
    private readonly string _filePath;
    private readonly string _bookStoreElementName;
    private readonly string _bookElementName;
    private readonly string _categoryAttributeName;
    private readonly string _isbnElementName;
    private readonly string _titleElementName;
    private readonly string _langAttributeName;
    private readonly string _authorElementName;
    private readonly string _yearElementName;
    private readonly string _priceElementName;

    public BookDao(IOptions<XmlDbOptions> options)
    {
        _filePath = options.Value.FileLocation;
        _bookStoreElementName = GetXmlName<BookStore>(nameof(BookStore));
        _bookElementName = GetXmlName<BookStore>(nameof(Book));
        _categoryAttributeName = GetXmlName<Book>(nameof(Book.Category));
        _isbnElementName = GetXmlName<Book>(nameof(Book.Isbn));
        _titleElementName= GetXmlName<Book>(nameof(Book.Title));
        _langAttributeName = GetXmlName<Title>(nameof(Title.Language));
        _authorElementName = GetXmlName<Book>(nameof(Book.Author));
        _yearElementName = GetXmlName<Book>(nameof(Book.Year));
        _priceElementName= GetXmlName<Book>(nameof(Book.Price));
    }

    public async Task<BookStore> GetAllBooksAsync()
    {
        if (!File.Exists(_filePath)) throw new NoDbException(_filePath);

        using var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
        var doc = await XDocument.LoadAsync(stream, LoadOptions.None, default);

        var library = new BookStore
        {
            Books = doc.Root?.Elements(_bookElementName).Select(b => new Book
            {
                Category = b.Attribute(_categoryAttributeName)?.Value ?? string.Empty,
                Isbn = b.Element(_isbnElementName)?.Value ?? string.Empty,
                Title = new Title
                {
                    Language = b.Element(_titleElementName)?.Attribute(_langAttributeName)?.Value ?? string.Empty,
                    Value = b.Element(_titleElementName)?.Value ?? string.Empty
                },
                Author = b.Element(_authorElementName)?.Value ?? string.Empty,
                Price = decimal.Parse(b.Element(_priceElementName)?.Value ?? "0"),
                Year = int.Parse(b.Element(_yearElementName)?.Value ?? "0")
            }).ToList() ?? new List<Book>()
        };

        return library;
    }

    public async Task SaveAllAsync(BookStore bookstore)
    {
        var doc = new XDocument(
            new XElement(_bookStoreElementName,
                bookstore.Books.Select(b => new XElement(_bookElementName,
                    new XAttribute(_categoryAttributeName, b.Category),
                    new XElement(_isbnElementName, b.Isbn),
                    new XElement(_titleElementName, new XAttribute(_langAttributeName, b.Title.Language), b.Title.Value),
                    new XElement(_authorElementName, b.Author),
                    new XElement(_priceElementName, b.Price),
                    new XElement(_yearElementName, b.Year)
                ))
            )
        );

        using var stream = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
        await doc.SaveAsync(stream, SaveOptions.None, default);
    }

    private static string GetXmlName<T>(string propertyName)
    {
        var property = typeof(T).GetProperty(propertyName);

        // Look for [XmlAttribute]
        var attr = property?.GetCustomAttributes(typeof(XmlAttributeAttribute), false)
                           .FirstOrDefault() as XmlAttributeAttribute;
        if (attr != null) return attr.AttributeName;

        // Look for [XmlElement]
        var element = property?.GetCustomAttributes(typeof(XmlElementAttribute), false)
                              .FirstOrDefault() as XmlElementAttribute;
        return element?.ElementName ?? propertyName.ToLower();
    }
}
