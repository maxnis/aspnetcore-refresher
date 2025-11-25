using WeatherForecast.Models;

namespace WeatherForecast.Services;

public class WeatherService : IWeatherService
{
    private readonly IList<CityWeather> _cities =
    [
        new()
        {
            CityUniqueCode = "LDN",
            CityName = "London",
            DateAndTime = DateTime.Parse("2030-01-01 8:00"),
            TemperatureFahrenheit = 33
        },
        new()
        {
            CityUniqueCode = "NYC",
            CityName = "New York",
            DateAndTime = DateTime.Parse("2030-01-01 3:00"),
            TemperatureFahrenheit = 60
        },
        new()
        {
            CityUniqueCode = "PAR",
            CityName = "Paris",
            DateAndTime = DateTime.Parse("2030-01-01 9:00"),
            TemperatureFahrenheit = 82
        }
    ];

    public CityWeather? GetWeatherByCityCode(string cityCode)
    {
        return _cities.FirstOrDefault(temp => temp.CityUniqueCode.Equals(cityCode, StringComparison.CurrentCultureIgnoreCase));
    }

    public List<CityWeather> GetWeatherDetails()
    {
        return [.. _cities];
    }
}
