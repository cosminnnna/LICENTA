using Microsoft.AspNetCore.Mvc;
using RecipesProject.Data;
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

        public GenerateController(ApplicationDbContext context)
        {
            _context = context;
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
            /*ViewData["ImageUrl"] = response.Item2;*/
            return View("GeneratedRecipe", ViewData);
        }

        private string GetCompletionFromPythonScript(List<string> ingredients)
        {
            /*string recipeText = string.Empty;
            string imageUrl = string.Empty;*/
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
                /* Arguments = $"/c \"{pythonPath} {cmd}\"",*/
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
                        // Găsește indexul unde începe cuvântul "IMAGE"
                        //int imageIndex = result.IndexOf("IMAGE");

                        // Extrage tot textul până la cuvântul "IMAGE"
                        // string textBeforeImage = result;

                        // Caută indexul unde începe "https://"
                        //int urlIndex = result.IndexOf("https://", imageIndex);

                        // Extrage URL-ul care începe de la "https://"
                        //string imageUrl = reader.ReadToEnd();
                    }
                }
            }
           
            return result;

        }
    }
}
