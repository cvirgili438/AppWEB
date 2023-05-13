using Microsoft.AspNetCore.Identity;

namespace AppWEB.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User Users { get; set; } = null!;
        
        public ICollection<Product> Products { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = null!;


    }
}
