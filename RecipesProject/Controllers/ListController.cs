using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesProject.Data;
using RecipesProject.Models;

namespace RecipesProject.Controllers
{
    public class ListController : Controller
    {
        /*        public IActionResult Index()
                {
                    return View();
                }*/
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ListController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var lists = await _context.Lists.Include(l => l.Items).Where(l => l.UserId == userId).ToListAsync();
            return View(lists);
        }

        [HttpPost]
        public async Task<IActionResult> AddList(string name)
        {
            var userId = _userManager.GetUserId(User);
            var list = new List { Name = name, UserId = userId };
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int listId, string text)
        {
            var list = await _context.Lists.FindAsync(listId);
            if (list != null)
            {
                var item = new Item { Text = text, IsChecked = false, ListId = listId };
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                item.IsChecked = !item.IsChecked;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

