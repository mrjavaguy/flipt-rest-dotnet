using Flipt.Rest;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetExample.Pages;

public class IndexModel : PageModel
{
    private readonly IFliptRestClient _flagService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IFliptRestClient flagService, ILogger<IndexModel> logger)
    {
        _flagService = flagService;
        _logger = logger;
    }

    public string BackgroundColor { get; set; } = "white"; // Default color

    public async Task OnGet()
    {
        var request = new EvaluationevaluationRequest
        {
            EntityId = Guid.NewGuid().ToString(),
            FlagKey = "BackgroundColor",
            Context = new Dictionary<string, string>
            {
                { "environment", "demo" }
            }
        };

        var response = await _flagService.EvaluateV1VariantAsync(request);

        if (response != null)
        {
            BackgroundColor = response.VariantKey.ToLowerInvariant();
        }   
    }
}
