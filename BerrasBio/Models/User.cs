using Microsoft.AspNetCore.Identity;

namespace BerrasBio.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public virtual ICollection<Booking>? Bookings { get; set; }
    }


}
