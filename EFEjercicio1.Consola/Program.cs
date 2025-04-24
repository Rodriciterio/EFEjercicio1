using EFEjercicio1.Consola.Validators;
using EFEjercicio1Data;
using EFEjercicio1Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EFEjercicio1.Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CreateDb();
            do
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1 - Confectioneries");
                Console.WriteLine("2 - Drinks");
                Console.WriteLine("x - Exit");
                Console.Write("Enter an option: ");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ConfectioneriesMenu();
                        break;
                    case "2":
                        DrinksMenu();
                        break;
                    case "x":
                        Console.WriteLine("Fin del programa");
                        return;
                    default:
                        break;
                }
            } while (true);

        }

        private static void DrinksMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("DRINKS");
                Console.WriteLine("1 - List of Drinks");
                Console.WriteLine("2 - Add New Drink");
                Console.WriteLine("3 - Delete and Drink");
                Console.WriteLine("4 - Edit an Drink");
                Console.WriteLine("r - Return");
                Console.Write("Enter an option: ");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ListDrinks();
                        break;
                    case "2":
                        AddDrinks();
                        break;
                    case "3":
                        DeleteDrinks();
                        break;
                    case "4":
                        EditDrinks();
                        break;
                    case "r":
                        return;
                    default:
                        break;
                }
            } while (true);
        }

        private static void EditDrinks()
        {
            Console.Clear();
            Console.WriteLine("Editing Drinks");
            Console.WriteLine("list Of Drinks to Edit");

            using (var context = new ConfectioneryContext())
            {
                var drinks = context.Drinks.OrderBy(b => b.Id)
                    .Select(b => new
                    {
                        b.Id,
                        b.Name
                    }).ToList();
                foreach (var item in drinks)
                {
                    Console.WriteLine($"{item.Id}-{item.Name}");
                }
                Console.Write("Enter DrinkID to edit (0 to Escape):");
                int drinkId = int.Parse(Console.ReadLine()!);
                if (drinkId < 0)
                {
                    Console.WriteLine("Invalid DrinkID... ");
                    Console.ReadLine();
                    return;
                }
                if (drinkId == 0)
                {
                    Console.WriteLine("Cancelled by user");
                    Console.ReadLine();
                    return;
                }

                var drinkInDb = context.Drinks.Include(b => b.Confectionery)
                    .FirstOrDefault(b => b.Id == drinkId);
                if (drinkInDb == null)
                {
                    Console.WriteLine("Drink does not exist...");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine($"Current Drink Name: {drinkInDb.Name}");
                Console.Write("Enter New Name (or ENTER to Keep the same):");
                var newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    drinkInDb.Name = newName;
                }
                Console.WriteLine($"Current Drink Size: {drinkInDb.Size}");
                Console.Write("Enter New Size (or ENTER to Keep the same):");
                var newSize = Console.ReadLine();
                if (!string.IsNullOrEmpty(newSize))
                {
                    drinkInDb.Size = newSize;
                }

                Console.WriteLine($"Current Drink Confectionery:{drinkInDb.Confectionery}");
                Console.WriteLine("Available Confectionery");
                var confectioneries = context.Confectioneries
                    .OrderBy(a => a.Id)
                    .ToList();
                foreach (var confectionery in confectioneries)
                {
                    Console.WriteLine($"{confectionery.Id}-{confectionery}");
                }
                Console.Write("Enter ConfectioneryID (or ENTER to Keep the same or 0 New Author):");
                var newConfectionery = Console.ReadLine();
                if (!string.IsNullOrEmpty(newConfectionery))
                {
                    if (!int.TryParse(newConfectionery, out int confectioneryId) || confectioneryId < 0)
                    {
                        Console.WriteLine("You enter an invalid ConfectioneryId");
                        Console.ReadLine();
                        return;
                    }
                    if (confectioneryId > 0)
                    {
                        var existConfectionery = context.Confectioneries.Any(a => a.Id == confectioneryId);
                        if (!existConfectionery)
                        {
                            Console.WriteLine("ConfectioneryId not found");
                            Console.ReadLine();
                            return;
                        }
                        drinkInDb.ConfectioneryId = confectioneryId;

                    }
                    else
                    {
                        //Entering new confectionery
                        Console.WriteLine("Adding a New Confectionery");
                        Console.Write("Enter Name:");
                        var name = Console.ReadLine();
                        var existingConfectionery = context.Confectioneries.FirstOrDefault(
                                a => a.Name.ToLower() == name!.ToLower());

                        if (existingConfectionery is not null)
                        {
                            Console.WriteLine("You have entered an existing confectionery!!!");
                            Console.WriteLine("Assigning his ConfectioneryID");

                            drinkInDb.ConfectioneryId = existingConfectionery.Id;
                        }
                        else
                        {
                            Confectionery Confectionery = new Confectionery
                            {
                                Name = name ?? string.Empty
                            };

                            var validationContext = new ValidationContext(Confectionery);
                            var errorMessages = new List<ValidationResult>();

                            bool isValid = Validator.TryValidateObject(Confectionery, validationContext, errorMessages, true);

                            if (isValid)
                            {
                                context.Confectioneries.Add(Confectionery);
                                context.SaveChanges();
                                drinkInDb.ConfectioneryId = Confectionery.Id;
                            }
                            else
                            {
                                foreach (var message in errorMessages)
                                {
                                    Console.WriteLine(message);
                                }
                            }

                        }
                    }

                }

                var originalDrink = context.Drinks
                    .AsNoTracking()
                    .FirstOrDefault(a => a.Id == drinkInDb.Id);

                Console.Write($"Are you sure to edit \"{originalDrink!.Name}\"? (y/n):");
                var confirm = Console.ReadLine();
                try
                {
                    if (confirm?.ToLower() == "y")
                    {
                        context.SaveChanges();
                        Console.WriteLine("Drink successfully edited");
                    }
                    else
                    {
                        Console.WriteLine("Operation cancelled by user");
                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
                return;


            }

        }

        private static void DeleteDrinks()
        {
            Console.Clear();
            Console.WriteLine("Deleting Drinks");
            Console.WriteLine("List of Drinks to Delete");
            using (var context = new ConfectioneryContext())
            {
                var drinks = context.Drinks
                    .OrderBy(d=>d.Id)
                    .Select(d => new
                {
                    d.Id,
                    d.Name
                }).ToList();
                foreach (var drin in drinks)
                {
                    Console.WriteLine($"{drin.Id} - {drin.Name}");
                }
                Console.Write("Select DrinkId to delete (0 to escape): ");
                if (!int.TryParse(Console.ReadLine(), out int drinkId) || drinkId < 0)
                {
                    Console.WriteLine("Invalid DrinkID...");
                    Console.ReadLine();
                    return;
                }
                if (drinkId == 0)
                {
                    Console.WriteLine("Cancelled by user");
                    Console.ReadLine();
                    return;
                }
                var deleteDrink = context.Drinks.Find(drinkId);
                if (deleteDrink is null)
                {
                    Console.WriteLine("Drink does not exist!!!");
                }
                else
                {
                    context.Drinks.Remove(deleteDrink);
                    context.SaveChanges();
                    Console.WriteLine("Drink successfuly Deleted");
                }
                Console.ReadLine();
                return;
            }
        }

        private static void AddDrinks()
        {
            Console.Clear();
            Console.WriteLine("Adding a new Drink");
            Console.Write("Enter Name´s Drink: ");
            var name = Console.ReadLine();
            Console.Write("Enter Name´s Size: ");
            var size = Console.ReadLine();
            Console.WriteLine("List of Confectioneries to select");

            using (var context = new ConfectioneryContext())
            {
                var confectioneriesList = context.Confectioneries
                    .OrderBy(c => c.Id)
                    .ToList();
                foreach (var confectionery in confectioneriesList)
                {
                    Console.WriteLine($"{confectionery.Id} - {confectionery.Name}");
                }
                Console.Write("Enter ConfectioneryId (0 New Confectionery): ");
                if (!int.TryParse(Console.ReadLine(), out var confectioneryId) || confectioneryId < 0)
                {
                    Console.WriteLine("Invalid ConfectioneryId...");
                    Console.ReadLine();
                    return;
                }
                var selectedConfectionery = context.Confectioneries.Find(confectioneryId);
                if (selectedConfectionery is null)
                {
                    Console.WriteLine("Confectionery not found!");
                    Console.ReadLine();
                    return;
                }
                var newDrink = new Drink
                {
                    Name = name ?? string.Empty,
                    Size = size ?? string.Empty,
                    ConfectioneryId = confectioneryId
                };

                var drinkValidator = new DrinksValidator();
                var validationResult = drinkValidator.Validate(newDrink);

                if (validationResult.IsValid)
                {
                    bool exist = context.Drinks.Any(d => d.Name.ToLower() == name!.ToLower() && d.Size.ToLower() == size!.ToLower()
                           && d.ConfectioneryId == confectioneryId);
                    //var existingDrink = context.Drinks.FirstOrDefault(d => d.Name == name && d.Size == size
                    //&& d.ConfectioneryId == confectioneryId);

                    if (!exist)
                    {
                        context.Drinks.Add(newDrink);
                        context.SaveChanges();
                        Console.WriteLine("Drink Successfully Added!");
                    }
                    else
                    {
                        Console.WriteLine("Drink duplicated!!");
                    }
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                    {
                        Console.WriteLine(error);
                    }
                }
                Console.ReadLine();
                return;
            }
        }

        private static void ListDrinks()
        {
            Console.Clear();
            Console.WriteLine("List of Drinks");
            using (var context = new ConfectioneryContext())
            {
                var drinks = context.Drinks
                    .Include(d => d.Confectionery)
                    .OrderBy(c => c.Name)
                    .ToList();
                foreach (var drink in drinks)
                {
                    Console.WriteLine($"{drink} - Confectionery: {drink.Confectionery}");
                }
                Console.WriteLine("ENTER to continue");
                Console.ReadLine();
            }
        }

        private static void ConfectioneriesMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Confectioneries");
                Console.WriteLine("1 - List of Confectioneries");
                Console.WriteLine("2 - Add New Confectionery");
                Console.WriteLine("3 - Delete and Confectionery");
                Console.WriteLine("4 - Edit an Confectionery");
                Console.WriteLine("r - Return");
                Console.Write("Enter an option: ");
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ListConfectioneries();
                        break;
                    case "2":
                        AddConfectioneries();
                        break;
                    case "3":
                        DeleteConfectioneries();
                        break;
                    case "4":
                        EditConfectioneries();
                        break;
                    case "r":
                        return;
                    default:
                        break;
                }
            } while (true);
        }

        private static void EditConfectioneries()
        {
            Console.Clear();
            Console.WriteLine("Edit an confectionery");
            using (var context = new ConfectioneryContext())
            {
                var confectioneries = context.Confectioneries
                    .OrderBy(c => c.Id)
                    .ToList();
                foreach (var confectionery in confectioneries)
                {
                    Console.WriteLine($"{confectionery.Id} - {confectionery}");
                }
                Console.WriteLine("Enter an ConfectioneryId to edit: ");
                int confectioneryId;
                if (!int.TryParse(Console.ReadLine(), out confectioneryId) || confectioneryId <= 0)
                {
                    Console.WriteLine("Invalid ConfectioneryId!");
                    Console.ReadLine();
                    return;
                }

                var confectioneryInDb = context.Confectioneries.Find(confectioneryId);
                if (confectioneryInDb == null)
                {
                    Console.WriteLine("Confectionery does not exist");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine($"Current Confectionery Name: {confectioneryInDb.Name}");
                Console.Write("Enter New Name");
                var newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    confectioneryInDb.Name = newName;
                }

                var ogConfectionery = context.Confectioneries
                    .AsNoTracking()
                    .FirstOrDefault(c => c.Id == confectioneryInDb.Id);

                Console.Write($"Are you sure to edit \"{ogConfectionery!.Name}\"? (y/n): ");
                var confirm = Console.ReadLine();
                if (confirm?.ToLower() == "y")
                {
                    context.SaveChanges();
                    Console.WriteLine("Confectionery successfully edited");
                }
                else
                {
                    Console.WriteLine("Operation cancelled by user");
                }
                Console.ReadLine();
                return;
            }
        }

        private static void DeleteConfectioneries()
        {
            Console.Clear();
            Console.WriteLine("Delete an confectionery");
            using (var context = new ConfectioneryContext())
            {
                var confectioneries = context.Confectioneries
                    .OrderBy(c => c.Id)
                    .ToList();
                foreach (var confectionery in confectioneries)
                {
                    Console.WriteLine($"{confectionery.Id} - {confectionery}");
                }
                Console.WriteLine("Enter an ConfectioneryId to delete: ");
                int confectioneryId;
                if (!int.TryParse(Console.ReadLine(), out confectioneryId) || confectioneryId <= 0)
                {
                    Console.WriteLine("Invalid ConfectioneryId!");
                    Console.ReadLine();
                    return;
                }

                var confectioneryInDb = context.Confectioneries.Find(confectioneryId);
                if (confectioneryInDb == null)
                {
                    Console.WriteLine("Confectionery does not exist");
                    Console.ReadLine();
                    return;
                }

                var hasDrinks = context.Drinks.Any(d => d.ConfectioneryId == confectioneryInDb.Id);

                if (!hasDrinks)
                {
                    Console.Write($"Are you sure to delete \"{confectioneryInDb.Name}\"? (y/n): ");
                    var confirm = Console.ReadLine();
                    if (confirm?.ToLower() == "y")
                    {
                        context.Confectioneries.Remove(confectioneryInDb);
                        context.SaveChanges();
                        Console.WriteLine("Confectionery successfully removed");
                    }
                    else
                    {
                        Console.WriteLine("Operation cancelled by user");
                    }
                }
                else
                {
                    Console.WriteLine("Confectionery with Drinks!! Delete deny");
                }
                Console.ReadLine();
                return;
            }
        }

        private static void AddConfectioneries()
        {
            Console.Clear();
            Console.WriteLine("Adding a new Confectionery");
            Console.Write("Enter Name:");
            var name = Console.ReadLine();
            using (var context = new ConfectioneryContext())
            {
                bool exist = context.Confectioneries.Any(c => c.Name == name);
                if (!exist)
                {
                    var confectionery = new Confectionery
                    {
                        Name = name ?? string.Empty
                    };

                    var validationContext = new ValidationContext(confectionery);
                    var errorMessages = new List<ValidationResult>();

                    bool isValid = Validator.TryValidateObject(confectionery, validationContext, errorMessages, true);

                    if (isValid)
                    {
                        context.Confectioneries.Add(confectionery);
                        context.SaveChanges();
                        Console.WriteLine("Confectionery Succesfully Added");
                    }
                    else
                    {
                        foreach (var message in errorMessages)
                        {
                            Console.WriteLine(message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Confectionery already exist");
                }
            }
            Console.ReadLine();
        }

        private static void ListConfectioneries()
        {
            Console.Clear();
            Console.WriteLine("List of Confectioneries");
            using (var context = new ConfectioneryContext())
            {
                var confectioneries = context.Confectioneries
                    .OrderBy(c => c.Name)
                    .ToList();
                foreach (var confectionery in confectioneries)
                {
                    Console.WriteLine(confectionery);
                }
                Console.WriteLine("ENTER to continue");
                Console.ReadLine();
            }
        }

        private static void CreateDb()
        {
            using (var context = new ConfectioneryContext())
            {
                context.Database.EnsureCreated();
            }
            Console.WriteLine("Database created!");
        }

    }
}
