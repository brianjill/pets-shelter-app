using PetsShelterApi.AzureAi;
using PetsShelterApi.Utilities;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.Configure<AzureAiServicesApiOptions>(
    builder.Configuration.GetSection(AzureAiServicesApiOptions.AzureAiServicesApi));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICommentService, CommentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsLocal())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet Shelter API V1");
        // optional: serve Swagger UI at app root
        c.RoutePrefix = String.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();