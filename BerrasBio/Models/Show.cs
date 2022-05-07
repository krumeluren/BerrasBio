namespace BerrasBio.Models
{
    public class Show
    {
        public int ShowID { get; set; }

        public DateTime ShowTime { get; set; }
        
        public int MovieID { get; set; }
        public virtual Movie? Movie { get; set; }
        
        public int SaloonID { get; set; }
        public virtual Saloon? Saloon { get; set; }

        public virtual ICollection<Bookable_Seats>? Bookable_Seats { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }

        
        /// <summary>
        /// Check if all selectedSeats belong to the same show
        /// </summary>
        /// <param name="selectedSeats"></param>
        /// <returns>true if all seats exist in the show</returns>
        public bool ContainsAll(IEnumerable<Bookable_Seats> selectedSeats)
        {
            // if all selectedSeats exist in Bookable_Seats
            return selectedSeats.All(seat => Bookable_Seats.Contains(seat));
        }
    }

    

}
