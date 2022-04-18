namespace BerrasBio.Models
{
    public class Seat
    {
        public int SeatID { get; set; }
        public int RowID { get; set; }
        public virtual Row Row { get; set; }

        public virtual ICollection<Bookable_Seats>? This_Seat_Per_Show { get; set; }
    }
}
