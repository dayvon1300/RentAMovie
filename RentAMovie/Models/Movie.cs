using System.ComponentModel.DataAnnotations;

namespace RentAMovie.Models
{
    public class Movie
    {
        public int Id { get; set; } //primary key за базата ни 

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; } = "";
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = "";
        [Required]
        [Range(1888, 2026, ErrorMessage = "Year must be between 1888 and 2026")]
        public int Year { get; set; }
        [Required]
        [Range(0.01, 10000, ErrorMessage = "Price must be positive")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; } = "";

        [Required]
        [Range(1, 1000, ErrorMessage = "Available copies must be at least 1")]
        public int AvailableCopies { get; set; } = 1;

    }
}
