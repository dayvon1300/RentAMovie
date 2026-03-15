using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentAMovie.Data;
using RentAMovie.Models;
using System.Security.Claims;

namespace RentAMovie.Controllers
{
    [Authorize]
    public class RentController : Controller
    {
        private readonly AppDbContext _context;

        public RentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(int MovieId)
        {
            var rent = new Rent
            {
                MovieId = MovieId,
                FromDate = DateTime.Today,
                ToDate = DateTime.Today.AddDays(1),
            };
            return View(rent);
        }

        [HttpPost]
        public IActionResult Create(Rent model)
        {
            if (model.FromDate >= model.ToDate)
            {
                ModelState.AddModelError("", "End date must be after start date");
            }

            var movie = _context.Movies.FirstOrDefault(m => m.Id == model.MovieId);
            if (movie == null)
            {
                TempData["ErrorMessage"] = "Movie not found!";
                return RedirectToAction("Index", "Home");
            }

            int activeRentalsCount = _context.Rents.Count(r =>
                r.MovieId == model.MovieId &&
                model.FromDate < r.ToDate &&
                model.ToDate > r.FromDate);

            if (activeRentalsCount >= movie.AvailableCopies)
            {
                TempData["ErrorMessage"] = "All copies of this movie are rented for this period! Please choose different dates.";
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            model.UserId = userId;

            _context.Rents.Add(model);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Movie rented successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}