namespace BerrasBio.Models
{
    public class Row
    {
        public int RowID { get; set; }
        public int SaloonID { get; set; }
        public virtual Saloon Saloon { get; set; }
        public virtual ICollection<Seat>? Seats { get; set; }

    }
}
