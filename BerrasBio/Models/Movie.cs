namespace BerrasBio.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public int Length { get; set; }
        
        public virtual ICollection<Show>? Shows { get; set; }
        
    }

}
