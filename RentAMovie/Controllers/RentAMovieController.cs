using Microsoft.AspNetCore.Mvc;

namespace RentAMovie.Controllers
{
    public class RentAMovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
