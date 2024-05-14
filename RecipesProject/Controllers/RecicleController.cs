using Microsoft.AspNetCore.Mvc;

namespace RecipesProject.Controllers
{
    public class RecicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
