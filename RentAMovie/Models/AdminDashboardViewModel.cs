namespace RentAMovie.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalMovies { get; set; }
        public int TotalRents { get; set; }
        public int ActiveRents { get; set; }
    }
}