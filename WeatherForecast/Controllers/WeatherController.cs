using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using WeatherForecast.Models;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers;

public class WeatherController(ILogger<WeatherController> logger, IWeatherService service) : Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        ViewBag.Title = "Weather Forecast";
        ViewBag.ShowDetails = true;
        var cities = service.GetWeatherDetails();
        logger.LogInformation("Accessed weather forecast index with {CityCount} cities", cities.Count);
        return View(cities);
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
        var city = service.GetWeatherByCityCode(cityCode);
        if (city == null)
        {
            return View("Error", new ErrorViewModel { 
                StatusCode = HttpStatusCode.NotFound, 
                ErrorMessage = $"City with code {cityCode} not found." 
            });
        }
        ViewBag.Title = city.CityName + " | City Weather";
        logger.LogInformation("Retrieved weather for city: {CityName}", city?.CityName ?? "Not Found");
        return View(city);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}