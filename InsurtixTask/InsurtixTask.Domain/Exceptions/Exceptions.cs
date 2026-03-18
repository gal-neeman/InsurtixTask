namespace InsurtixTask.Domain.Exceptions;

public class NoDbException : Exception
{
    public NoDbException(string fileLocation) : 
        base($"No XML file was found at {fileLocation}") { }
}

public class BookNotFoundException: Exception
{
    public BookNotFoundException(string isbn) : base($"No book found with isbn '{isbn}'") { }
}
