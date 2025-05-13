using EFEjercicio1.Consola.Validators;
using EFEjercicio1.Ioc;
using EFEjercicio1.Service.Interfaces;
using EFEjercicio1Entities;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;


namespace EFEjercicio1.Consola
{
    internal class Program
    {
        static IServiceProvider _serviceProvider = null!;

        static void Main(string[] args)
        {
            _serviceProvider = DI.ConfigureDI();
            var confectioneryMenu = new ConfectioneryMenu(_serviceProvider);
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
                        confectioneryMenu.MostrarMenu();
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
                        //ListDrinks();
                        break;
                    case "2":
                        //AddDrinks();
                        break;
                    case "3":
                        //DeleteDrinks();
                        break;
                    case "4":
                        //EditDrinks();
                        break;
                    case "r":
                        return;
                    default:
                        break;
                }
            } while (true);
        }

        //private static void EditDrinks()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Editing Drinks");
        //    Console.WriteLine("List of Drinks to Edit");

        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var _service = scope.ServiceProvider.GetRequiredService<IDrinkService>();
        //        var _serviceC = scope.ServiceProvider.GetRequiredService<IConfectioneryService>();

        //        var drinks = _service.GetAll();
        //        foreach (var item in drinks)
        //        {
        //            Console.WriteLine($"{item.Id}-{item.Name}");
        //        }

        //        Console.Write("Enter DrinkID to edit (0 to Escape):");
        //        int drinkId = int.Parse(Console.ReadLine()!);
        //        if (drinkId == 0)
        //        {
        //            Console.WriteLine("Cancelled by user");
        //            Console.ReadLine();
        //            return;
        //        }

        //        var drinkInDb = _service.GetById(drinkId, tracked: true);
        //        if (drinkInDb == null)
        //        {
        //            Console.WriteLine("Drink does not exist...");
        //            Console.ReadLine();
        //            return;
        //        }

        //        Console.WriteLine($"Current Drink Name: {drinkInDb.Name}");
        //        Console.Write("Enter New Name (or ENTER to Keep the same):");
        //        var newName = Console.ReadLine();
        //        if (!string.IsNullOrEmpty(newName))
        //        {
        //            drinkInDb.Name = newName;
        //        }

        //        Console.WriteLine($"Current Drink Size: {drinkInDb.Size}");
        //        Console.Write("Enter New Size (or ENTER to Keep the same):");
        //        var newSize = Console.ReadLine();
        //        if (!string.IsNullOrEmpty(newSize))
        //        {
        //            drinkInDb.Size = newSize;
        //        }

        //        Console.WriteLine($"Current Drink Confectionery: {drinkInDb.Confectionery?.Name}");
        //        Console.WriteLine("Available Confectioneries");
        //        var confectioneries = _serviceC.GetAll("Id");
        //        foreach (var confectionery in confectioneries)
        //        {
        //            Console.WriteLine($"{confectionery.Id}-{confectionery.Name}");
        //        }

        //        Console.Write("Enter ConfectioneryID (or ENTER to Keep the same or 0 for New Confectionery):");
        //        var newConfectioneryInput = Console.ReadLine();
        //        if (!string.IsNullOrEmpty(newConfectioneryInput))
        //        {
        //            if (!int.TryParse(newConfectioneryInput, out int confectioneryId) || confectioneryId < 0)
        //            {
        //                Console.WriteLine("Invalid ConfectioneryId entered");
        //                Console.ReadLine();
        //                return;
        //            }

        //            if (confectioneryId > 0)
        //            {
        //                var existConfectionery = _serviceC.GetById(confectioneryId);
        //                if (existConfectionery == null)
        //                {
        //                    Console.WriteLine("ConfectioneryId not found");
        //                    Console.ReadLine();
        //                    return;
        //                }

        //                drinkInDb.ConfectioneryId = confectioneryId;
        //            }
        //            else
        //            {
        //                // Entering a new confectionery
        //                Console.WriteLine("Adding a New Confectionery");
        //                Console.Write("Enter Name:");
        //                var confectioneryName = Console.ReadLine();
        //                var existingConfectionery = _serviceC.GetByName(confectioneryName);

        //                if (existingConfectionery != null)
        //                {
        //                    Console.WriteLine("This confectionery already exists! Assigning its ID.");
        //                    drinkInDb.ConfectioneryId = existingConfectionery.Id;
        //                }
        //                else
        //                {
        //                    var newConfectionery = new Confectionery
        //                    {
        //                        Name = confectioneryName ?? string.Empty
        //                    };

        //                    var validationContext = new ValidationContext(newConfectionery);
        //                    var errorMessages = new List<ValidationResult>();

        //                    bool isValid = Validator.TryValidateObject(newConfectionery, validationContext, errorMessages, true);
        //                    if (isValid)
        //                    {
        //                        _serviceC.Save(newConfectionery);
        //                        drinkInDb.ConfectioneryId = newConfectionery.Id;
        //                    }
        //                    else
        //                    {
        //                        foreach (var message in errorMessages)
        //                        {
        //                            Console.WriteLine(message);
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        var originalDrink = _service.GetById(drinkInDb.Id, tracked: false);

        //        Console.Write($"Are you sure you want to edit \"{originalDrink!.Name} - {originalDrink.Size}\"? (y/n):");
        //        var confirm = Console.ReadLine();

        //        try
        //        {
        //            if (confirm?.ToLower() == "y")
        //            {
        //                _service.Edit(drinkInDb); 
        //                Console.WriteLine("Drink successfully edited");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Operation cancelled by user");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error: {ex.Message}");
        //        }

        //        Console.ReadLine();
        //        return;
        //    }
        //}



        //private static void DeleteDrinks()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Deleting Drinks");
        //    Console.WriteLine("List of Drinks to Delete");
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var _service = scope.ServiceProvider.GetRequiredService<IDrinkService>();
        //        var drinks = _service.GetAll();

        //        foreach (var drin in drinks)
        //        {
        //            Console.WriteLine($"{drin.Id} - {drin.Name}");
        //        }
        //        Console.Write("Select DrinkId to delete (0 to escape): ");
        //        if (!int.TryParse(Console.ReadLine(), out int drinkId) || drinkId < 0)
        //        {
        //            Console.WriteLine("Invalid DrinkID...");
        //            Console.ReadLine();
        //            return;
        //        }
        //        if (drinkId == 0)
        //        {
        //            Console.WriteLine("Cancelled by user");
        //            Console.ReadLine();
        //            return;
        //        }
        //        var deleteDrink = _service.GetById(drinkId);
        //        if (deleteDrink is null)
        //        {
        //            Console.WriteLine("Drink does not exist!!!");
        //        }
        //        else
        //        {
        //            _service.Delete(drinkId);
        //            Console.WriteLine("Drink successfuly Deleted");
        //        }
        //        Console.ReadLine();
        //        return;
        //    }
        //}

        //private static void AddDrinks()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Adding New Drinks");
        //    Console.Write("Enter drinks's name: ");
        //    var name = Console.ReadLine();
        //    Console.Write("Enter Size: ");
        //    var size = Console.ReadLine();
        //    Console.WriteLine("List of Confectioneries to Select");

        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var _service = _serviceProvider.GetRequiredService<IDrinkService>();
        //        var _serviceC = _serviceProvider.GetRequiredService<IConfectioneryService>();

        //        var confectioneriesList = _serviceC.GetAll()
        //            .OrderBy(c => c.Id)
        //            .ToList();
        //        foreach (var confectionery in confectioneriesList)
        //        {
        //            Console.WriteLine($"{confectionery.Id} - {confectionery.Name}");
        //        }

        //        Console.Write("Enter ConfectioneryID (0 for New Confectionery): ");
        //        if (!int.TryParse(Console.ReadLine(), out var confectioneryId) || confectioneryId < 0)
        //        {
        //            Console.WriteLine("Invalid ConfectioneryID....");
        //            Console.ReadLine();
        //            return;
        //        }

        //        if (confectioneryId > 0)
        //        {
        //            var selectedConfectionery = _serviceC.GetById(confectioneryId);
        //            if (selectedConfectionery is null)
        //            {
        //                Console.WriteLine("Confectionery not found!!!");
        //                Console.ReadLine();
        //                return;
        //            }

        //            var newDrink = new Drink
        //            {
        //                Name = name ?? string.Empty,
        //                Size = size ?? string.Empty,
        //                ConfectioneryId = confectioneryId
        //            };

        //            var drinksValidator = new DrinksValidator();
        //            var validationResult = drinksValidator.Validate(newDrink);

        //            if (validationResult.IsValid)
        //            {
        //                bool exists = _service.Exist(name, size, confectioneryId);

        //                if (!exists)
        //                {
        //                    _service.Save(newDrink);
        //                    Console.WriteLine("Drink Successfully Added!!!");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Drink duplicated!!!");
        //                }
        //            }
        //            else
        //            {
        //                foreach (var error in validationResult.Errors)
        //                {
        //                    Console.WriteLine(error);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // Entering new confectionery
        //            Console.WriteLine("Adding a New Confectionery");
        //            Console.Write("Enter Name: ");
        //            var nameC = Console.ReadLine();

        //            var existingConfectionery = _serviceC.GetByName(nameC);

        //            if (existingConfectionery is not null)
        //            {
        //                Console.WriteLine("You have entered an existing confectionery!");
        //                Console.WriteLine("Assigning its ConfectioneryID...");

        //                var newDrink = new Drink
        //                {
        //                    Name = name ?? string.Empty,
        //                    Size = size ?? string.Empty,
        //                    ConfectioneryId = existingConfectionery.Id
        //                };

        //                var drinksValidator = new DrinksValidator();
        //                var validationResult = drinksValidator.Validate(newDrink);

        //                if (validationResult.IsValid)
        //                {
        //                    bool drinkExists = _service.Exist(name, size, existingConfectionery.Id);

        //                    if (!drinkExists)
        //                    {
        //                        _service.Save(newDrink);
        //                        Console.WriteLine("Drink Successfully Added!");
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("Drink duplicated!");
        //                    }
        //                }
        //                else
        //                {
        //                    foreach (var error in validationResult.Errors)
        //                    {
        //                        Console.WriteLine(error);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Confectionery newConfectionery = new Confectionery
        //                {
        //                    Name = nameC ?? string.Empty
        //                };

        //                var validationContext = new ValidationContext(newConfectionery);
        //                var errorMessages = new List<ValidationResult>();

        //                bool isValid = Validator.TryValidateObject(newConfectionery, validationContext, errorMessages, true);

        //                if (isValid)
        //                {
        //                    var newDrink = new Drink
        //                    {
        //                        Name = name ?? string.Empty,
        //                        Size = size ?? string.Empty,
        //                        Confectionery = newConfectionery
        //                    };

        //                    var drinksValidator = new DrinksValidator();
        //                    var validationResult = drinksValidator.Validate(newDrink);

        //                    if (validationResult.IsValid)
        //                    {
        //                        bool existingDrink = _service.Exist(name, size, newConfectionery.Id);

        //                        if (!existingDrink)
        //                        {
        //                            _service.Save(newDrink);
        //                            Console.WriteLine("Drink Successfully Added!!!");
        //                        }
        //                        else
        //                        {
        //                            Console.WriteLine("Drink duplicated!!!");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        foreach (var error in validationResult.Errors)
        //                        {
        //                            Console.WriteLine(error);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    foreach (var message in errorMessages)
        //                    {
        //                        Console.WriteLine(message);
        //                    }
        //                }
        //            }
        //        }
        //        Console.ReadLine();
        //        return;
        //    }
        //}


        //private static void ListDrinks()
        //{
        //    Console.Clear();
        //    Console.WriteLine("List of Drinks");
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var _service = scope.ServiceProvider
        //            .GetRequiredService<IDrinkService>();
        //        var drinks = _service.GetAll();
        //        foreach (var drink in drinks)
        //        {
        //            Console.WriteLine($"{drink} - Confectionery: {drink.Confectionery}");
        //        }
        //        Console.WriteLine("ENTER to continue");
        //        Console.ReadLine();
        //    }
        //}

       
    }
}
