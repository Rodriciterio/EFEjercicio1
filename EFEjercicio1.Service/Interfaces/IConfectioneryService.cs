using EFEjercicio1.Service.DTOs.Confectionery;
using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Interfaces
{
    public interface IConfectioneryService
    {
        bool Create(ConfectioneryCreateDto confectioneryDto, out List<string> errors);
        bool Update(ConfectioneryUpdateDto confectioneryDto, out List<string> errors);
        bool Delete(int confectioneryId, out List<string> errors);
        bool Exist(string name, int? excludeId = null);
        List<ConfectioneryDto> GetAll(string sortedBy = "Name");
        ConfectioneryDto? GetById(int confectioneryId);
        ConfectioneryDto? GetByName(string name);
        List<ConfectioneryWithDrinksDto> GetAllWithDrinks();
        List<ConfectioneryDrinksCountDto> ConfectioneriesWithDrinksCount();
    }
}
