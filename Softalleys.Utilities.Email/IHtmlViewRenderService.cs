using Softalleys.Utilities.Email.Exceptions;

namespace Softalleys.Utilities.Email;

/// <summary>
/// Interface for rendering HTML views.
/// </summary>
public interface IHtmlViewRenderService
{
    /// <summary>
    /// Renders an HTML template with the specified view model.
    /// </summary>
    /// <typeparam name="TModel">The type of the view model.</typeparam>
    /// <param name="htmlTemplate">The HTML template to render.</param>
    /// <param name="viewModel">The view model to use for rendering the template.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the rendered HTML string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="htmlTemplate"/> is null or empty.</exception>
    /// <exception cref="RenderErrorException">Thrown when an error occurs during rendering.</exception>
    public Task<string> RenderHtmlAsync<TModel>(string htmlTemplate, TModel viewModel) where TModel : class;
}