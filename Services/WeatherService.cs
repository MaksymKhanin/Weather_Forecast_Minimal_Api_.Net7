using AutoMapper;
using MinimalApi.DomainObjects;
using MinimalApi.DTOs;
using MinimalApi.Error_Handling;

namespace MinimalApi.Services;

public class WeatherService : IWeatherService
{
    private readonly ILogger<WeatherService> _logger;
    private readonly IMapper _mapper;

    private static List<WeatherForecast> InMemoryWeatherForecastStorage = new();
    public WeatherService(ILogger<WeatherService> logger, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    public Result<WeatherForecastResponse> GetWeatherForecastByDate(DateOnly date)
    {
        _logger.LogInformation("Trying to get weather forecast from in memory storage.");

        var weatherForecast = InMemoryWeatherForecastStorage.FirstOrDefault(x => x.Date.Equals(date));

        if (weatherForecast is null)
        {
            return Result.FailAndLog(new WeatherForecastNotFoundError(date), _logger, LogLevel.Warning);
        }

        _logger.LogInformation("Found weather forecast: {@weatherForecast}.", weatherForecast);

        var weatherForecastResponse = _mapper.Map<WeatherForecastResponse>(weatherForecast);

        return Result.Success(weatherForecastResponse);
    }

    public Result AddWeatherForecast(WeatherForecast weatherForecast)
    {
        _logger.LogInformation("Trying to add weather forecast to in memory storage.");

        if (InMemoryWeatherForecastStorage.Contains(weatherForecast))
        {
            return Result.FailAndLog(new WeatherForecastAlreadyExistsError(), _logger, LogLevel.Warning);
        }

        InMemoryWeatherForecastStorage.Add(weatherForecast);

        _logger.LogInformation("Successfully added weather forecast.");

        return Result.Success();
    }
}





