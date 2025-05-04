using EFEjercicio1.Consola.Validators;
using EFEjercicio1.Ioc;
using EFEjercicio1.Service.Interfaces;
using EFEjercicio1Data;
using EFEjercicio1Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;


namespace EFEjercicio1.Consola
{
    internal class Program
    {
        static IServiceProvider _serviceProvider = null!;

        static void Main(string[] args)
        {
            _serviceProvider = DI.ConfigureDI();
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
                    //case "1":
                    //    ListDrinks();
                    //    break;
                    //case "2":
                    //    AddDrinks();
                    //    break;
                    //case "3":
                    //    DeleteDrinks();
                    //    break;
                    //case "4":
                    //    EditDrinks();
                    //    break;
                    //case "r":
                    //    return;
                    //default:
                    //    break;
                }
            } while (true);
        }

        //private static void EditDrinks()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Editing Drinks");
        //    Console.WriteLine("list Of Drinks to Edit");

        //    using (var context = new ConfectioneryContext())
        //    {
        //        var drinks = context.Drinks.OrderBy(b => b.Id)
        //            .Select(b => new
        //            {
        //                b.Id,
        //                b.Name
        //            }).ToList();
        //        foreach (var item in drinks)
        //        {
        //            Console.WriteLine($"{item.Id}-{item.Name}");
        //        }
        //        Console.Write("Enter DrinkID to edit (0 to Escape):");
        //        int drinkId = int.Parse(Console.ReadLine()!);
        //        if (drinkId < 0)
        //        {
        //            Console.WriteLine("Invalid DrinkID... ");
        //            Console.ReadLine();
        //            return;
        //        }
        //        if (drinkId == 0)
        //        {
        //            Console.WriteLine("Cancelled by user");
        //            Console.ReadLine();
        //            return;
        //        }

        //        var drinkInDb = context.Drinks.Include(b => b.Confectionery)
        //            .FirstOrDefault(b => b.Id == drinkId);
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

        //        Console.WriteLine($"Current Drink Confectionery:{drinkInDb.Confectionery}");
        //        Console.WriteLine("Available Confectionery");
        //        var confectioneries = context.Confectioneries
        //            .OrderBy(a => a.Id)
        //            .ToList();
        //        foreach (var confectionery in confectioneries)
        //        {
        //            Console.WriteLine($"{confectionery.Id}-{confectionery}");
        //        }
        //        Console.Write("Enter ConfectioneryID (or ENTER to Keep the same or 0 New Author):");
        //        var newConfectionery = Console.ReadLine();
        //        if (!string.IsNullOrEmpty(newConfectionery))
        //        {
        //            if (!int.TryParse(newConfectionery, out int confectioneryId) || confectioneryId < 0)
        //            {
        //                Console.WriteLine("You enter an invalid ConfectioneryId");
        //                Console.ReadLine();
        //                return;
        //            }
        //            if (confectioneryId > 0)
        //            {
        //                var existConfectionery = context.Confectioneries.Any(a => a.Id == confectioneryId);
        //                if (!existConfectionery)
        //                {
        //                    Console.WriteLine("ConfectioneryId not found");
        //                    Console.ReadLine();
        //                    return;
        //                }
        //                drinkInDb.ConfectioneryId = confectioneryId;

        //            }
        //            else
        //            {
        //                //Entering new confectionery
        //                Console.WriteLine("Adding a New Confectionery");
        //                Console.Write("Enter Name:");
        //                var name = Console.ReadLine();
        //                var existingConfectionery = context.Confectioneries.FirstOrDefault(
        //                        a => a.Name.ToLower() == name!.ToLower());

        //                if (existingConfectionery is not null)
        //                {
        //                    Console.WriteLine("You have entered an existing confectionery!!!");
        //                    Console.WriteLine("Assigning his ConfectioneryID");

        //                    drinkInDb.ConfectioneryId = existingConfectionery.Id;
        //                }
        //                else
        //                {
        //                    Confectionery Confectionery = new Confectionery
        //                    {
        //                        Name = name ?? string.Empty
        //                    };

        //                    var validationContext = new ValidationContext(Confectionery);
        //                    var errorMessages = new List<ValidationResult>();

        //                    bool isValid = Validator.TryValidateObject(Confectionery, validationContext, errorMessages, true);

        //                    if (isValid)
        //                    {
        //                        context.Confectioneries.Add(Confectionery);
        //                        context.SaveChanges();
        //                        drinkInDb.ConfectioneryId = Confectionery.Id;
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

        //        var originalDrink = context.Drinks
        //            .AsNoTracking()
        //            .FirstOrDefault(a => a.Id == drinkInDb.Id);

        //        Console.Write($"Are you sure to edit \"{originalDrink!.Name}\"? (y/n):");
        //        var confirm = Console.ReadLine();
        //        try
        //        {
        //            if (confirm?.ToLower() == "y")
        //            {
        //                context.SaveChanges();
        //                Console.WriteLine("Drink successfully edited");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Operation cancelled by user");
        //            }

        //        }
        //        catch (Exception ex)
        //        {

        //            Console.WriteLine(ex.Message);
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
        //    using (var context = new ConfectioneryContext())
        //    {
        //        var drinks = context.Drinks
        //            .OrderBy(d => d.Id)
        //            .Select(d => new
        //            {
        //                d.Id,
        //                d.Name
        //            }).ToList();
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
        //        var deleteDrink = context.Drinks.Find(drinkId);
        //        if (deleteDrink is null)
        //        {
        //            Console.WriteLine("Drink does not exist!!!");
        //        }
        //        else
        //        {
        //            context.Drinks.Remove(deleteDrink);
        //            context.SaveChanges();
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
        //    Console.Write("Enter drinks's name:");
        //    var name = Console.ReadLine();
        //    Console.Write("Enter Size:");
        //    var size = Console.ReadLine();
        //    Console.WriteLine("List of Confectioneries to Select");
        //    using (var context = new ConfectioneryContext())
        //    {
        //        var confectioneriesList = context.Confectioneries
        //            .OrderBy(c => c.Id)
        //            .ToList();
        //        foreach (var confectionery in confectioneriesList)
        //        {
        //            Console.WriteLine($"{confectionery.Id} - {confectionery}");
        //        }
        //        Console.Write("Enter ConfectioneryID (0 New Confectionery):");
        //        if (!int.TryParse(Console.ReadLine(), out var confectioneryId) || confectioneryId < 0)
        //        {
        //            Console.WriteLine("Invalid AuthorID....");
        //            Console.ReadLine();
        //            return;
        //        }
        //        if (confectioneryId > 0)
        //        {
        //            var selectedConfectionery = context.Confectioneries.Find(confectioneryId);
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
        //                bool exist = context.Drinks.Any(d => d.Name.ToLower() == name!.ToLower() &&
        //                    d.ConfectioneryId == confectioneryId);
        //                var existingDrink = context.Drinks.FirstOrDefault(d => d.Name.ToLower() == name!.ToLower() &&
        //                    d.ConfectioneryId == confectioneryId);

        //                if (existingDrink is null)
        //                {
        //                    context.Drinks.Add(newDrink);
        //                    context.SaveChanges();
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
        //            //Entering new confectionery
        //            Console.WriteLine("Adding a New Confectionery");
        //            Console.Write("Enter Name:");
        //            var nameC = Console.ReadLine();

        //            var existingConfectionery = context.Confectioneries.FirstOrDefault(
        //                    c => c.Name.ToLower() == nameC!.ToLower());
        //            if (existingConfectionery is not null)
        //            {
        //                Console.WriteLine("You have entered an existing confectionery!!!");
        //                Console.WriteLine("Assigning his ConfectioneryID");

        //                var newdrink = new Drink
        //                {
        //                    Name = name ?? string.Empty,
        //                    Size = size ?? string.Empty,
        //                    ConfectioneryId = existingConfectionery.Id
        //                };

        //                var drinksValidator = new DrinksValidator();
        //                var validationResult = drinksValidator.Validate(newdrink);

        //                if (validationResult.IsValid)
        //                {
        //                    var existingDrink = context.Drinks.FirstOrDefault(d => d.Name.ToLower() == name!.ToLower() &&
        //                        d.ConfectioneryId == confectioneryId);

        //                    if (existingDrink is null)
        //                    {
        //                        context.Drinks.Add(newdrink);
        //                        context.SaveChanges();
        //                        Console.WriteLine("Drink Successfully Added!!!");

        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("Drink duplicated!!!");
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
        //                        var existingDrink = context.Drinks.FirstOrDefault(d => d.Name.ToLower() == name!.ToLower() &&
        //                            d.ConfectioneryId == confectioneryId);

        //                        if (existingDrink is null)
        //                        {
        //                            context.Add(newDrink);
        //                            context.SaveChanges();
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
        //    using (var context = new ConfectioneryContext())
        //    {
        //        var drinks = context.Drinks
        //            .Include(d => d.Confectionery)
        //            .OrderBy(c => c.Name)
        //            .ToList();
        //        foreach (var drink in drinks)
        //        {
        //            Console.WriteLine($"{drink} - Confectionery: {drink.Confectionery}");
        //        }
        //        Console.WriteLine("ENTER to continue");
        //        Console.ReadLine();
        //    }
        //}

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
                Console.WriteLine("5 - List of Confectioneries With Drinks");
                Console.WriteLine("6 - Confectioneries With Drinks (Summary or Details)");
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
                    case "5":
                        ListOfConfectioneriesWithDrinks();
                        break;
                    case "6":
                        ConfectioneriesWithDrinksSummaryOrDetails();
                        break;
                    case "r":
                        return;
                    default:
                        break;
                }
            } while (true);
        }



        private static void ConfectioneriesWithDrinksSummaryOrDetails()
        {
            Console.Clear();
            Console.WriteLine("List of Confectioneries");
            Console.WriteLine("Show (1) Summary or (2) Details?");
            var option = Console.ReadLine();

            using (var scope = _serviceProvider.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IConfectioneryService>();
                var confectioneriesWithDrinks = _service.GetAllWithDrinks();

                foreach (var c in confectioneriesWithDrinks)
                {
                    Console.WriteLine($"{c.Id} - {c} (Drinks: {c.Drinks.Count})");

                    if (option == "2")
                    {
                        if (c.Drinks.Any())
                        {
                            Console.WriteLine("Drinks:");
                            foreach (var drink in c.Drinks)
                            {
                                Console.WriteLine($"  - {drink.Name} - {drink.Size}");
                            }
                        }
                        else
                        {
                            Console.WriteLine(" NO Drinks available.");
                        }
                    }
                }
            }

            Console.WriteLine("ENTER to continue...");
            Console.ReadLine();

        }


        private static void ListOfConfectioneriesWithDrinks()
        {

            Console.Clear();
            Console.WriteLine("List of Confectioneries With Drinks");
            using (var scope = _serviceProvider.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IConfectioneryService>();
                var confectioneryGroups = _service.ConfectioneriesGroupIdDrinks();
                foreach (var group in confectioneryGroups)
                {
                    Console.WriteLine($"ConfectioneryID: {group.Key}");
                    var confectionery = _service.GetById(group.Key);
                    Console.WriteLine($"Confectionery: {confectionery}");
                    foreach (var drink in group) 
                    {
                        Console.WriteLine($"    {drink.Name}");
                    }
                    Console.WriteLine($"Drinks Count: {group.Count()}");
                }
            }
            Console.ReadLine();

        }

        private static void EditConfectioneries()
        {
            Console.Clear();
            Console.WriteLine("Edit an confectionery");
            using (var scope = _serviceProvider.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IConfectioneryService>();
                var confectioneries = _service.GetAll("Id");
                foreach (var confectionery in confectioneries)
                {
                    Console.WriteLine($"{confectionery.Id} - {confectionery}");
                }
                Console.WriteLine("Enter an ConfectioneryId to edit (0 to escape): ");
                int confectioneryId;
                if (!int.TryParse(Console.ReadLine(), out confectioneryId) || confectioneryId <= 0)
                {
                    Console.WriteLine("Invalid ConfectioneryId!");
                    Console.ReadLine();
                    return;
                }
                if (confectioneryId == 0) return;

                var confectioneryInDb = _service.GetById(confectioneryId);
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

                var ogConfectionery = _service.GetById(confectioneryId);

                Console.Write($"Are you sure to edit \"{ogConfectionery!.Name}\"? (y/n): ");
                var confirm = Console.ReadLine();
                if (confirm?.ToLower() == "y")
                {
                    bool exist = _service.Exist(confectioneryInDb.Name,
                        confectioneryInDb.Id);
                    if (!exist)
                    {
                        var confectioneryValidator = new ConfectioneriesValidator();
                        var result = confectioneryValidator.Validate(confectioneryInDb);

                        if (result.IsValid)
                        {
                            _service.Save(confectioneryInDb);
                            Console.WriteLine("Confectionery successfully edited");
                        }
                        else
                        {
                            foreach (var message in result.Errors)
                            {
                                Console.WriteLine(message);
                            }
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Confectionery already exist!!!");
                    }
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
            using (var scope = _serviceProvider.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IConfectioneryService>();
                var confectioneries = _service.GetAll("Id");
                foreach (var confectionery in confectioneries)
                {
                    Console.WriteLine($"{confectionery.Id} - {confectionery}");
                }
                Console.WriteLine("Enter an ConfectioneryId to delete (0 to escape): ");
                int confectioneryId;
                if (!int.TryParse(Console.ReadLine(), out confectioneryId) || confectioneryId < 0)
                {
                    Console.WriteLine("Invalid ConfectioneryId!");
                    Console.ReadLine();
                    return;
                }
                if (confectioneryId == 0) return;
                               
                var confectioneryInDb = _service.GetById(confectioneryId, true);
                if (confectioneryInDb == null)
                {
                    Console.WriteLine("Confectionery does not exist");
                    Console.ReadLine();
                    return;
                }

                var hasDrinks = _service.HasDrinks(confectioneryId);

                if (!hasDrinks)
                {
                    Console.Write($"Are you sure to delete \"{confectioneryInDb.Name}\"? (y/n): ");
                    var confirm = Console.ReadLine();
                    if (confirm?.ToLower() == "y")
                    {
                        _service.Delete(confectioneryId);
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
                    _service.LoadDrinks(confectioneryInDb);
                    foreach (var drink in confectioneryInDb.Drinks!)
                    {
                        Console.WriteLine($"{drink.Name}");
                    }
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
            using (var scope = _serviceProvider.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService<IConfectioneryService>();
                bool exist = _service.Exist(name);  
                
                if (!exist)
                {
                    var confectionery = new Confectionery
                    {
                        Name = name ?? string.Empty
                    };

                    var confectioneryValidator = new ConfectioneriesValidator();

                    var result = confectioneryValidator.Validate(confectionery);

                    if (result.IsValid)
                    {
                        _service.Save(confectionery);
                        Console.WriteLine("Confectionery Succesfully Added");
                    }
                    else
                    {
                        foreach (var message in result.Errors)
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
            using (var scope = _serviceProvider.CreateScope())
            {
                var _service = scope.ServiceProvider.GetRequiredService
                    <IConfectioneryService>();
                var confectioneries = _service.GetAll();

                foreach (var confectionery in confectioneries)
                {
                    Console.WriteLine(confectionery);
                }
            }
            Console.WriteLine("ENTER to continue");
            Console.ReadLine();

        }
    }
}
