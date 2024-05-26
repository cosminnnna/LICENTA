namespace RecipesProject.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string UserId { get; set; } // Pentru a asocia lista cu un utilizator
       // public virtual ApplicationUser User { get; set; }
    }
}
