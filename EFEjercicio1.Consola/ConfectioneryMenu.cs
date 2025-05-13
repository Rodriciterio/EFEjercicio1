using EFEjercicio1.Consola.Validators;
using EFEjercicio1.Service.DTOs.Confectionery;
using EFEjercicio1.Service.Interfaces;
using EFEjercicio1Entities;
using Microsoft.Extensions.DependencyInjection;

namespace EFEjercicio1.Consola
{
    public class ConfectioneryMenu
    {
        private readonly IServiceProvider _serviceProvider = null!;

        public ConfectioneryMenu(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void MostrarMenu()
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

                using (var scope = _serviceProvider.CreateScope())
                {
                    var confectioneryService = scope.ServiceProvider
                        .GetRequiredService<IConfectioneryService>();

                    switch (option)
                    {
                        case "1":
                            ListConfectioneries(confectioneryService);
                            break;
                        case "2":
                            AddConfectioneries(confectioneryService);
                            break;
                        case "3":
                            DeleteConfectioneries(confectioneryService);
                            break;
                        case "4":
                            EditConfectioneries(confectioneryService);
                            break;
                        case "5":
                            ListOfConfectioneriesWithDrinks(confectioneryService);
                            break;
                        case "6":
                            ConfectioneriesWithDrinksSummaryOrDetails(confectioneryService);
                            break;
                        case "r":
                            return;
                        default:
                            break;
                    }
                }
           
            } while (true);
        }

