using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentAMovie.Data;

namespace RentAMovie.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(List<string> genres)
        {
            var movies = _context.Movies.AsQueryable();

            // Filter by genres
            if (genres != null && genres.Any())
            {
                movies = movies.Where(m => genres.Contains(m.Genre));
            }

            // Get all unique genres
            ViewBag.Genres = _context.Movies
                .Select(m => m.Genre)
                .Distinct()
                .OrderBy(g => g)
                .ToList();

            ViewBag.SelectedGenres = genres ?? new List<string>();

            return View(movies.ToList());
        }
    }
}