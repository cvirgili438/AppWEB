using Microsoft.AspNetCore.Identity;

namespace AppWEB.Models
{
    public class User : IdentityUser
    {       
        public Cart Cart { get; set; }  = null!;
    }
}
