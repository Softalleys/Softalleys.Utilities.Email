namespace Softalleys.Utilities.Email.Exceptions;

/// <summary>
/// Represents an exception that is thrown when an error occurs during template rendering.
/// </summary>
public class RenderErrorException : Exception
{
    /// <summary>
    /// Gets or sets the type of error that occurred during rendering.
    /// </summary>
    public required string ErrorType { get; set; }

    /// <summary>
    /// Gets the exception message that describes the rendering error.
    /// </summary>
    public override string Message { get; } = "An error occurred while rendering the template.";
}