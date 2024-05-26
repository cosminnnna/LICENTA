/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null || users.Count == 0)
            {
                ViewData["Users"] = "Nu s-au găsit utilizatori.";
            }

            ViewData["Users"] = users;
            return View();
        }
    }
}
*/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipesProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AdminController> _logger;
        public IActionResult Index()
        {
            return View();
        }

        public AdminController(UserManager<IdentityUser> userManager, ILogger<AdminController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Users()
        {
            _logger.LogInformation("Fetching users from the database.");

            var users = await _userManager.Users.ToListAsync();

            if (users == null || users.Count == 0)
            {
                _logger.LogWarning("No users found.");
                ViewData["Users"] = "Nu s-au găsit utilizatori.";
            }
            else
            {
                _logger.LogInformation($"Number of users found: {users.Count}");
                ViewData["Users"] = users;
            }

            return View();
        }
    }
}
