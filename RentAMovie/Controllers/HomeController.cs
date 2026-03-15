using Microsoft.AspNetCore.Mvc;
using RentAMovie.Models;
using System.Reflection.Metadata;

namespace RentAMovie.Controllers
{
    public class HomeController : Controller
    {
        // Cinematic Experience Landing Page
        public IActionResult Index()
        {
            return View();
        }
    }
}
