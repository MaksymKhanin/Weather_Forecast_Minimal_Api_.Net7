using MinimalApi.ApiEndpoints;
using MinimalApi.Mapping;
using MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddAutoMapper(typeof(RequestToDomainMapping));
builder.Services.AddAutoMapper(typeof(DomainToResponseMapping));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

WeatherEndpoints.MapWeatherEndpoints(app);

app.Run();
