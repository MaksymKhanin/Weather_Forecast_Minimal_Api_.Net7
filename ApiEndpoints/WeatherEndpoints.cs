using AutoMapper;
using MinimalApi.DomainObjects;
using MinimalApi.Requests;
using MinimalApi.Services;

namespace MinimalApi.ApiEndpoints;

public class WeatherEndpoints
{
    public static void MapWeatherEndpoints(IEndpointRouteBuilder app)
    {
        var weather = app.MapGroup("api/Weather");

        weather.MapGet("/GetWeatherForecastForDate", GetWeatherForecastForDateAsync);
        weather.MapPost("/AddWeather", AddWeatherAsync);
    }

    public static IResult GetWeatherForecastForDateAsync(DateOnly date, ILogger<WeatherEndpoints> _logger, IWeatherService _weatherService)
    {
        _logger.BeginScope("Request: {@request}", date);
        _logger.LogInformation("Received request to get weather forecast by date: {@date}", date);

        return _weatherService.GetWeatherForecastByDate(date).Match<IResult>(
            success => TypedResults.Ok(success),
            error => TypedResults.BadRequest(error),
            notFound => TypedResults.NotFound(notFound));
    }

    public static IResult AddWeatherAsync([AsParameters] AddWeatherRequest addWeatherForecastRequest, IMapper _mapper, ILogger<WeatherEndpoints> _logger, IWeatherService _weatherService)
    {
        _logger.BeginScope("Request: {@request}", addWeatherForecastRequest);
        _logger.LogInformation("Adding weather forecast: {@weatherForecast}", addWeatherForecastRequest);

        var weatherForecast = _mapper.Map<WeatherForecast>(addWeatherForecastRequest.WeatherForecast);

        var result = _weatherService.AddWeatherForecast(weatherForecast);

        return (result.IsSuccess) 
            ? TypedResults.Ok() 
            : TypedResults.BadRequest(result.Error);
    }
}
