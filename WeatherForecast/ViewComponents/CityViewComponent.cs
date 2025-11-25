using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.ViewComponents;

public class CityViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(CityWeather city)
    {
        ViewBag.BackColor = GetBgColor(city.TemperatureFahrenheit);
        return View(city);
    }

    public static string GetBgColor(int temperature)
    {
        return temperature switch
        {
            > 74 => "orange-back",
            < 44 => "blue-back",
            _ => "green-back"
        };
    }
}
