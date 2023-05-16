using Microsoft.AspNetCore.Identity;

namespace SalesPageAPI.Models
{
    public class User : IdentityUser
    {
        public DateTime DayBirth { get; set; }

        public User() :base() {}

    }
}
