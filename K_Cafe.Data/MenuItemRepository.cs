namespace Komodo_Cafe
{
    public class MenuItemRepository
    {
        private readonly List<MenuItem> _menuitems = new List<MenuItem>();
        private int _count;

        public MenuItemRepository()
        {
            _count = 0;

        }

        public MenuItem CreateMenuItems(MenuItem menuItem)
        {
            
                _count++;
                menuItem.MealNumber = _count;
                _menuitems.Add(menuItem);
                return menuItem;
        }

        public List<MenuItem> Read()
        {
            return _menuitems;
        }

        public MenuItem Find(int mealNum)
        {
            foreach (MenuItem menuItem in _menuitems)
            {
                if (mealNum == menuItem.MealNumber)
                {
                    return menuItem;
                }
            }
            return null;
        }

        public MenuItem Update(int mealNumber, Dictionary<string, string> updateArguments) 
        {
            var menuItem = Find(mealNumber);

            if (updateArguments.ContainsKey("MealName"))
                menuItem.MealName = updateArguments["MealName"];
            if (updateArguments.ContainsKey("Description"))
                menuItem.MealDescription = updateArguments["Description"];
            if (updateArguments.ContainsKey("Ingredients"))
                menuItem.Ingredients = updateArguments["Ingredients"].Replace(" ", "").Split(',').ToList(); //[Ingredients] is accessing the dictionary in the same way [] access an array. Since the value at the accesor is a string, I can perform string logic on it.
            if (updateArguments.ContainsKey("Price"))
                menuItem.Price = Convert.ToDouble(updateArguments["Price"]);
            return menuItem;
        }

        public MenuItem Delete(int mealNumber)
        {
            var itemAboutToBeDeleted = Find(mealNumber);
            _menuitems.Remove(itemAboutToBeDeleted);
            return itemAboutToBeDeleted;
        }

    }
}
