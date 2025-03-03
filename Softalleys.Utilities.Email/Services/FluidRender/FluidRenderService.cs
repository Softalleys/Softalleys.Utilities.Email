using Fluid;
using Softalleys.Utilities.Email.Exceptions;

namespace Softalleys.Utilities.Email.Services.FluidRender;

public class FluidRenderService : IHtmlViewRenderService
{
    public async Task<string> RenderHtmlAsync<TModel>(string htmlTemplate, TModel viewModel) where TModel : class
    {
        if (string.IsNullOrWhiteSpace(htmlTemplate))
        {
            throw new ArgumentNullException(nameof(htmlTemplate));
        }

        try
        {
            var parser = new FluidParser();

            if (!parser.TryParse(htmlTemplate, out var template))
                throw new RenderErrorException
                {
                    ErrorType = "invalid_template"
                };
            
            var context = new TemplateContext(viewModel);
            var result = await template.RenderAsync(context);
            Console.WriteLine(result);
            return result ?? throw new RenderErrorException
            {
                ErrorType = "invalid_template"
            };
        }
        catch (Exception)
        {
            throw new RenderErrorException
            {
                ErrorType = "invalid_request_object",
            };
        }
    }
}