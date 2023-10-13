using SlamCodeBlog.DotNetDITips.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ITestService, FirstTestService>();
//builder.Services.AddScoped<ITestService, FirstTestService>();
//builder.Services.AddSingleton<ITestService, FirstTestService>();

//builder.Services.AddTransient<ISubService, FirstSubService>();
//builder.Services.AddScoped<ISubService, FirstSubService>();
builder.Services.AddSingleton<ISubService, FirstSubService>();

//builder.Services.AddScoped<ITestService, TestServiceWithNoPublicCtsr>();
//builder.Services.AddScoped<ITestService, TestServiceWithMarkedCstr>();
//builder.Services.AddScoped<ITestService, TestServiceWithNoDefaultCstr>();
builder.Services.AddSingleton<ITestService, TestServiceWithNoCstr>();
builder.Services.AddScoped<ITestService, TestServiceWithDiffNoOfParamsInCstr>();

builder.Services.AddSingleton<GetDateTime>(DateTimeHelper.Utc);
builder.Services.AddSingleton<Func<DateTime>>(() => DateTime.UtcNow);

builder.Services.AddScoped<TimeConsumingService>();
builder.Services.AddScoped<ServiceUsingListOfServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (
    ITestService testService
    , ServiceUsingListOfServices serviceUsingList
    // , GetDateTime getDateTime
    , Func<DateTime> getDateTime
    ) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(getDateTime().AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
