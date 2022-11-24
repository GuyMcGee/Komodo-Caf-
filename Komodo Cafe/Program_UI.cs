using Komodo_Cafe;
using System.Net;

internal class Program_UI
{
    private MenuItemRepository repo = new MenuItemRepository();

    public void Run()
    {
        Console.WriteLine();
        Console.WriteLine("\tKomodo Cafe Menu Management Terminal");
        Console.WriteLine();
        Console.WriteLine("\tType \"help\" to see a list of available commands");
        Console.WriteLine();

    input:
        Console.WriteLine();
        var userInput = Console.ReadLine();
        Console.WriteLine();

        switch (userInput)
        {
            case "help":
                HelpMenu();
                break;

            case "menu":
                ShowMenu();
                break;

            case "new item":
                var menuItem = new MenuItem();

                Console.WriteLine("Enter new item's name.");
            itemName:
                var itemName = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(itemName))
                {
                    Console.WriteLine("Name cannot be blank. Please try again.");
                    goto itemName;
                }
                menuItem.MealName = itemName;

                Console.WriteLine("Enter item's description.");
            itemDescription:
                var itemDescription = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(itemDescription))
                {
                    Console.WriteLine("Description cannot be blank. Please try again.");
                    goto itemDescription;
                }
                menuItem.MealDescription = itemDescription;

                Console.WriteLine("Enter ingredients, separating each ingedient with a comma (e.g. \"eggs, flour, milk\").");
            ingredients:
                var ingredientList = Console.ReadLine();
                if (ingredientList.Contains(',') && ingredientList.Length >= 3 == true)
                    {
                        menuItem.Ingredients = ingredientList.Replace(" ","").Split(',').ToList();
                    }
                else
                    {
                        Console.WriteLine("Invalid input. Try again.");
                        goto ingredients;
                    }

                Console.WriteLine("Enter price of item.");
            price:
                double price;
                try
                {
                    price = Convert.ToDouble(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input. Enter price again.");
                    goto price;
                }
                menuItem.Price = price;

                menuItem = repo.CreateMenuItems(menuItem);
                Console.WriteLine("New menu item:");
                PrintMenuItem(menuItem);


                break;

            case "update":
                Dictionary<string, string> updateArguments = new Dictionary<string, string>();

                Console.WriteLine("Please provide the meal number of the menu item you would like to update.");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Would you like to update the meal name? Answer \"y\" to update or any other key to continue.");
                var userAnswer = Console.ReadLine();
                if(userAnswer == "y")
                {
                Console.WriteLine("Please provide a new meal name");
                var mealName = Console.ReadLine();
                    updateArguments.Add("MealName", mealName);
                }

                Console.WriteLine("Would you like to update the meal description? Answer \"y\" to update or any other key to continue.");
                var userAnswer2 = Console.ReadLine();
                if (userAnswer2 == "y")
                {
                    Console.WriteLine("Please provide a new meal description");
                    var mealDescription = Console.ReadLine();
                    updateArguments.Add("Description", mealDescription);

                }

                Console.WriteLine("Would you like to update the meal ingredients? Answer \"y\" to update or any other key to continue.");
                var userAnswer3 = Console.ReadLine();
                if (userAnswer3 == "y")
                {
                    Console.WriteLine("Update ingredients, separating each ingedient with a comma (e.g. \"eggs, flour, milk\").");
                ingredients2:
                    var updatedIngredients = Console.ReadLine();
                    if (updatedIngredients.Contains(',') && updatedIngredients.Length >= 3 == true)
                    {
                        updateArguments.Add("Ingredients", updatedIngredients);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try again.");
                        goto ingredients2;
                    }
             
                }

                Console.WriteLine("Would you like to update the price? Answer \"y\" to update or any other key to continue.");
                var userAnswer4 = Console.ReadLine();
                if (userAnswer4 == "y")
                {
                    Console.WriteLine("Please provide a new price");
                    var mealPrice = Console.ReadLine();
                    updateArguments.Add("Price", mealPrice);

                }

                var updatedMenuItem = repo.Update(id, updateArguments);
                Console.WriteLine("Updated menu item:");
                PrintMenuItem(updatedMenuItem);
                break;   

            case "delete":
                Console.WriteLine("Please enter the meal number of the menu item you would like to delete");
                var mealId = Convert.ToInt32(Console.ReadLine());
                var itemForDeletion = repo.Find(mealId);
                Console.WriteLine("Are you sure you want to delete " + itemForDeletion.MealName + "? Press \"c\" to confirm or any other key to exit.");
                var confirmation = Console.ReadLine();
                if(confirmation == "c")
                {
                    Console.WriteLine("The following menu item has been deleted: ");
                PrintMenuItem(repo.Delete(mealId));
                break;
                }
                else
                {
                    Console.WriteLine(itemForDeletion.MealName + " was not deleted.");
                    break;
                }

                default:
                Console.WriteLine("\nInvalid command. Please enter a valid command. Type \"help\" for a list of valid commands.");
                break;
        }
        goto input;
    }
    public void HelpMenu()
    {
        Console.WriteLine("\tmenu -- see all menu items");
        Console.WriteLine();
        Console.WriteLine("\tnew item -- create new menu item(s)");
        Console.WriteLine();
        Console.WriteLine("\tupdate -- edit menu items");
        Console.WriteLine();
        Console.WriteLine("\tdelete -- delete menu items");
    }

    public void NewItem()
    { 
        
    }

    public void ShowMenu()
    {
        if (repo.Read().Count == 0)
            Console.WriteLine("There are currently no items in the menu database.");
        foreach (MenuItem menuItem in repo.Read())
        PrintMenuItem(menuItem);
    }

    public void PrintMenuItem(MenuItem menuItem)
    {
        Console.WriteLine("\tMeal Number: " + menuItem.MealNumber);
        Console.WriteLine("\t\tMeal Name: " + menuItem.MealName);
        Console.WriteLine("\t\tDescription: " + menuItem.MealDescription);
        Console.WriteLine("\t\tIngredients: ");
        foreach (String ingredient in menuItem.Ingredients)
        {
            Console.WriteLine("\t\t\t" + ingredient);
        }
        Console.WriteLine("\t\tPrice: " + menuItem.Price);
        Console.WriteLine();
    }
}

