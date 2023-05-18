using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppWEB.Data;
using AppWEB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AppWEB.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public RolesController(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager
            )
        {
            _context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // GET: Roles
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
              return roles != null ? 
                          View(roles) :
                          Problem("Entity set 'ApplicationDbContext.Role'  is null.");
        }

        // Get  Admins details
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Details(string id) 
        {
            var roleEntity = await roleManager.FindByIdAsync(id); ;
            List<IdentityUser> users =
                (List<IdentityUser>) await userManager.GetUsersInRoleAsync(roleEntity.Name);
            ViewBag.Name = roleEntity.Name;
            return users != null ? View(users):Problem("No users in this role");
        }
        
    }
}
