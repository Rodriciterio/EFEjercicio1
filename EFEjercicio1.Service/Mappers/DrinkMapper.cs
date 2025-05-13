using EFEjercicio1.Service.DTOs.Drink;
using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Mappers
{
    public static class DrinkMapper
    {
        public static DrinkDto ToDto(Drink drink) => new()
        {
            Id = drink.Id,
            Name = drink.Name
        };
    }
}
