using OperationsHub.Api.Features.Assets;
using OperationsHub.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<InMemoryOperationsDataStore>();
builder.Services.AddSingleton<AssetBatchStatusService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("web", policy =>
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("web");
app.UseAuthorization();
app.MapControllers();
app.Run();
