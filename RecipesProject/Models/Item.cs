namespace RecipesProject.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }

        // Foreign Key
        public int ListId { get; set; }

        // Navigation Property
        public List List { get; set; }
    }
}
