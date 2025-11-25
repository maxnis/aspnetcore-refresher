using WeatherForecast.Models;

namespace WeatherForecast.Services;

public interface IWeatherService
{
    List<CityWeather> GetWeatherDetails();

    CityWeather? GetWeatherByCityCode(string cityCode);
}