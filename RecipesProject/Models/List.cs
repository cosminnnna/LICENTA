namespace RecipesProject.Models
{
    public class List
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string UserId { get; set; } // Pentru a asocia lista cu un utilizator
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
