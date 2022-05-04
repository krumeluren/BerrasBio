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
        /// Business logic. Validate the seats selected by the user before allowing them to be added to a booking
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

            // If seats is 0 or longer than 12 seats throw error
            if (seats.Count() <= 0 || seats.Count() > 12 )
            {
                //throw new Exception("Selected seats for booking out of allowed range");
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
