using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Interfaces
{
    public interface IConfectioneryService
    {
        void Save(Confectionery confectionery);
        void Delete(int confectioneryId);
        bool Exist(string name, int? excludeId = null);
        List<Confectionery> GetAll(string sortedBy = "Name");
        Confectionery? GetById(int confectioneryId, bool tracked = false);
        bool HasDrinks(int confectioneryId);
        void LoadDrinks(Confectionery confectionery);
        List<Confectionery> GetAllWithDrinks();
        List<IGrouping<int, Drink>> ConfectioneriesGroupIdDrinks();
        Confectionery? GetByName(string name);
    }
}
