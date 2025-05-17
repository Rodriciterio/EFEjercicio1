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
            var drinkMenu = new DrinkMenu(_serviceProvider);
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
                        drinkMenu.MostrarMenu();
                        break;
                    case "x":
                        Console.WriteLine("Fin del programa");
                        return;
                    default:
                        break;
                }
            } while (true);
        }       
    }
}
