using AppWEB.Data;
using AppWEB.DTO;
using AppWEB.ManualMapper;
using AppWEB.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

namespace AppWEB.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public CartController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager
            )
        {
            this.context = context;
            this.userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var user = await userManager.GetUserAsync(User);
            var cart = await context.Carts.Include(e => e.CartItems)
                .ThenInclude(e => e.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (cart == null)
            {
                return View(new List<CartItem>());
            }
            var cartItems = cart.CartItems.Select(ci => new CartItem
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                Product = ci.Product,

            }).ToList();

            return View(cartItems);

        }
        [Authorize]
        public async Task<IActionResult> CartPartialView()
        {
            var user = await userManager.GetUserAsync(User);
            var cart = await context.Carts.Include(e => e.CartItems)
                .ThenInclude(e => e.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);
            if (cart == null)
            {
                return PartialView("_Cart",new List<CartItem>());
            }
            var cartItems = cart.CartItems.Select(ci => new CartItem
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                Product = ci.Product,

            }).ToList();

            return PartialView("_Cart",cartItems); ;

        }
        [Authorize]
        
        public async Task<IActionResult> Post(
            [Bind("ProductId,UserName,Quantity")] CartPostDTO cartPostDTO) 
        {
            var user = await userManager.GetUserAsync(User);
            var cart = await context.Carts.Include(e => e.CartItems)
                .ThenInclude(c => c.Product)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);
            
            if (cart == null)
            {
                
                cart = new Cart
                {
                    UserId = user.Id,
                    CartItems = new List<CartItem>()
                };
               var newCart = await context.Carts.AddAsync(cart);
                await context.SaveChangesAsync();
            }

            var cartItem = cart.CartItems.FirstOrDefault(
                ci => ci.ProductId == cartPostDTO.ProductId);

            if (cartItem == null)
            {
                var carts = await context.Carts.SingleAsync(e=>e.UserId == user.Id);
                cartItem = new CartItem
                {
                    
                    CartId = carts.Id,
                    ProductId = cartPostDTO.ProductId,
                    Quantity = cartPostDTO.Quantity,
                };
                 await context.CartItems.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += cartPostDTO.Quantity;
            }

            await context.SaveChangesAsync();


            return RedirectToAction("Index","Home");
        }
    }
}
