namespace RecipesProject.Models
{
    public class RecyclingCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Paper { get; set; }
        public bool Plastic { get; set; }
        public bool Metal { get; set; }
        public bool Glass { get; set; }
    }
}
