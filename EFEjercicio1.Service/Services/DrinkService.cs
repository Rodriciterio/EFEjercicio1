using EFEjercicio1.Service.Interfaces;
using EFEjercicio1Data.Interfaces;
using EFEjercicio1Data.Repositories;
using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinksRepository _repository = null!;

        public DrinkService(IDrinksRepository repository)
        {
            _repository = repository;
        }

        public void Delete(int drinkId)
        {
            _repository.Delete(drinkId);
        }

        public bool Exist(string name, string size, int confectioneryId, int? excludeId = null)
        {
            return _repository.Exist(name, size, confectioneryId, excludeId);
        }

        public List<Drink> GetAll(string sortedBy = "name")
        {
            return _repository.GetAll(sortedBy);
        }

        public Drink? GetById(int drinkId, bool tracked = false)
        {
            return _repository.GetById(drinkId, tracked);
        }

        public void Save(Drink drink)
        {
            if (drink.Id == 0)
            {
                _repository.Add(drink);
            }
            else
            {
                _repository.Edit(drink);
            }
        }

        public Drink? GetByNameAndConfectioneryId(string name, int confectioneryId)
        {
            return _repository.GetByNameAndConfectioneryId(name, confectioneryId);
        }

        public void Edit(Drink drink)
        {
            _repository.Edit(drink);
        }

    }
}
