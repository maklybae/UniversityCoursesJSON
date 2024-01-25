namespace JSONCourse;

/// <summary>
/// Represents an exception specific to JSON parsing in the JSONCourse namespace.
/// </summary>
public class JSONParserException: ArgumentException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JSONParserException"/> class.
    /// </summary>
    public JSONParserException() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="JSONParserException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public JSONParserException(string message) : base(message) { }
}