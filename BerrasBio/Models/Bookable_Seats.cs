
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BerrasBio.Models
{
    public class Bookable_Seats
    {
        public int Bookable_SeatsID { get; set; }
        public decimal Ticket_Price { get; set; }
        public int ShowID { get; set; }
        public virtual Show Show { get; set; }
        
        public int SeatID { get; set; }
        public virtual Seat Seat { get; set; }
        

        public int? BookingID { get; set; }
        public virtual Booking? Booking { get; set; }
        

    }
}
