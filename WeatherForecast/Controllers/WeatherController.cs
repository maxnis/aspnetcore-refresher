using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
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
        return View(_cities);
    }

    [HttpGet]
    [Route("/weather/{cityCode}")]
    public IActionResult GetCityWeather(string cityCode)
    {
        ViewBag.Title = "City Weather Forecast";
        ViewBag.ShowDetails = false;
        var city = _cities.FirstOrDefault(temp => temp.CityUniqueCode.Equals(cityCode, StringComparison.CurrentCultureIgnoreCase));
        if (city == null)
        {
            return View("Error", new ErrorViewModel { 
                StatusCode = HttpStatusCode.NotFound, 
                ErrorMessage = $"City with code {cityCode} not found." 
            });
        }
        return View("CityWeather", city);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}