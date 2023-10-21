using SlamCodeBlog.DotNetDITips;
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
//builder.Services.AddScoped<ITestService, TestServiceWithNoResolvableCstr>();

builder.Services.AddSingleton<GetDateTime>(DateTimeHelper.Utc);
builder.Services.AddSingleton<Func<DateTime>>(() => DateTime.UtcNow);

builder.Services.AddSingleton<string>($"{DateTime.UtcNow.ToShortTimeString()}");
// builder.Services.AddSingleton<DateTime>(DateTime.UtcNow);
// builder.Services.AddSingleton(new SomeStruct());
// builder.Services.AddSingleton<Double>(DateTime.UtcNow.Month * 1.0 / DateTime.UtcNow.Day);
// builder.Services.AddSingleton<Double>((sp) => DateTime.UtcNow.Month * 1.0 / DateTime.UtcNow.Day);    
//dynamic myDynamicSomething = new { BlahBlah = "Some string" };
//builder.Services.AddSingleton(typeof(myDynamicSomething), myDynamicSomething);
//builder.Services.Add(new ServiceDescriptor(typeof(myDynamicSomething), myDynamicSomething));

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
