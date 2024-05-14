using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesProject.Data;
using RecipesProject.Helpers;
using RecipesProject.Models;
using System.Diagnostics;
/*using System.IO;
using System;*/

namespace RecipesProject.Controllers
{
    public class GenerateController : Controller
    {

        public IActionResult Generate()
        {
            return View("Generate");
        }

        private readonly ApplicationDbContext _context;

        public GenerateController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Generate(string ingredient1, string ingredient2, string ingredient3)
        {
            string response = GetCompletionFromPythonScript(ingredient1, ingredient2, ingredient3);
            ViewData["Response"] = response;
            return View("GeneratedRecipe", ViewData);
        }

        /*[HttpPost]
        public IActionResult Save(string recipeText)
        {
            List<string> savedRecipes = HttpContext.Session.GetObject<List<string>>("SavedRecipes");
            if (savedRecipes == null)
            {
                savedRecipes = new List<string>();
            }
            savedRecipes.Add(recipeText);
            HttpContext.Session.SetObject("SavedRecipes", savedRecipes);

            return RedirectToAction("Recipe");
        }*/
        [HttpPost]
        public IActionResult Save(string recipeText)
        {
            // Crează o nouă instanță a modelului Recipe și salvează textul rețetei în câmpul corespunzător
            var newRecipe = new Recipe { Description = recipeText };

            // Adaugă rețeta nouă în contextul bazei de date și salvează modificările
            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

            // Redirectează către acțiunea "Recipe" care afișează toate rețetele
            return RedirectToAction("Index", "Recipe");
        }

        public IActionResult Favorite()
        {
            List<string> savedRecipes = HttpContext.Session.GetObject<List<string>>("SavedRecipes");
            ViewBag.Recipes = savedRecipes ?? new List<string>();
            return View("Index", "Recipe");
        }

        private string GetCompletionFromPythonScript(string ingredient1, string ingredient2, string ingredient3)
        {
            string result = string.Empty;

            // Specificarea căii către interpreterul Python și scriptul nostru
            string pythonPath = "C:\\Users\\cosmi\\AppData\\Local\\Programs\\Python\\Python310\\python.exe"; // Înlocuiește cu calea către interpreterul Python
            string scriptPath = "C:\\Users\\cosmi\\Desktop\\LICENTA\\RecipesProject\\LICENTA\\RecipesProject\\PhythonScripts\\ApiScript.py"; // Înlocuiește cu calea către scriptul Python

            // Construirea comenzii pentru a rula scriptul Python
            string arguments = $"{scriptPath} {ingredient1} {ingredient2} {ingredient3}";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Pornirea procesului și citirea rezultatului din ieșirea standard
            using (Process process = Process.Start(startInfo))
            {
                if (process != null)
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }

            return result;
        }
    }
}


