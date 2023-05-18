using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppWEB.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
