namespace WeatherForecast.Models;

public class CityWeather
{
    public required string CityUniqueCode { get; set; }
    public required string CityName { get; set; }
    public DateTime DateAndTime { get; set; }
    public int TemperatureFahrenheit { get; set; }
}