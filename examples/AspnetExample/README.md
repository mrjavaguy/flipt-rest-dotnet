# ASPNET example

Shows using Flipt `Evaluate` endpoint.

## Setup

### Flipt Setup with Docker


1. Start Flipt in a docker container: The command provided runs Flipt in detached mode, mapping the necessary ports to allow local access to the Flipt UI and API.

```
docker run -d \
    -p 8080:8080 \
    -p 9000:9000 \
    docker.flipt.io/flipt/flipt:latest
```

2. Configure Flipt:

* Access the Flipt UI by navigating to http://localhost:8080 in your web browser.
* Create a new feature flag named BackgroundColor. This flag will control the dynamic background color of your webpage.
* Add variants for the flag, such as lightgreen and lightblue, to represent the possible background colors.
* Define a distribution rule for these variants, setting a 50/50 percentage split to evenly distribute the color variations among page refreshes.

### ASP.NET Application Setup

1. Prepare the ASP.NET Application:

* Navigate to the examples/AspnetExample directory within your project.
* Run the application using the `dotnet run` command.
* Open a web browser and go to the URL indicated in the console output (e.g., http://localhost:5046). The port may vary based on your application's configuration.

2. Experience Dynamic Background Color Changes:

* Refresh the browser multiple times to observe the background color changing between lightgreen and lightblue, demonstrating the feature flag in action.

## Detailed Implementation

### Setting Up the HttpClient

* In [Program.cs](examples/AspnetExample/Program.cs),configure the `HttpClient` for dependency injection. This setup allows your application to communicate with the Flipt API.

```
builder.Services.AddHttpClient<IFliptRestClient, FliptRestClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Flipt:BaseUrl"]!);
});
```

### Evaluating the Feature Flag

* In [Index.cshtml.cs](examples/AspnetExample/Pages/Index.cshtml.cs), construct an `EvaluationRequest` to determine which variant of the `BackgroundColor` flag should be applied. This request includes a unique `EntityId` for each evaluation, simulating different users or sessions, and a context to provide additional information for flag evaluation.

```
        var request = new EvaluationRequest
        {
            EntityId = Guid.NewGuid().ToString(),
            FlagKey = "BackgroundColor",
            Context = new Dictionary<string, string>
            {
                { "environment", "demo" }
            }
        };

        var (response, exception) = await _flagService.EvaluateVariantAsync(request);
        if (exception != null)
        {
            _logger.LogError(exception, "Error evaluating flag");
            return;
        }

        if (response != null)
        {
            BackgroundColor = response.VariantKey.ToLowerInvariant();
        }  
```
