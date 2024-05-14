using Azure;
using Microsoft.AspNetCore.Mvc;

namespace RecipesProject.Controllers
{
    public class VideoController : Controller
    {
        public IActionResult Index()
        {
            var videoLinks = new List<string>
            {
                "https://www.youtube.com/watch?v=vbOGCSTd6Mg",
                "https://www.youtube.com/watch?v=vbOGCSTd6Mg",
                "https://www.youtube.com/watch?v=vbOGCSTd6Mg",
                "https://www.youtube.com/watch?v=vbOGCSTd6Mg",
                "https://www.youtube.com/watch?v=vbOGCSTd6Mg"
            };

            return View("Index", videoLinks);
        }
    }
}
