using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Interfaces
{
    public interface IDrinkService
    {
        void Delete(int drinkId);
        bool Exist(string name, string size, int confectioneryId, int? excludeId = null);
        List<Drink> GetAll(string sortedBy = "name");
        Drink? GetById(int drinkId, bool tracked = false);
        void Save(Drink drink);
        Drink? GetByNameAndConfectioneryId(string name, int confectioneryId);

    }
}