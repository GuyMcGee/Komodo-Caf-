namespace Komodo_Cafe
{
    public class MenuItem
    {
        public int MealNumber { get; set; }
        public string MealName { get; set; }
        public string MealDescription { get; set; }
        public List<string> Ingredients { get; set; }
        public double Price { get; set; }

        public MenuItem()
        {

        }
    }
}
