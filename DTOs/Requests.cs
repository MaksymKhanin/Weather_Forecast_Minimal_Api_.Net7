using MinimalApi.DomainObjects;

namespace MinimalApi.Requests;

public record AddWeatherRequest(WeatherForecastRequest WeatherForecast);
public record WeatherForecastRequest(DateOnly Date, WeatherRequest Weather);
public record WeatherRequest(double Temperature, WindDirection WindDirection, double WindSpeed, string Name, string Description, string? Recommendation);