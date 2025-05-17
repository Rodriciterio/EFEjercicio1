using EFEjercicio1.Service.DTOs.Confectionery;
using EFEjercicio1.Service.DTOs.Drink;
using EFEjercicio1.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EFEjercicio1.Consola
{
    public class DrinkMenu
    {
        private readonly IServiceProvider _serviceProvider = null!;

        public DrinkMenu(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void MostrarMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("DRINKS");
                Console.WriteLine("1 - List of Drinks");
                Console.WriteLine("2 - Add New Drink");
                Console.WriteLine("3 - Delete and Drink");
                Console.WriteLine("4 - Edit an Drink");
                Console.WriteLine("5 - Drinks Group By Confectionery");
                Console.WriteLine("r - Return");
                Console.Write("Enter an option: ");
                var option = Console.ReadLine();
                using (var scope = _serviceProvider.CreateScope())
                {
                    var drinkService = scope.ServiceProvider
                        .GetRequiredService<IDrinkService>();
                    var confectioneryService = scope.ServiceProvider
                        .GetRequiredService<IConfectioneryService>();
                    switch (option)
                    {
                        case "1":
                            DrinkList(drinkService);
                            break;
                        case "2":
                            AddDrinks(drinkService, confectioneryService);
                            break;
                        case "3":
                            DeleteDrinks(drinkService);
                            break;
                        case "4":
                            EditDrinks(drinkService, confectioneryService);
                            break;
                        case "5":
                            DrinksGroupByConfectionery(drinkService);
                            break;
                        case "r":
                            return;
                        default:
                            break;
                    }
                }
            } while (true);
        }

        private void EditDrinks(IDrinkService drinkService, IConfectioneryService confectioneryService)
        {
            Console.Clear();
            Console.WriteLine("Editing Drinks");
            Console.WriteLine("list Of Drinks to Edit");

            var drinks = drinkService.GetAll("Id");
            foreach (var item in drinks)
            {
                Console.WriteLine($"{item.Id.ToString().PadLeft(4, ' ')}-{item.Name}");
            }

            Console.Write("Enter DrinkID to edit (0 to Escape):");
            int drinkId = int.Parse(Console.ReadLine()!);
            if (drinkId < 0)
            {
                Console.WriteLine("Invalid DrinkID... ");
                Console.ReadLine();
                return;
            }
            if (drinkId == 0) return;

            var drinkInDb = drinkService.GetById(drinkId);
            if (drinkInDb == null)
            {
                Console.WriteLine("Drink does not exist...");
                Console.ReadLine();
                return;
            }
            DrinkUpdateDto drinkUpdateDto = new DrinkUpdateDto()
            {
                Id = drinkInDb.Id,
                Name = drinkInDb.Name,
                Size = drinkInDb.Size,
                ConfectioneryId = drinkInDb.ConfectioneryId
            };


            Console.WriteLine($"Current Drink Name: {drinkInDb.Name}");
            Console.Write("Enter New Title (or ENTER to Keep the same):");
            var newTitle = Console.ReadLine();
            if (!string.IsNullOrEmpty(newTitle))
            {
                drinkUpdateDto.Name = newTitle;
            }
            Console.WriteLine($"Current Drink Size: {drinkInDb.Size}");
            Console.Write("Enter Drink Size (or ENTER to Keep the same):");
            var newSize = Console.ReadLine();
            if (!string.IsNullOrEmpty(newSize))
            {
                drinkUpdateDto.Size = newSize;
            }

            ConfectioneryDto? confectioneryDto = confectioneryService.GetById(drinkInDb.ConfectioneryId);
            Console.WriteLine($"Current Drink Confectionery:{confectioneryDto!.Name}");
            Console.WriteLine("Available Confectioneries");
            var confectioneries = confectioneryService.GetAll("Id");
            foreach (var confectionery in confectioneries)
            {
                Console.WriteLine($"{confectionery.Id.ToString().PadLeft(4, ' ')}-{confectionery.Name}");
            }
            Console.Write("Enter ConfectioneryId (or ENTER to Keep the same or 0 New Confectionery):");
            var newConfectioneryId = Console.ReadLine();
            if (!string.IsNullOrEmpty(newConfectioneryId))
            {
                if (!int.TryParse(newConfectioneryId, out int confectioneryId) || confectioneryId < 0)
                {
                    Console.WriteLine("You enter an invalid ConfectioneryID");
                    Console.ReadLine();
                    return;
                }
                if (confectioneryId > 0)
                {
                    var existingConfectionery = confectioneryService.GetById(confectioneryId);
                    if (existingConfectionery is null)
                    {
                        Console.WriteLine("ConfectioneryID not found");
                        Console.ReadLine();
                        return;
                    }
                    drinkUpdateDto.ConfectioneryId = confectioneryId;

                }
                else
                {
                    //Entering new confectionery
                    Console.WriteLine("Adding a New Confectionery");
                    Console.Write("Enter Name:");
                    var name = Console.ReadLine();
                  
                    var existingConfectionery = confectioneryService.GetByName(name ?? string.Empty);

                    if (existingConfectionery is not null)
                    {
                        Console.WriteLine("You have entered an existing confectionery!!!");
                        Console.WriteLine("Assigning his ConfectioneryID");

                        drinkUpdateDto.ConfectioneryId = existingConfectionery.Id;
                    }
                    else
                    {
                        ConfectioneryCreateDto newConfectionery = new ConfectioneryCreateDto
                        {
                            Name = name ?? string.Empty
                        };
                        ConfectioneryDto? confectioneryCreated = null;
                        if (confectioneryService.Create(newConfectionery, out confectioneryCreated, out var confectioneryErrors))
                        {
                            Console.WriteLine("New Confectionery Added Successfully");
                            drinkUpdateDto.ConfectioneryId = confectioneryCreated!.Id;
                        }
                        else
                        {
                            Console.WriteLine("Errors while trying to add a new confectionery");
                            foreach (var item in confectioneryErrors)
                            {
                                Console.WriteLine(item);
                            }
                        }


                    }
                }

            }

            var originalDrink = drinkService.GetById(drinkId);

            Console.Write($"Are you sure to edit \"{originalDrink!.Name}\"? (y/n):");
            var confirm = Console.ReadKey().KeyChar;
            try
            {
                if (confirm.ToString().ToLower() == "y")
                {
                    if (drinkService.Update(drinkUpdateDto, out var errors))
                    {
                        Console.WriteLine("Drink successfully edited");

                    }
                    else
                    {
                        Console.WriteLine("Errors while trying to update a drink");
                        errors.ForEach(error => Console.WriteLine(error));
                    }
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


        }

        private void DeleteDrinks(IDrinkService drinkService)
        {
            Console.Clear();
            Console.WriteLine("Deleting Drinks");
            Console.WriteLine("List of Drinks to Delete");
            var drinks = drinkService.GetAll("Id");
            foreach (var drink in drinks)
            {
                Console.WriteLine($"{drink.Id.ToString().PadLeft(4, ' ')} - {drink.Name}");
            }

            Console.Write("Select DrinkID to Delete (0 to Escape):");
            if (!int.TryParse(Console.ReadLine(), out int drinkId) || drinkId < 0)
            {
                Console.WriteLine("Invalid DrinkID...");
                Console.ReadLine();
                return;
            }
            if (drinkId == 0) return;
            Console.WriteLine($"Are you sure to delete this drink? (y/n)");
            var response = Console.ReadKey().KeyChar;
            if (response.ToString().ToUpper() == "Y")
            {
                if (drinkService.Delete(drinkId, out var errors))
                {
                    Console.WriteLine("Drink Successfully Deleted");

                }
                else
                {
                    Console.WriteLine("Errors while trying to delete a drink");
                    errors.ForEach(x => Console.WriteLine(x));
                }
            }
            else
            {
                Console.WriteLine("Operation cancelled by user");
            }
            Console.ReadLine();
        }

        private void AddDrinks(IDrinkService drinkService, IConfectioneryService confectioneryService)
        {
            Console.Clear();
            Console.WriteLine("Adding New Drinks");
            Console.Write("Enter drink's name:");
            var name = Console.ReadLine();
            Console.Write("Enter Size: ");
            var size = Console.ReadLine();
            Console.WriteLine("List of Confectioneries to Select");

            var confectioneriesList = confectioneryService!.GetAll("Id");
            foreach (var confectionery in confectioneriesList)
            {
                Console.WriteLine($"{confectionery.Id} - {confectionery.Name}");
            }

            Console.Write("Enter ConfectioneryID (0 New Confectionery):");
            if (!int.TryParse(Console.ReadLine(), out var confectioneryId) || confectioneryId < 0)
            {
                Console.WriteLine("Invalid ConfectioneryID....");
                Console.ReadLine();
                return;
            }
            if (confectioneryId > 0)
            {
                var selectedConfectionery = confectioneryService.GetById(confectioneryId);
                if (selectedConfectionery is null)
                {
                    Console.WriteLine("Confectionery not found!!!");
                    Console.ReadLine();
                    return;
                }

            }
            else
            {
                //Entering new confectionery
                Console.WriteLine("Adding a New Confectionery");
                Console.Write("Enter Name:");
                var confectioneryName = Console.ReadLine();

                if (confectioneryService.Exist(confectioneryName!))
                {
                    var existingConfectionery = confectioneryService.GetByName(confectioneryName ?? string.Empty);

                    Console.WriteLine("You have entered an existing confectionery!!!");
                    Console.WriteLine("Assigning his ConfectioneryID");
                    confectioneryId = existingConfectionery!.Id;

                }
                else
                {
                    ConfectioneryCreateDto newConfectionery = new ConfectioneryCreateDto
                    {
                        Name = confectioneryName ?? string.Empty
                    };
                    ConfectioneryDto? confectioneryCreated = null;
                    if (confectioneryService.Create(newConfectionery, out confectioneryCreated, out var authorErrors))
                    {
                        Console.WriteLine("New Confectionery Added Successfully");
                        confectioneryId = confectioneryCreated!.Id;
                    }
                    else
                    {
                        Console.WriteLine("Errors while trying to add a new confectionery");
                        foreach (var item in authorErrors)
                        {
                            Console.WriteLine(item);
                        }
                    }

                }

            }
            DrinkCreateDto drinkDto = new DrinkCreateDto
            {
                Name = name ?? string.Empty,
                Size = size ?? string.Empty,
                ConfectioneryId = confectioneryId
            };

            if (drinkService.Create(drinkDto, out var errors))
            {
                Console.WriteLine("Drink Successfully Added");
            }
            else
            {
                Console.WriteLine("Errors while trying to add a new drink");
                errors.ForEach(error => Console.WriteLine(error));
            }

            Console.ReadLine();
        }

        private void DrinksGroupByConfectionery(IDrinkService drinkService)
        {
            Console.Clear();
            Console.WriteLine("List of Drinks");
            var groups = drinkService.DrinksGroupByConfectionery();
            foreach (var group in groups)
            {
                Console.WriteLine($"ConfectioneryId:{group.Confectionery.Id} - Confectionery: {group.Confectionery.Name}");
                Console.WriteLine("    Drinks");
                if (group.Drinks is not null)
                {
                    foreach (var drink in group.Drinks)
                    {
                        Console.WriteLine($"        {drink.Name}");

                    }

                }
                else
                {
                    Console.WriteLine("         No -fucking- drinks!!!");
                }
            }
            Console.ReadLine();
        }

        private void DrinkList(IDrinkService drinkService)
        {
            Console.Clear();
            Console.WriteLine("List of Available Drinks");
            var drinks = drinkService.GetAll();
            foreach (var book in drinks)
            {
                Console.WriteLine($"{book.Name}");
            }
            Console.ReadLine();
        }
    }
}
