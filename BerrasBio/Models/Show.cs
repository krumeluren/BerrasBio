namespace BerrasBio.Models
{
    public class Show
    {
        public int ShowID { get; set; }
        public DateTime ShowTime { get; set; }
        
        public int MovieID { get; set; }
        public Movie Movie { get; set; }
        
        public int SaloonID { get; set; }
        public Saloon Saloon { get; set; }

        public virtual ICollection<Bookable_Seats>? Bookable_Seats { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }
    }

}
