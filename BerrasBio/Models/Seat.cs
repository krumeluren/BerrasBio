namespace BerrasBio.Models
{
    public class Seat
    {
        public int SeatID { get; set; }
        public int SaloonID { get; set; }
        public virtual Saloon Saloon { get; set; }

        public virtual ICollection<Bookable_Seats>? This_Seat_Per_Show { get; set; }
    }
}
