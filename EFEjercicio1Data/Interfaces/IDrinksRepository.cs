using EFEjercicio1Entities;

namespace EFEjercicio1Data.Interfaces
{
    public interface IDrinksRepository
    {
        void Add(Drink drink);
        void Delete(int drinkId);
        bool Exist(string drinkName, int drinkConfectioneryId, int? excludeId = null);

        List<Drink> GetAll(string sortedBy = "Name");
        Drink? GetById(int drinkId, bool tracked = false);
        void Update(Drink drink);
        void SaveChanges();
    }
}