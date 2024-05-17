using Microsoft.AspNetCore.Mvc;
using RecipesProject.Data;
using RecipesProject.Models;
using System;

namespace RecipesProject.Controllers
{
    public class RecyclingCenterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecyclingCenterController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       /* // Endpoint pentru a obține centrele de reciclare în funcție de tipul de material
        public IActionResult GetRecyclingCenters(bool paper, bool plastic, bool metal, bool glass)
        {
            var centers = _context.RecyclingCenters.Where(c =>
                (paper && c.Paper) || (plastic && c.Plastic) || (metal && c.Metal) || (glass && c.Glass))
                .ToList();

            return Json(centers); // Returnează lista de centre de reciclare sub formă de JSON
        }*/

        // Metoda care returnează centrele de reciclare în funcție de categorie
        [HttpGet]
        public IActionResult GetRecyclingCenters(string category)
        {

            // Convert category to lowercase to ensure case-insensitive comparison
            var lowerCategory = category?.ToLower();

            var centers = _context.RecyclingCenters.Where(c =>
                (lowerCategory == "hartie" && c.Paper) ||
                (lowerCategory == "plastic" && c.Plastic) ||
                (lowerCategory == "sticla" && c.Glass) ||
                (lowerCategory == "metal" && c.Metal)
            ).ToList();


            // Transformați centrele de reciclare într-o listă de obiecte simplificate pentru a le trimite către frontend
            var simplifiedCenters = centers.Select(c => new
            {
                c.Name,
                c.Latitude,
                c.Longitude
            });

            return Json(simplifiedCenters); // Returnați lista de centre de reciclare sub formă de JSON
        }
    }
}


