using EFEjercicio1Entities;

namespace EFEjercicio1Data.Interfaces
{
    public interface IDrinksRepository
    {
        void Add(Drink drink);
        void Delete(int drinkId);
        void Edit(Drink drink);
        List<Drink> GetAll(string sortedBy = "Name");
        Drink? GetById(int drinkId, bool tracked = false);
        Drink? GetByNameAndConfectioneryId(string name, int confectioneryId);
        bool Exist(string name, string size, int confectioneryId, int? excludeId = null);
    }
}