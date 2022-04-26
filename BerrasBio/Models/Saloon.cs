namespace BerrasBio.Models
{
    public class Saloon
    {
        public int SaloonID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Seat>? Seats { get; set; }
        public virtual ICollection<Show>? Shows { get; set; }
    }
}
