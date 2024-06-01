using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipesProject.Data;
using RecipesProject.Models;

namespace RecipesProject.Controllers
{
    public class VideoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VideoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acțiune pentru a afișa lista de linkuri video
        /*        public IActionResult Index()
                {
                    List<string> videoLinks = _context.Videos.Select(v => v.Link).ToList();
                    return View("Index", videoLinks);
                }*/

        // Acțiune pentru a afișa lista de linkuri video
        public IActionResult Index()
        {
            List<Video> videos = _context.Videos.ToList();
            return View(videos);
        }

        // Acțiune pentru a permite doar adminilor să adauge un nou link video
        /*[Authorize(Roles = "User, Admin")]*/
        public IActionResult AddVideo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       /* [Authorize(Roles = "User, Admin")]*/
        public async Task<IActionResult> AddVideo(Video video)
        {
            if (ModelState.IsValid)
            {
                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(video);
        }
    }
}
