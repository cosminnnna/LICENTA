using Microsoft.AspNetCore.Mvc;

namespace RecipesProject.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
