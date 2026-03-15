using System.ComponentModel.DataAnnotations;

namespace RentAMovie.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";
    }
}
