using EFEjercicio1.Service.DTOs.Drink;
using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Interfaces
{
    public interface IDrinkService
    {
        bool Create(DrinkCreateDto drinkDto, out List<string> errors);
        bool Update(DrinkUpdateDto drinkDto, out List<string> errors);
        
        bool Delete(int drinkId, out List<string> errors);
        bool Exist(string name, int confectioneryId, int? excludeId = null);
        List<DrinkListDto> GetAll(string sortedBy = "name");
        DrinkDto? GetById(int drinkId);
        List<DrinksWithConfectioneryDto> DrinksGroupByConfectionery();

    }
}