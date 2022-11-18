using Komodo_Cafe;
using System.Xml.Serialization;

namespace K_Cafe.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void FindMenuItemTest()
        {
            var repo = new MenuItemRepository();
            var item = new MenuItem();
            var mealId = repo.CreateMenuItems(item).MealNumber;
            var founditem = repo.Find(mealId);

            Assert.Equal(mealId, founditem.MealNumber);
        }

        [Fact]
        public void CreateMenuItemTest()
        {
            var repo = new MenuItemRepository();
            MenuItem item = new MenuItem();
            item.MealName = "Hot Dog";
            item.MealDescription = "Dog in bun";
            var createdItem = repo.CreateMenuItems(item);

            Assert.Equal(item.MealName, createdItem.MealName);
            Assert.Equal(item.MealDescription, createdItem.MealDescription);
        }

        [Fact]
        public void ReadTest()
        {
            var repo = new MenuItemRepository();
            var item = new MenuItem();
            var item2 = new MenuItem();
            repo.CreateMenuItems(item);
            repo.CreateMenuItems(item2);

            Assert.Equal(2, repo.Read().Count);
        }

        [Fact]
        public void UpdateTest()
        {
            var repo = new MenuItemRepository();
            var item = new MenuItem();
            var dictionary = new Dictionary<string, string>();
            repo.CreateMenuItems(item);
            dictionary.Add("MealName", "Hot Dog");
            dictionary.Add("Description", "A dog that's hot");
            dictionary.Add("Ingredients", "meat, bun, mustard");
            dictionary.Add("Price", "1.50");
            repo.Update(item.MealNumber, dictionary);

            Assert.Equal(item.MealName, "Hot Dog");
            Assert.Equal(item.MealDescription, "A dog that's hot");
            Assert.Equal(item.Ingredients, "meat, bun, mustard".Replace(" ", "").Split(',').ToList());
            Assert.Equal(item.Price, Convert.ToDouble("1.50"));
        }

        [Fact]
        public void DeleteTest()
        {
            var repo = new MenuItemRepository();
            var item = new MenuItem();
            repo.CreateMenuItems(item);
            repo.Delete(item.MealNumber);

            Assert.Equal(0, repo.Read().Count);
        }
    }
}