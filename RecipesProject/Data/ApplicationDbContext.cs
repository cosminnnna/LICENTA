using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipesProject.Models;

namespace RecipesProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecyclingCenter> RecyclingCenters { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Item> Items { get; set; }

    }
}
