using MinimalApi.DomainObjects;
using MinimalApi.DTOs;
using MinimalApi.Error_Handling;

namespace MinimalApi.Services
{
    public interface IWeatherService
    {
        Result AddWeatherForecast(WeatherForecast weatherForecast);
        Result<WeatherForecastResponse> GetWeatherForecastByDate(DateOnly date);
    }
}