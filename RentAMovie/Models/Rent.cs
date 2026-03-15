namespace RentAMovie.Models
{
    //табличката която ще ни свърза User и Movie и ще пази периода ни 
    public class Rent
    {
        public int Id { get; set; }

        //Връзки с които ще работим
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        //период на наемане
        public DateTime FromDate {  get; set; }
        public DateTime ToDate { get; set; }

        
    }
}
