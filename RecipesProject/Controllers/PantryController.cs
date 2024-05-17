using Microsoft.AspNetCore.Mvc;

namespace RecipesProject.Controllers
{
    public class PantryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
