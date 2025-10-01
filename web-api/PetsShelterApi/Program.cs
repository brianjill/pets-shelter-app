using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using PetsShelterApi.AzureAi;
using PetsShelterApi.Utilities;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .Configure<AzureAiChatOptions>(builder.Configuration.GetSection(AzureAiChatOptions.AzureAiChatApi))
    .Configure<AzureAiHubOptions>(builder.Configuration.GetSection(AzureAiHubOptions.AzureAiHubApi));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICommentService, CommentService>()
    .AddScoped<IChatService, ChatService>()
    .AddScoped<ITextAnalyticsProvider, TextAnalyticsProvider>();

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