using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAMovie.Data;
using RentAMovie.Models;

namespace RentAMovie.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Dashboard
        public IActionResult Index()
        {
            var stats = new AdminDashboardViewModel
            {
                TotalUsers = _context.Users.Count(),
                TotalMovies = _context.Movies.Count(),
                TotalRents = _context.Rents.Count(),
                ActiveRents = _context.Rents.Count(r => r.ToDate >= DateTime.Today)
            };
            return View(stats);
        }

        // Movie Management
        public IActionResult Movies()
        {
            var Movies = _context.Movies.ToList();
            return View(Movies);
        }

        [HttpGet]
        public IActionResult CreateMovie()
        {
            return View(new Movie());
        }

        [HttpPost]
        public IActionResult CreateMovie(Movie model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Movies.Add(model);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Movie added successfully!";
            return RedirectToAction("Movies");
        }

        [HttpGet]
        public IActionResult EditMovie(int id)
        {
            var Movie = _context.Movies.Find(id);
            if (Movie == null) return NotFound();
            return View(Movie);
        }

        [HttpPost]
        public IActionResult EditMovie(Movie model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Movies.Update(model);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Movie updated successfully!";
            return RedirectToAction("Movies");
        }

        [HttpPost]
        public IActionResult DeleteMovie(int id)
        {
            var Movie = _context.Movies.Find(id);
            if (Movie != null)
            {
                _context.Movies.Remove(Movie);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Movie deleted successfully!";
            }
            return RedirectToAction("Movies");
        }

        // Rent Management
        public IActionResult Rents()
        {
            var rents = _context.Rents
                .Include(r => r.Movie)
                .Include(r => r.User)
                .OrderByDescending(r => r.FromDate)
                .ToList();
            return View(rents);
        }

        [HttpPost]
        public IActionResult DeleteRent(int id)
        {
            var rent = _context.Rents.Find(id);
            if (rent != null)
            {
                _context.Rents.Remove(rent);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Rent deleted successfully!";
            }
            return RedirectToAction("Rents");
        }

        // User Management
        public IActionResult Users()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult ToggleAdmin(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.Role = user.Role == "Admin" ? "User" : "Admin";
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"User role updated to {user.Role}!";
            }
            return RedirectToAction("Users");
        }
    }
}