        private void ConfectioneriesWithDrinksSummaryOrDetails(IConfectioneryService confectioneryService)
        {
            Console.Clear();
            Console.WriteLine("List of Confectioneries");

            Console.Write("Show (1) Summary or (2) Details? ");
            var option = Console.ReadLine();

            var confectioneriesWithDrinks = confectioneryService.ConfectioneriesWithDrinksCount();
            foreach (var confectionery in confectioneriesWithDrinks)
            {
                Console.WriteLine($"{confectionery.Id} - {confectionery.Name} (Drinks: {confectionery.DrinksCount})");

                if (option == "2") // Opción de detalle
                {
                    if (confectionery.Drinks!.Any())
                    {
                        Console.WriteLine("    Drinks:");
                        foreach (var drink in confectionery.Drinks!)
                        {
                            Console.WriteLine($"     - {drink.Name}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("    No drinks available.");
                    }
                }
            }
            Console.ReadLine();
        }

        private void ListOfConfectioneriesWithDrinks(IConfectioneryService confectioneryService)
        {
            Console.Clear();
            Console.WriteLine("List of Drinks");

            var confectioneriesWithDrinks = confectioneryService.GetAllWithDrinks();
            foreach (var item in confectioneriesWithDrinks)
            {
                Console.WriteLine($"ConfectioneryId:{item.Id} - Confectionery: {item.Name}");
                Console.WriteLine("    Drinks");
                if (item.Drinks.Count > 0)
                {
                    foreach (var drink in item.Drinks)
                    {
                        Console.WriteLine($"        {drink.Name}");
                    }

                }
                else
                {
                    Console.WriteLine("         No drinks available");
                }
            }
            Console.WriteLine("ENTER to continue");
            Console.ReadLine();
        }

        private void EditConfectioneries(IConfectioneryService confectioneryService)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Edit An Confectionery");
                var confectioneries = confectioneryService.GetAll("Id");
                foreach (var confectionery in confectioneries)
                {
                    Console.WriteLine($"{confectionery.Id} - {confectionery.Name}");
                }

                Console.Write("Enter an ConfectioneryID to edit (0 to Escape):");
                int confectioneryId;
                if (!int.TryParse(Console.ReadLine(), out confectioneryId) || confectioneryId < 0)
                {
                    Console.WriteLine("Invalid ConfectioneryId!!!");
                    Console.ReadLine();
                    return;
                }
                if (confectioneryId == 0) return;

                var confectioneryInDb = confectioneryService.GetById(confectioneryId);
                if (confectioneryInDb != null)
                {
                    Console.WriteLine($"Current Confectionery Name: {confectioneryInDb.Name}");
                    Console.Write("Enter New First Name (or ENTER to Keep the same)");
                    var newFirstName = Console.ReadLine();
                    if (string.IsNullOrEmpty(newFirstName))
                    {
                        newFirstName = confectioneryInDb.Name;
                    }

                    var originalConfectionery = confectioneryService.GetById(confectioneryId);

                    Console.Write($"Are you sure to edit \"{originalConfectionery!.Name} \"? (y/n):");
                    var confirm = Console.ReadLine();
                    if (confirm?.ToLower() == "y")
                    {
                        ConfectioneryUpdateDto confectioneryUpdateDto = new ConfectioneryUpdateDto()
                        {
                            Id = confectioneryInDb.Id,
                            Name = newFirstName ?? string.Empty,    
                        };
                        if (confectioneryService.Update(confectioneryUpdateDto, out var errors))
                        {
                            Console.WriteLine("Confectionery successfully updated");
                        }
                        else
                        {
                            Console.WriteLine("Errors while trying to update an confectionery!!");
                            foreach (var message in errors)
                            {
                                Console.WriteLine(message);
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("Operation cancelled by user");
                    }
                }
                else
                {
                    Console.WriteLine("Confectionery does not exist");
                }
                Console.ReadLine();

            } while (true);
        }

        private void DeleteConfectioneries(IConfectioneryService confectioneryService)
        {
            Console.Clear();
            Console.WriteLine("Delete An Confectionery");
            var confectioneries = confectioneryService.GetAll("Id");
            foreach (var confectionery in confectioneries)
            {
                Console.WriteLine($"{confectionery.Id} - {confectionery.Name}");
            }

            Console.Write("Enter an ConfectioneryID to delete (0 to Escape):");
            int confectioneryId;
            if (!int.TryParse(Console.ReadLine(), out confectioneryId) || confectioneryId < 0)
            {
                Console.WriteLine("Invalid ConfectioneryId!!!");
                Console.ReadLine();
                return;
            }
            if (confectioneryId == 0) return;
            var confectioneryInDb = confectioneryService.GetById(confectioneryId);
            if (confectioneryInDb is null)
            {
                Console.WriteLine("ID no found!!!");
                Console.ReadLine();
                return;
            }
            Console.Write($"Are you sure to delete \"{confectioneryInDb.Name} \"? (y/n):");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() == "y")
            {
                if (confectioneryService.Delete(confectioneryId, out var errors))
                {
                    Console.WriteLine("Confectionery Successfully Removed");
                }
                else
                {
                    Console.WriteLine("Error while trying to delete an confectionery");
                    foreach (var message in errors)
                    {
                        Console.WriteLine(message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Operation cancelled by user");
            }

            Console.ReadLine();
        }

        private void AddConfectioneries(IConfectioneryService confectioneryService)
        {
            Console.Clear();
            Console.WriteLine("Adding a new Confectionery");
            Console.Write("Enter Name:");
            var name = Console.ReadLine();

            var confectioneryDto = new ConfectioneryCreateDto
            {
                Name = name ?? string.Empty
            };

            if (confectioneryService.Create(confectioneryDto, out var errors))
            {
                Console.WriteLine("Confectionery Succesfully Added");
            }
            else
            {
                foreach (var message in errors)
                {
                    Console.WriteLine(message.ToString());
                }
            }
            Console.ReadLine();
        }

        private void ListConfectioneries(IConfectioneryService confectioneryService)
        {
            Console.Clear();
            Console.WriteLine("List of Confectioneries");
      
                var confectioneries = confectioneryService.GetAll();

                foreach (var confectionery in confectioneries)
                {
                    Console.WriteLine($"{confectionery.Name}");
                }
            
            Console.WriteLine("ENTER to continue");
            Console.ReadLine();
        }
    }
}
