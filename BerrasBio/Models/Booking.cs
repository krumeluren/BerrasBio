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

        /// <summary>
        /// Validate the seats selected by the user before adding a booking
        /// </summary>
        /// <param name="seats"></param>
        /// <returns>true if success</returns>
        public static bool ValidateSeatsForBooking(IEnumerable<Bookable_Seats>? seats)
        {

            // If seats is null, throw error, return false
            if (seats == null)
            {
                throw new ArgumentNullException(nameof(seats));
                return false;
            }

            // If seats is longer than 12 seats throw error
            if (seats.Count() > 12)
            {
                throw new Exception("You can only book up to 12 seats at a time");
                return false;
            }
            
            foreach (var seat in seats)
            {
                // If any of the seats are already booked throw error
                if (seat.Booking != null)
                {
                    throw new Exception("You cannot book seats that are already booked");
                    return false;
                }
            }
            
            return true;
        }
    }



}
