namespace BerrasBio.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int AccountID { get; set; }
        public virtual Account Account { get; set; }
        public int ShowID { get; set; }
        public virtual Show Show { get; set; }

        public virtual ICollection<Bookable_Seats>? Booked_Seats { get; set; }
    }
}
