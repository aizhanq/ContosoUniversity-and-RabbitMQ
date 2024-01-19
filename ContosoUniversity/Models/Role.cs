using Microsoft.AspNetCore.Identity;

namespace ContosoUniversity.Models
{
    public class Role : IdentityRole
    {
        public int UserId { get; set; }
    }
}
