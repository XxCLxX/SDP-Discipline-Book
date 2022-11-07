using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public DateTime Birthdate { get; set; }

        public string Phone { get; set; }

        public string Type { get; set; }
    }
}