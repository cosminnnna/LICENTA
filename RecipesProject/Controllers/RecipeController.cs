using Microsoft.AspNetCore.Mvc;
using RecipesProject.Data;
using RecipesProject.Models;
using System.Linq;

namespace RecipesProject.Controllers
{
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Afișează toate rețetele salvate din baza de date
            var recipes = _context.Recipes.ToList();
            return View(recipes);
        }

        [HttpPost]
        public IActionResult Create(string text)
        {
            // Crează o nouă instanță a modelului Recipe și salvează textul rețetei în câmpul corespunzător
            var newRecipe = new Recipe { Description = text };

            // Adaugă rețeta nouă în contextul bazei de date și salvează modificările
            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

            // Redirectează către acțiunea "Index" care afișează toate rețetele
            return RedirectToAction("Index");
        }

        // Acțiune pentru a afișa detalii despre o rețetă
        public IActionResult Details(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            return View(recipe);
        }

        // Acțiune pentru a actualiza o rețetă existentă
        [HttpPost]
        public IActionResult Update(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Acțiune pentru a șterge o rețetă
        public IActionResult Delete(int id)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == id);
            _context.Recipes.Remove(recipe);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
