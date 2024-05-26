using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipesProject.Data;
using RecipesProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace RecipesProject.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RecipeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var recipes = await _context.Recipes.Where(recipe => recipe.UserId == userId).ToListAsync();
            return View(recipes);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string text)
        {
            var userId = _userManager.GetUserId(User);

            // Crează o nouă instanță a modelului Recipe și salvează textul rețetei în câmpul corespunzător
            var newRecipe = new Recipe { Description = text, UserId = userId };
            
            // Adaugă rețeta nouă în contextul bazei de date și salvează modificările
            _context.Recipes.Add(newRecipe);
            await _context.SaveChangesAsync();

            // Redirectează către acțiunea "Index" care afișează toate rețetele
            return RedirectToAction("Index");
        }

/*        // Acțiune pentru a afișa detalii despre o rețetă
        public IActionResult Details(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            return View(recipe);
        }
*/
/*        // Acțiune pentru a actualiza o rețetă existentă
        [HttpPost]
        public IActionResult Update(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
*/
/*        // Acțiune pentru a șterge o rețetă
        public IActionResult Delete(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }*/
    }
}
