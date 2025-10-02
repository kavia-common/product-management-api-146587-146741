using NSwag;
using NSwag.Generation.Processors.Security;
using product_backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // We use a custom validation filter; suppress default automatic 400
        options.SuppressModelStateInvalidFilter = true;
    });

// Add repository
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

 // Add Swagger/OpenAPI (NSwag)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(settings =>
{
    settings.Title = "Product Management API";
    settings.Version = "v1";
    settings.Description = "A REST API for managing products with CRUD operations.\nTheme: Ocean Professional";
    settings.DocumentName = "v1";
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowCredentials()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware
app.UseCors("AllowAll");

// OpenAPI/Swagger
app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.Path = "/docs";
    config.DocumentTitle = "Product Management API • Ocean Professional";
});

// Health check endpoint
// PUBLIC_INTERFACE
app.MapGet("/", () => new { message = "Healthy" })
   .WithName("HealthCheck")
   .WithTags("Health");

app.MapControllers();

app.Run();