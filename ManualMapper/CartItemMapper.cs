using AppWEB.DTO;
using AppWEB.Models;

namespace AppWEB.ManualMapper
{
    public class CartItemMapper
    {
       public CartItem cartItem { get; set; }
        public CartItemMapper(CartPostDTO dto,int CartId)
        {
            cartItem.ProductId = dto.ProductId;
            cartItem.CartId = CartId;
            cartItem.Quantity = dto.Quantity;
            
        }
    }
}
