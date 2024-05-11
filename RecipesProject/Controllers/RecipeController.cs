using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
/*using System.IO;
using System;*/

namespace RecipesProject.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult Generate()
        {
            return View("Generate");
        }

        [HttpPost]
        public IActionResult Generate(string ingredient1, string ingredient2, string ingredient3)
        {
            string response = GetCompletionFromPythonScript(ingredient1, ingredient2, ingredient3);
            ViewData["Response"] = response;
            return View("GeneratedRecipe", ViewData);
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


