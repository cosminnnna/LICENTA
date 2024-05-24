using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipesProject.Data;
using RecipesProject.Helpers;
using RecipesProject.Models;
using System.ComponentModel;
using System.Diagnostics;

namespace RecipesProject.Controllers
{
    public class GenerateController : Controller
    {
        
        public IActionResult Generate()
        {
            return View("Generate");
        }

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GenerateController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Generate(List<string> ingredients)
        {
           /* if (ingredients.Count < 3)
            {
                ModelState.AddModelError(string.Empty, "Please provide at least 2 ingredients.");
                return View("Generate");
            }*/

            string response = GetCompletionFromPythonScript(ingredients);
            if (string.IsNullOrEmpty(response))
            {
                // ModelState.AddModelError("", "Failed to generate recipe.");
                ViewData["Recipe"] = "RESPONSE E NULL";
                return View("GeneratedRecipe", ViewData);
            }

            ViewData["Recipe"] = response;
            string[] lines = response.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            string imagePrompt = lines[0];

            string image = GetImageFromPythonScript(imagePrompt);
            ViewData["Image"] = image;

            return View("GeneratedRecipe", ViewData);
        }



        [HttpPost]
        public IActionResult Save(string recipeText)
        {
            var userId = _userManager.GetUserId(User); // Obține UserId-ul utilizatorului curent

            // Crează o nouă instanță a modelului Recipe și salvează textul rețetei în câmpul corespunzător
            var newRecipe = new Recipe { Description = recipeText, UserId = userId };
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



        private string GetCompletionFromPythonScript(List<string> ingredients)
        {
            string result = string.Empty;

            /*  string pythonPath = "C:\\Users\\cosmi\\AppData\\Local\\Programs\\Python\\Python310\\python.exe";
              string scriptPath = "C:\\Users\\cosmi\\Desktop\\LICENTA\\RecipesProject\\LICENTA\\RecipesProject\\PhythonScripts\\ApiScript.py";*/
            //string envPath = "C:\\Users\\cosmi\\Desktop\\LICENTA\\RecipesProject\\LICENTA\\RecipesProject\\env";
            //string pythonPath = "C:\\Users\\cosmi\\Desktop\\LICENTA\\RecipesProject\\LICENTA\\RecipesProject\\env\\Scripts\\python.exe"; 
            string pythonPath = "C:\\Python312\\python.exe";
            string scriptPath = "C:\\Users\\cosmi\\Desktop\\LICENTA\\RecipesProject\\LICENTA\\RecipesProject\\PhythonScripts\\ApiScript.py";
            string args = string.Join(" ", ingredients);
            string cmd = $"{scriptPath} {args}";

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = cmd,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(start))
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

        private string GetImageFromPythonScript(string imagePrompt)
        {
            string result = string.Empty;

            /*  string pythonPath = "C:\\Users\\cosmi\\AppData\\Local\\Programs\\Python\\Python310\\python.exe";
              string scriptPath = "C:\\Users\\cosmi\\Desktop\\LICENTA\\RecipesProject\\LICENTA\\RecipesProject\\PhythonScripts\\ApiScript.py";*/
            //string envPath = "C:\\Users\\cosmi\\Desktop\\LICENTA\\RecipesProject\\LICENTA\\RecipesProject\\env";
            //string pythonPath = "C:\\Users\\cosmi\\Desktop\\LICENTA\\RecipesProject\\LICENTA\\RecipesProject\\env\\Scripts\\python.exe"; 
            string pythonPath = "C:\\Python312\\python.exe";
            string scriptPath = "C:\\Users\\cosmi\\Desktop\\LICENTA\\RecipesProject\\LICENTA\\RecipesProject\\PhythonScripts\\ImageScript.py";
            string args = string.Join(" ", imagePrompt);
            string cmd = $"{scriptPath} {args}";

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = cmd,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(start))
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
