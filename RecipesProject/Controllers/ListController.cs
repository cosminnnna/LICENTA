using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesProject.Data;
using RecipesProject.Models;

namespace RecipesProject.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
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
            var lists = await _context.Lists
                .Include(l => l.Items)
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.CreatedDate) // Ordine descrescătoare după data creării
                .ToListAsync();
            return View(lists);
        }

        [HttpPost]
        public async Task<IActionResult> AddList(string name)
        {
            var userId = _userManager.GetUserId(User);
            var list = new List { Name = name, UserId = userId, CreatedDate = DateTime.UtcNow }; // Setează data creării
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

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _context.Lists.Include(l => l.Items).FirstOrDefaultAsync(l => l.Id == id);
            if (list != null)
            {
                _context.Items.RemoveRange(list.Items);
                _context.Lists.Remove(list);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

