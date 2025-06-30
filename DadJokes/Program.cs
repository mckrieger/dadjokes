using DadJokes.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add Http Client
builder.Services.AddHttpClient("ICanHazDadJoke", httpClient =>
{
    httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "DadJokes Project (https://github.com/mckrieger/dadjokes)");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    httpClient.BaseAddress = new Uri("https://icanhazdadjoke.com/");
});
builder.Services.AddScoped<IJokeService, JokeService>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
