using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers;

public class WeatherController(ILogger<WeatherController> logger) : Controller
{
    private readonly ILogger<WeatherController> _logger = logger;

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

    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        ViewBag.Title = "Weather Forecast";
        ViewBag.ShowDetails = true;
        _logger.LogInformation("Accessed weather forecast index with {CityCount} cities", _cities.Count);
        return View(_cities);
    }

    [HttpGet]
    [Route("/weather/{cityCode}")]
    public IActionResult City(string cityCode)
    {
        if (string.IsNullOrWhiteSpace(cityCode))
        {
            return View();
        }

        ViewBag.ShowDetails = false;
        var city = _cities.FirstOrDefault(temp => temp.CityUniqueCode.Equals(cityCode, StringComparison.CurrentCultureIgnoreCase));
        if (city == null)
        {
            return View("Error", new ErrorViewModel { 
                StatusCode = HttpStatusCode.NotFound, 
                ErrorMessage = $"City with code {cityCode} not found." 
            });
        }
        ViewBag.Title = city.CityName + " | City Weather";
        _logger.LogInformation("Retrieved weather for city: {CityName}", city?.CityName ?? "Not Found");
        return View(city);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}