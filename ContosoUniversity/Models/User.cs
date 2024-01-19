using Microsoft.AspNetCore.Identity;

namespace ContosoUniversity.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
