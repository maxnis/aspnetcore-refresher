using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialMediaLinks.Models;

namespace SocialMediaLinks.Controllers
{
    public class HomeController : Controller
    {
        private SocialMediaLinksOptions _options;

        public HomeController(IOptions<SocialMediaLinksOptions> linksOptions) 
        {
            _options = linksOptions.Value;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Facebook = _options.Facebook;
            ViewBag.Twitter = _options.Twitter;
            ViewBag.Instagram = _options.Instagram;
            ViewBag.Youtube = _options.Youtube;

            return View();
        }
    }
}
