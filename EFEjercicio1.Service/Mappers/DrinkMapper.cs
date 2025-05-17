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

        public static DrinkListDto ToDrinkListDto(Drink drink) => new()
        {
            Id = drink.Id,
            Name = drink.Name

        };

        public static Drink ToEntity(DrinkCreateDto drinkDto) => new()
        {
            Name = drinkDto.Name,
            Size = drinkDto.Size,
            ConfectioneryId = drinkDto.ConfectioneryId
        };

        public static Drink ToEntity(DrinkUpdateDto drinkDto) => new()
        {
            Id = drinkDto.Id,
            Name = drinkDto.Name,
            Size = drinkDto.Size,
            ConfectioneryId = drinkDto.ConfectioneryId
        };
    }
}
